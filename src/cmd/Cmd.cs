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
            var commando = new Commando(Runner);
            commando.AddCommand(binder.Name);
            result =  commando;
            return true;
        }
    }
}
