namespace cmd.Runner.Shells
{
    public abstract class Shell
    {
        public static IRunner Cmd = new CmdShell();
        public static IRunner Powershell = new Posh();
    }
}
