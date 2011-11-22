using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Invoicing.InvoiceItem obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} {1}, {2}", obj.Quantity, obj.Amount, obj.Description);
        }

        [Invocation]
        public static void postSet_Amount(InvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        [Invocation]
        public static void postSet_AmountNet(InvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        private static void NotifyInvoiceChanged(InvoiceItem obj)
        {
            Invoice invoice = null;
            if (obj is PurchaseInvoiceItem) invoice = ((PurchaseInvoiceItem)obj).PurchaseInvoice;
            if (obj is SalesInvoiceItem) invoice = ((SalesInvoiceItem)obj).SalesInvoice;
            if (invoice != null)
            {
                invoice.NotifyPropertyChanged("Total", (decimal)0, (decimal)0);
                invoice.NotifyPropertyChanged("TotalNet", (decimal)0, (decimal)0);
            }
        }
    }
}
