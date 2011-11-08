using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Presentables;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class QuoteItemActions
    {
        [Invocation]
        public static void postSet_UnitPrice(QuoteItem obj, PropertyPostSetterEventArgs<decimal?> e)
        {
            if (obj.UnitPrice.HasValue && obj.Amount == 0)
            {
                obj.Amount = obj.Quantity * obj.UnitPrice.Value;
            }
        }
    }
}
