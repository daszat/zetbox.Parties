using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class CustomerRelationshipActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.CustomerRelationship obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} from {1:d} thru {2:d}", obj.Customer, obj.From, obj.Thru);
        }
    }
}
