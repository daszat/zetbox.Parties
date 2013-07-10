using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Accounting;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class PurchaseQuoteActions
    {
        [Invocation]
        public static void postSet_Items(PurchaseQuote obj)
        {
            obj.Recalculate("Total");
            obj.Recalculate("TotalNet");
        }
    }
}
