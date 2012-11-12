using System;
using System.Dynamic;

namespace cmd
{
    public class Cmd : DynamicObject
    {
        public override bool TryInvokeMember(
        InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                result = new Cmd();
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override bool TryInvoke(
              InvokeBinder binder, object[] args, out object result)
        {
            result = 1;
            return true;
        }
    }
}
