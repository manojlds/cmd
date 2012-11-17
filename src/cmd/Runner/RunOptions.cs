namespace cmd.Runner
{
    public class RunOptions
    {
        public RunOptions(Cmd cmd)
        {
            Command = cmd.Command;
            Arguments = cmd.Arguments;
        }

        public string Command { get; set; }
        public string Arguments { get; set; }
    }
}