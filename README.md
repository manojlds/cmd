# cmd #

A C# Library to run external programs / commands in a simpler way. It is inspired from the [sh](https://github.com/amoffat/sh) library for Python, and is intended to showcase the "dynamic" features of C#

**How to get it?**

Cmd is available through the Nuget Package Manager.

Or, you can build it from source.

**How to use it?**

Create a dynamic instance of Cmd:

```csharp
dynamic cmd = new Cmd();
```

Now, you can call commands off cmd:

```csharp
cmd.git.clone("http://github.com/manojlds/cmd");
```

The above would be equivalent to `git clone http://github.com/manojlds/cmd`.

You can pass flags by naming the arguments:

```csharp
cmd.git.log(grep: "test");
```

The above would be equivalent to `git log --grep test`

Or:

```csharp
cmd.git.branch(a: true);
```

which would be equivalent to `git branch -a`

Note that single character flags are mapped as `-<flag>` and multi-character ones are mapped as `--<flag>`

Also, non-string values are ignored and if there is no flag, the argument is not considered.

You can call multiple commands off the same instance of cmd:

```csharp
var gitOutput = cmd.git();
var svnOutput = cmd.svn();
```

Note that the commands can be case sensitive, and as such `cmd.git` is not same as, say, `cmd.Git`.

**Shells**

You can use cmd to run command on, well, cmd and Powershell. Choose the shell you want to use while creating cmd:

```csharp
dynamic cmd = new Cmd(Shell.Cmd);
dynamic posh = new Cmd(Shell.Powershell);
cmd.dir();
```
`cmd.dir()` is equivalent to `cmd /c dir`

When using `Shell.Cmd`, flags are constructed using `/` instead of `-` and `--`

Powershell support is still a work in progress.

**What's ahead?**

cmd is in a very nascent stage. More `sh` like goodness coming soon.
