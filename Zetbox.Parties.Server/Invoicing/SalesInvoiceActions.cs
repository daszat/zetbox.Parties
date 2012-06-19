using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class SalesInvoiceActions
    {
        [Invocation]
        public static void FinalizeInvoice(SalesInvoice obj)
        {
            if (obj.FinalizedOn.HasValue) throw new InvalidOperationException("Sales Invoice is already finalized");

            obj.InvoiceID = obj.InternalOrganization.InvoiceGenerator.GetNextInvoiceID();
            obj.FinalizedOn = DateTime.Now;
        }
    }
}
