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
        public static void NotifyPreSave(SalesInvoice obj)
        {
            if (obj.ObjectState == DataObjectState.New && obj.InternalOrganization != null && obj.InternalOrganization.InvoiceGenerator != null)
            {
                obj.InvoiceID = obj.InternalOrganization.InvoiceGenerator.GetNextInvoiceID();
            }
        }
    }
}
