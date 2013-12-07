using System.Collections.Generic;
using cmd.Commands;

namespace cmd.Runner
{
    public interface IRunner
    {
        string Run(IRunOptions runOptions);
        string BuildArgument(Arguments.Argument argument);
        IDictionary<string, string> EnvironmentVariables { get; set; }
        ICommando GetCommand();
    }
}