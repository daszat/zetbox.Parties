using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class PartyRoleActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.PartyRole obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{1} is {0}", obj.Context.GetInterfaceType(obj).Type.Name, obj.Party);
        }
    }
}
