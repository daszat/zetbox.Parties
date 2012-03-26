using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class ReceiptActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Invoicing.Receipt obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}, Total {1}/{2}", obj.Description, obj.TotalNet, obj.Total);
        }

        [Invocation]
        public static void UpdateTotal(ZBox.Basic.Invoicing.Receipt obj)
        {
            // Do nothing, work is done in derived classes
        }

        [Invocation]
        public static void get_FulfillmentAmount(ZBox.Basic.Invoicing.Receipt obj, PropertyGetterEventArgs<decimal?> e)
        {
            e.Result = 0; // TODO: Not possible! obj.Transactions.Sum(i => i.Amount);
        }

        [Invocation]
        public static void get_FulfillmentAmountNet(ZBox.Basic.Invoicing.Receipt obj, PropertyGetterEventArgs<decimal?> e)
        {
            e.Result = 0; // TODO: Not possible! obj.Transactions.Sum(i => i.Amount);
        }
    }
}
