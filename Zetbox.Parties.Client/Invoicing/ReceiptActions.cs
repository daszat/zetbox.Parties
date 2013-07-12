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
        public static void postSet_PaymentAmount(Receipt obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if ((obj.Status == null || obj.Status == ReceiptStatus.Open || obj.Status == ReceiptStatus.Partial || obj.Status == ReceiptStatus.Fulfilled) && e.NewValue != 0)
            {
                obj.Status = e.NewValue == obj.Total ? ReceiptStatus.Fulfilled : ReceiptStatus.Partial;
            }
        }
    }
}
