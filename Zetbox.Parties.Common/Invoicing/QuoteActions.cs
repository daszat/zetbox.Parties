using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Accounting;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class QuoteActions
    {
        [Invocation]
        public static void ToString(Quote obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}, {1}, Total {2}/{3}", obj.QuoteID, obj.Description, obj.TotalNet, obj.Total);
        }

        [Invocation]
        public static void get_Total(Quote obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj is PurchaseQuote)
            {
                var pQuote = (PurchaseQuote)obj;
                e.Result = pQuote.Items.Sum(i => i.Amount);
            }
            else if (obj is SalesQuote)
            {
                var sQuote = (SalesQuote)obj;
                e.Result = sQuote.Items.Sum(i => i.Amount);
            }
        }

        [Invocation]
        public static void get_TotalNet(Quote obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj is PurchaseQuote)
            {
                var pQuote = (PurchaseQuote)obj;
                e.Result = pQuote.Items.Sum(i => i.AmountNet);
            }
            else if (obj is SalesQuote)
            {
                var sQuote = (SalesQuote)obj;
                e.Result = sQuote.Items.Sum(i => i.AmountNet);
            }
        }
    }
}
