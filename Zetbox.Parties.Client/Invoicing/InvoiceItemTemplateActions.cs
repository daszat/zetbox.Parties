using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemTemplateActions
    {
        [Invocation]
        public static void postSet_Quantity(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.UnitPrice.HasValue)
            {
                obj.AmountNet = obj.Quantity * obj.UnitPrice.Value;
            }
        }

        [Invocation]
        public static void postSet_UnitPrice(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<decimal?> e)
        {
            if (obj.UnitPrice.HasValue)
            {
                obj.AmountNet = obj.Quantity * obj.UnitPrice.Value;
            }
        }

        [Invocation]
        public static void postSet_AmountNet(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? (decimal)0) / (decimal)100;
                obj.Amount = obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? (decimal)0);
            }
        }

        [Invocation]
        public static void postSet_VATType(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<VATType> e)
        {
            if (obj.VATType != null)
            {
                var percent = (obj.VATType.Percentage ?? (decimal)0) / (decimal)100;
                obj.Amount = obj.AmountNet + (obj.AmountNet * percent) + (obj.VATType.Absolute ?? (decimal)0);
            }
        }
    }
}