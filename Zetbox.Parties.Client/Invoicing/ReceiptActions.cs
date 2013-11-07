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
        public static void postSet_FulfillmentDate(Receipt obj, PropertyPostSetterEventArgs<DateTime?> e)
        {
            if (e.NewValue.HasValue && obj.PaymentAmount == 0)
            {
                obj.PaymentAmount = obj.Total;
            }
        }

        [Invocation]
        public static void postSet_PaymentAmount(Receipt obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (e.NewValue != 0 && obj.Status.In(ReceiptStatus.Open, ReceiptStatus.Partial, ReceiptStatus.Fulfilled))
            {
                obj.Status = e.NewValue == obj.Total ? ReceiptStatus.Fulfilled : ReceiptStatus.Partial;
            }
        }
    }
}
