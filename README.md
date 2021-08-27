[![Build Status](https://app.travis-ci.com/poychang/DotNetNotebook.EnvMagicCommand.svg?branch=main)](https://app.travis-ci.com/github/poychang/DotNetNotebook.EnvMagicCommand)
[![NuGet version](https://badge.fury.io/nu/DotNetNotebook.EnvMagicCommand.svg)](https://badge.fury.io/nu/DotNetNotebook.EnvMagicCommand)

# Env Magic Command

Load .env file in .NET Interactive Notebook. This package helps .NET Notebook player custom their environment easier.

## How to use

Try it by running: `#!env -f [file-path] -n [variable-name]`

You can use `#!env --help` in .NET Notebook to get help description.

```
#!env
  Load .env to .NET Interactive Notebook.

Usage:
  [options] #!env

Options:
  -f, --file-path <file-path>  The .env file path
  -n, --var-name <var-name>    The variable name which contain the .env setting
  -?, -h, --help               Show help and usage information
```

Assume you have a `.env` file in your project root directory. After install this magic command in .NET Notebook, you can use it like this:

```
#!env -f ".\.env" -n MyEnv
display(MyEnv["YOUR_VARIABLE"]);
```

You can find more notebook samples in `samples` folder.
