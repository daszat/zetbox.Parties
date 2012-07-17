
namespace Zetbox.Basic.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Basic.Invoicing;

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
