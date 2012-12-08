# cmd #

A C# Library to run external programs / commands in a simpler way. It is inspired from the [sh](https://github.com/amoffat/sh) library for Python.

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
cmd.git.clone("http://github.com/manojlds/cmd")();
```

The above would be equivalent to `git clone http://github.com/manojlds/cmd`.

Note the `()` at the end which actually executes the command. ( yeah it's not nice. Send pull request if you have a better way.)

You can pass flags by naming the arguments:

```csharp
cmd.git.log(grep: "test")();
```

The above would be equivalent to `git log --grep test`

Or:

```csharp
cmd.git.branch(a: true)();
```

which would be equivalent to `git branch -a`

Note that single character flags are mapped as `-<flag>` and multi-character ones are mapped as `--<flag>`

Also, non-string values are ignored and if there is no flag, the argument is not considered.

**What's ahead?**

cmd is in a very nascent stage. More `sh` like goodness coming soon.