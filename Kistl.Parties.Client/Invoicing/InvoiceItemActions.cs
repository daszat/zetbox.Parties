using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Presentables;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemActions
    {
        [Invocation]
        public static void postSet_UnitPrice(InvoiceItem obj, PropertyPostSetterEventArgs<decimal?> e)
        {
            if (obj.UnitPrice.HasValue)
            {
                obj.AmountNet = obj.Quantity * obj.UnitPrice.Value;
            }
        }

        [Invocation]
        public static void postSet_AmountNet(InvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? (decimal)0) / (decimal)100;
                obj.Amount = obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? (decimal)0);
            }
        }

        [Invocation]
        public static void postSet_VATType(InvoiceItem obj, PropertyPostSetterEventArgs<VATType> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? (decimal)0) / (decimal)100;
                obj.Amount = obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? (decimal)0);
            }
        }
    }
}
