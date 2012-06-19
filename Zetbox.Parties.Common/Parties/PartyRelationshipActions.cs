using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class PartyRelationshipActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.PartyRelationship obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} from {1:d} thru {2:d}", obj.Context.GetInterfaceType(obj).Type.Name, obj.From, obj.Thru);
        }
    }
}
