using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class ReceiptActions
    {
        [Invocation]
        public static void postSet_Transactions(Receipt obj)
        {
            //if (obj.FulfillmentDate.HasValue == false && Math.Abs(obj.Transactions.Sum(i => i.Amount)) == obj.Total)
            //{
            //    obj.FulfillmentDate = obj.Transactions.Max(i => i.Date);
            //}
        }
    }
}
