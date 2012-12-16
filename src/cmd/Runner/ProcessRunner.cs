using System;
using System.Diagnostics;
using cmd.Runner.Arguments;

namespace cmd.Runner
{
    internal class ProcessRunner : IRunner
    {
        private readonly Lazy<IArgumentBuilder> argumentBuilder = new Lazy<IArgumentBuilder>(() => new ArgumentBuilder());

        protected virtual IArgumentBuilder ArgumentBuilder
        {
            get { return argumentBuilder.Value; }
        }

        public string BuildArgument(Argument argument)
        {
            return ArgumentBuilder.Build(argument);
        }

        public virtual string Run(IRunOptions runOptions)
        {
            var process = new Process
                        {
                            StartInfo =
                                {
                                    FileName = runOptions.Command,
                                    Arguments = runOptions.Arguments,
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true
                                }
                        };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
    }
}
