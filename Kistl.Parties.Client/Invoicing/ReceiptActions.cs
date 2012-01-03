using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Presentables;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class ReceiptActions
    {
        [Invocation]
        public static void postSet_Transactions(Receipt obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.FulfillmentDate.HasValue == false && Math.Abs(obj.Transactions.Sum(i => i.Amount)) == obj.Total)
            {
                obj.FulfillmentDate = obj.Transactions.Max(i => i.Date);
            }
        }
    }
}
