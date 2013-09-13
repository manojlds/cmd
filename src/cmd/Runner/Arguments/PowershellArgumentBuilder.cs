namespace cmd.Runner.Arguments
{
    internal class PowershellArgumentBuilder : ArgumentBuilder
    {
        protected override string BuildFlag(Argument argument)
        {
            return argument.Flag == null ? null : string.Format("-{0}", argument.Flag);
        }
    }
}