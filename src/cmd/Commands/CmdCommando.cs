using System.Collections.Generic;
using cmd.Runner;

namespace cmd.Commands
{
    internal class CmdCommando : Commando
    {
        public CmdCommando(IRunner runner) : base(runner)
        {
            commands.Add("cmd");
            commands.Add("/c");
        }
    }
}