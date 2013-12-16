using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.Presentables;
using Zetbox.Parties.Client.Reporting;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class PurchaseInvoiceActions
    {
        [Invocation]
        public static void NotifyDeleting(PurchaseInvoice obj)
        {
            // Workaround - remove from lists
            foreach (var item in obj.Items.ToList())
            {
                obj.Context.Delete(item);
            }
        }
    }
}
