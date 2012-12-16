using cmd.Commands;

namespace cmd.Runner
{
    internal class RunOptions : IRunOptions
    {
        public RunOptions(Commando commando)
        {
            Command = commando.Command;
            Arguments = commando.Arguments;
        }

        public string Command { get; set; }
        public string Arguments { get; set; }
    }
}