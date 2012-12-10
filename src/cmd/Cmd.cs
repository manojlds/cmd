using System.Dynamic;
using cmd.Runner;

namespace cmd
{
    public class Cmd : DynamicObject
    {
        private IRunner Runner { get; set; }

        public Cmd(IRunner runner = null)
        {
            Runner = runner ?? new ProcessRunner();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var commando = new Commando(Runner);
            commando.AddCommand(binder.Name);
            result =  commando;
            return true;
        }
    }
}
