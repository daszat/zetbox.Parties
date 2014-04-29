
namespace Zetbox.Basic.HR
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
        public static void ToString(Employee obj, MethodReturnEventArgs<System.String> e)
        {
            // Base is good enough
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
