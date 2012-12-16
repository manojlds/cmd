namespace cmd.Commands
{
    public interface ICommando
    {
        string Command { get; }
        string Arguments { get; }
        void AddCommand(string command);
    }
}