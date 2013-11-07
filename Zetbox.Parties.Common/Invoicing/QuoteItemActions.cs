using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class QuoteItemActions
    {
        [Invocation]
        public static void postSet_Amount(QuoteItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        [Invocation]
        public static void postSet_AmountNet(QuoteItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            NotifyInvoiceChanged(obj);
        }

        private static void NotifyInvoiceChanged(QuoteItem obj)
        {
            Quote quote = null;
            if (obj is PurchaseQuoteItem) quote = ((PurchaseQuoteItem)obj).PurchaseQuote;
            if (obj is SalesQuoteItem) quote = ((SalesQuoteItem)obj).SalesQuote;
            if (quote != null)
            {
                quote.Recalculate("Total");
                quote.Recalculate("TotalNet");
            }
        }
    }
}
