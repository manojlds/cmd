using cmd.Commands;

namespace cmd.Runner
{
    public interface IRunner
    {
        string Run(IRunOptions runOptions);
        string BuildArgument(Arguments.Argument argument);
        ICommando GetCommand();
    }
}