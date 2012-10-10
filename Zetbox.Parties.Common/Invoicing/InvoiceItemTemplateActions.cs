using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemTemplateActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Invoicing.InvoiceItemTemplate obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} {1}, {2}", obj.Quantity, obj.Amount, obj.Description);
        }

        [Invocation]
        public static void postSet_Amount(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        [Invocation]
        public static void postSet_AmountNet(InvoiceItemTemplate obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        private static void NotifyInvoiceChanged(InvoiceItemTemplate obj)
        {
            ReceiptTemplate invoice = null;
            if (obj is PurchaseInvoiceItemTemplate) invoice = ((PurchaseInvoiceItemTemplate)obj).PITemplate;
            if (obj is SalesInvoiceItemTemplate) invoice = ((SalesInvoiceItemTemplate)obj).SITemplate;
            if (invoice != null)
            {
                invoice.UpdateTotal();
            }
        }
    }
}
