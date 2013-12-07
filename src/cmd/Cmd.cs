using System.Collections.Generic;
using System.Dynamic;
using cmd.Runner;
using cmd.Runner.Shells;

namespace cmd
{
    public class Cmd : DynamicObject
    {
        private IRunner Runner { get; set; }

        public Cmd(IRunner runner = null)
        {
            Runner = runner ?? Shell.Default;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var commando = Runner.GetCommand();
            commando.AddCommand(binder.Name);
            result =  commando;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            if (binder.Name == "_Env")
            {
                if (args.Length != 1) return false;

                Runner.EnvironmentVariables = (Dictionary<string, string>)args[0];
                return true;
            }

            return false;
        }
    }
}
