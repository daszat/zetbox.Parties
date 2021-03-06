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
    public static class SalesInvoiceActions
    {
        [Invocation]
        public static void NotifyDeleting(SalesInvoice obj)
        {
            // Workaround - remove from lists
            foreach (var item in obj.Items.ToList())
            {
                obj.Context.Delete(item);
            }
        }

        [Invocation]
        public static void CreateInvoiceDocument(SalesInvoice obj)
        {
            if (obj.InternalOrganization != null && obj.InternalOrganization.InvoiceGenerator != null)
            {
                var ctx = obj.Context;
                var file = obj.InternalOrganization.InvoiceGenerator.CreateDocument(obj);
                if (file == null) return;

                if (obj.Document != null)
                {
                    ctx.Delete(obj.Document);
                }

                obj.Document = file;
            }
        }

        [Invocation]
        public static void CreateInvoiceDocumentCanExec(SalesInvoice obj, MethodReturnEventArgs<bool> e)
        {
            e.Result = obj.InternalOrganization != null && obj.InternalOrganization.InvoiceGenerator != null;
        }

        [Invocation]
        public static void CreateInvoiceDocumentCanExecReason(SalesInvoice obj, MethodReturnEventArgs<string> e)
        {
            if (obj.InternalOrganization == null)
            {
                e.Result = "Internal Organization must be set";
            }
            else if (obj.InternalOrganization.InvoiceGenerator == null)
            {
                e.Result = "Internal Organization must provide a invoice generator.";
            }
        }
    }
}
