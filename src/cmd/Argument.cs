namespace cmd
{
    public class Argument
    {
        private string flag;

        public Argument(string flag, object value)
        {
            Flag = flag;
            Value = value as string;
        }

        public Argument(object value) : this(null, value)
        {
        }

        private string Flag
        {
            get
            {
                if (flag == null) return null;
                var prefix = flag.Length == 1 ? "-" : "--";
                return string.Format("{0}{1}", prefix, flag);
            }
            set { flag = value; }
        }

        public string Value { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Flag)) return Value;
            if (string.IsNullOrEmpty(Value)) return Flag;
            return string.Format("{0} {1}", Flag, Value);
        }

        public static implicit operator string(Argument argument)
        {
            return argument.ToString();
        }
    }
}