using System;
using System.Collections.Generic;
using System.Diagnostics;
using cmd.Commands;
using cmd.Runner.Arguments;

namespace cmd.Runner.Shells
{
    internal class ProcessRunner : IRunner
    {
        private readonly Lazy<IArgumentBuilder> _argumentBuilder = new Lazy<IArgumentBuilder>(() => new ArgumentBuilder());

        protected virtual IArgumentBuilder ArgumentBuilder
        {
            get { return _argumentBuilder.Value; }
        }

        public string BuildArgument(Argument argument)
        {
            return ArgumentBuilder.Build(argument);
        }

        public IDictionary<string, string> EnvironmentVariables { get; set; }

        public virtual ICommando GetCommand()
        {
            return new Commando(this);
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
                                    RedirectStandardOutput = true,
                                }
                        };
            PopulateEnvironment(process);

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        private void PopulateEnvironment(Process process)
        {
            foreach (var variable in EnvironmentVariables)
            {
                process.StartInfo.EnvironmentVariables[variable.Key] = variable.Value;
            }
        }
    }
}
