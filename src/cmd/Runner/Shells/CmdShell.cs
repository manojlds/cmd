using System;
using cmd.Runner.Arguments;

namespace cmd.Runner.Shells
{
    internal class CmdShell : ProcessRunner
    {
        private readonly Lazy<IArgumentBuilder> argumentBuilder = new Lazy<IArgumentBuilder>(() => new CmdArgumentBuilder());

        protected override IArgumentBuilder ArgumentBuilder
        {
            get { return argumentBuilder.Value; }
        }
    }
}