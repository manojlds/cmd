using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using cmd.Runner;
using cmd.Runner.Arguments;
using cmd.Runner.Shells;

namespace cmd.Commands
{
    internal class Commando : DynamicObject, ICommando
    {
        protected readonly List<string> commands = new List<string>();
        protected readonly List<Argument> arguments = new List<Argument>();

        protected IRunner Runner { get; set; }

        public Commando(IRunner runner = null)
        {
            Runner = runner ?? new ProcessRunner();
        }

        public virtual string Command
        {
            get { return commands.First(); }
        }

        public string Arguments
        {
            get { return string.Join(" ", commands.Skip(1).Concat(arguments.Select(argument => Runner.BuildArgument(argument)).Where(s => !string.IsNullOrEmpty(s)))); }
        }

        public void AddCommand(string command)
        {
            commands.Add(command);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            AddCommand(binder.Name);
            result = this;
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var names = binder.CallInfo.ArgumentNames;
            var numberOfArguments = binder.CallInfo.ArgumentCount;
            var numberOfNames = names.Count;

            var allNames = Enumerable.Repeat<string>(null, numberOfArguments - numberOfNames).Concat(names);
            arguments.AddRange(allNames.Zip(args, (flag, value) => new Argument(flag, value)));

            var runOptions = new RunOptions(this);
            result = Runner.Run(runOptions);
            return true;
        }
    }
}