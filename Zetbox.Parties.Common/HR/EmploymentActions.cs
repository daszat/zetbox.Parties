using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.HR
{
    [Implementor]
    public static class EmploymentActions
    {
        [Invocation]
        public static void ToString(Employment obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("Employment of {0} from {1:d} thru {2:d}", obj.Employee.Party, obj.From, obj.Thru);
        }
    }
}
