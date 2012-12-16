namespace cmd.Runner.Arguments
{
    public class Argument
    {
        public Argument(string flag, object value)
        {
            Flag = flag;
            Value = value as string;
        }

        public Argument(object value) : this(null, value)
        {
        }

        public string Flag { get; private set; }

        public string Value { get; private set; }
    }
}