using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using cmd.Runner;

namespace cmd
{
    public class Cmd : DynamicObject
    {
        private readonly List<string> commands = new List<string>();
        private readonly List<Argument> arguments = new List<Argument>();

        public Cmd(IRunner runner = null)
        {
            Runner = runner ?? new ProcessRunner();
        }

        private IRunner Runner { get; set; }

        public string Command
        {
            get { return commands.First(); }
        }

        public string Arguments
        {
            get { return string.Join(" ", commands.Skip(1).Concat(arguments.Select(argument => argument.ToString()).Where(s => !string.IsNullOrEmpty(s)))); }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            commands.Add(binder.Name);
            result = this;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var names = binder.CallInfo.ArgumentNames;
            var numberOfArguments = binder.CallInfo.ArgumentCount;
            var numberOfNames = names.Count;

            var allNames = Enumerable.Repeat<string>(null, numberOfArguments - numberOfNames).Concat(names);
            arguments.AddRange(allNames.Zip(args, (flag, value) => new Argument(flag, value)));

            commands.Add(binder.Name);
            result = this;
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            if (!commands.Any())
            {
                result = null;
                return true;
            }

            var runOptions = new RunOptions(this);
            result = Runner.Run(runOptions);
            return true;
        }
    }
}
