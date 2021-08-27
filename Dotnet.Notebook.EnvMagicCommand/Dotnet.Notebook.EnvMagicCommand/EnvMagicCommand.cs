using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Formatting;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace Dotnet.Notebook.EnvMagicCommand
{
    public class LoadEnvFileKernelExtension : IKernelExtension
    {
        public Task OnLoadAsync(Kernel kernel)
        {
            var loadEnvCommand = new Command("#!env", "Load .env to .NET Interactive Notebook.")
            {
                new Option<string>(new[]{ "-f", "--file-path" }, "The .env file path"),
                new Option<string>(new[]{ "-n", "--var-name" }, "The variable name which contain the .env setting"),
            };

            loadEnvCommand.Handler = CommandHandler.Create(
                async (string filePath, string varName, KernelInvocationContext invocationContext) =>
                {
                    var filePathString = string.IsNullOrEmpty(filePath) ? string.Empty : Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, filePath));
                    var command = new SubmitCode($"var {varName} = dotenv.net.DotEnv.Fluent().WithEnvFiles(\"{filePathString.Replace(@"\", @"\\")}\").Read();");

                    PocketView outputMessage = PocketViewTags.div(
                        "Loaded .env file (", PocketViewTags.code(filePathString), ")", PocketViewTags.br,
                        "Now you can use ", PocketViewTags.code($"{varName}[\"YOUR_VARIABLE\"]"), " to read the env value.", PocketViewTags.br
                    );
                    invocationContext.Display(outputMessage);

                    await invocationContext.HandlingKernel.SendAsync(command);
                });

            kernel.AddDirective(loadEnvCommand);

            if (KernelInvocationContext.Current is { } context)
            {
                PocketView view = PocketViewTags.div(
                    PocketViewTags.code(nameof(EnvMagicCommand)), " is loaded. It adds loading feature for .env file.", PocketViewTags.br,
                    "Try it by running: ", PocketViewTags.code("#!env -f [file-path] -n [variable-name]")
                );

                context.Display(view);
            }

            return Task.CompletedTask;
        }
    }
}
