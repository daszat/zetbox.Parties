using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceActions
    {
        [Invocation]
        public static void ToString(Invoice obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}, {1}, Total {2}/{3}", obj.InvoiceID, obj.Description, obj.TotalNet, obj.Total);
        }
    }
}
