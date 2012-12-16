namespace cmd.Runner.Arguments
{
    internal class ArgumentBuilder : IArgumentBuilder
    {
        public string Build(Argument argument)
        {
            var flag = BuildFlag(argument);

            if (string.IsNullOrEmpty(flag) && string.IsNullOrEmpty(argument.Value)) return null;
            if (string.IsNullOrEmpty(flag)) return argument.Value;
            return string.IsNullOrEmpty(argument.Value) ? flag : string.Format("{0} {1}", flag, argument.Value);
        }

        protected virtual string BuildFlag(Argument argument)
        {
            if (argument.Flag == null) return null;
            var prefix = argument.Flag.Length == 1 ? "-" : "--";
            return string.Format("{0}{1}", prefix, argument.Flag);
        }
    }
}