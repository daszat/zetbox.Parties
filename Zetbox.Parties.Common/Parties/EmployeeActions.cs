using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class EmployeeActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.Employee obj, MethodReturnEventArgs<System.String> e)
        {
            // Base is good enougth
            // e.Result = string.Format("Employee of {0} from {1:d} thru {2:d}", obj.Party, obj.From, obj.Thru);
        }
    }
}
