using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class SalesItemActions
    {
        [Invocation]
        public static void postSet_Amount(SalesInvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.SalesInvoice != null)
            {
                obj.SalesInvoice.NotifyPropertyChanged("Total", (decimal)0, (decimal)0);
                obj.SalesInvoice.NotifyPropertyChanged("TotalNet", (decimal)0, (decimal)0);
            }
        }

        [Invocation]
        public static void postSet_AmountNet(SalesInvoiceItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.SalesInvoice != null)
            {
                obj.SalesInvoice.NotifyPropertyChanged("Total", (decimal)0, (decimal)0);
                obj.SalesInvoice.NotifyPropertyChanged("TotalNet", (decimal)0, (decimal)0);
            }
        }
    }
}
