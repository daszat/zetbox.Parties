using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Invoicing.Invoice obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}, {1}, Total {2}/{3}", obj.InvoiceID, obj.Description, obj.TotalNet, obj.Total);
        }

        [Invocation]
        public static void get_Total(ZBox.Basic.Invoicing.Invoice obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj is PurchaseInvoice)
            {
                e.Result = ((PurchaseInvoice)obj).Items.Sum(i => i.Amount);
            }
            else if (obj is SalesInvoice)
            {
                e.Result = ((SalesInvoice)obj).Items.Sum(i => i.Amount);
            }
        }

        [Invocation]
        public static void get_TotalNet(ZBox.Basic.Invoicing.Invoice obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj is PurchaseInvoice)
            {
                e.Result = ((PurchaseInvoice)obj).Items.Sum(i => i.AmountNet);
            }
            else if (obj is SalesInvoice)
            {
                e.Result = ((SalesInvoice)obj).Items.Sum(i => i.AmountNet);
            }
        }
    }
}
