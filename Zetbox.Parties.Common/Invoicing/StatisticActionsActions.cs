
namespace ZBox.Basic.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using ZBox.Basic.Invoicing;

    [Implementor]
    public static class StatisticActionsActions
    {
        [Invocation]
        public static void GetName(StatisticActions obj, MethodReturnEventArgs<string> e)
        {
            // Singelton!
            e.Result = "Invoicing.StatisticActions";
        }
    }
}
