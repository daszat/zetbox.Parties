
namespace Zetbox.Basic.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    [Implementor]
    public static class EmployeeActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.Employee obj, MethodReturnEventArgs<System.String> e)
        {
            // Base is good enough
            // e.Result = string.Format("Employee of {0} from {1:d} thru {2:d}", obj.Party, obj.From, obj.Thru);
        }
        [Invocation]
        public static void postSet_Identity(Employee obj, PropertyPostSetterEventArgs<Identity> e)
        {
            if (obj.TimeSheet != null)
            {
                // keep the timesheet owned by the employee
                obj.TimeSheet.Owner = e.NewValue;
            }
        }
    }
}
