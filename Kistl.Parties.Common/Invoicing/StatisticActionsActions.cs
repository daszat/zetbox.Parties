
namespace ZBox.Basic.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using ZBox.Basic.Invoicing;

    public static class StatisticActionsActions
    {
        public static void GetName(StatisticActions obj, MethodReturnEventArgs<string> e)
        {
            // Singelton!
            e.Result = "StatisticActions";
        }
    }
}
