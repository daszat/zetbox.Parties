using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class PurchaseInvoiceItemActions
    {
        [Invocation]
        public static void postSet_Amount(PurchaseInvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.PurchaseInvoice != null)
            {
                obj.PurchaseInvoice.NotifyPropertyChanged("Total", (decimal)0, (decimal)0);
                obj.PurchaseInvoice.NotifyPropertyChanged("TotalNet", (decimal)0, (decimal)0);
            }
        }

        [Invocation]
        public static void postSet_AmountNet(PurchaseInvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.PurchaseInvoice != null)
            {
                obj.PurchaseInvoice.NotifyPropertyChanged("Total", (decimal)0, (decimal)0);
                obj.PurchaseInvoice.NotifyPropertyChanged("TotalNet", (decimal)0, (decimal)0);
            }
        }
    }
}
