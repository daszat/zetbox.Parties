using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceActions
    {
        [Invocation]
        public static void ToString(Invoice obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}, {1}, {2}, Total {3}/{4}", obj.InvoiceID, obj.Description, obj.Period, obj.TotalNet, obj.Total);
        }
    }
}
