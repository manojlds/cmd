using System;
using System.Dynamic;
using System.Runtime.Serialization.Formatters.Binary;

namespace cmd.Commands
{
    public class PowershellCommando : DynamicObject, ICommando
    {
        public string Command { get; private set; }
        public string Arguments { get; private set; }
        public void AddCommand(string command)
        {
            throw new System.NotImplementedException();
        }
    }

}