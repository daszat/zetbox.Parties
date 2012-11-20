using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemActions
    {
        [Invocation]
        public static void postSet_Quantity(InvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.UnitPrice.HasValue)
            {
                obj.AmountNet = Math.Round(obj.Quantity * obj.UnitPrice.Value, 2);
            }
        }

        [Invocation]
        public static void postSet_UnitPrice(InvoiceItem obj, PropertyPostSetterEventArgs<decimal?> e)
        {
            if (obj.UnitPrice.HasValue)
            {
                obj.AmountNet = Math.Round(obj.Quantity * obj.UnitPrice.Value, 2);
            }
        }

        [Invocation]
        public static void postSet_AmountNet(InvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? 0m) / 100m;
                obj.Amount = Math.Round(obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? 0m), 2);
            }
        }

        [Invocation]
        public static void postSet_VATType(InvoiceItem obj, PropertyPostSetterEventArgs<VATType> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? 0m) / 100m;
                obj.Amount = Math.Round(obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? 0m), 2);
            }
        }
    }
}
