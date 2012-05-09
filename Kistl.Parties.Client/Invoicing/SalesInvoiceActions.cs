using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Presentables;
using Kistl.Parties.Client.Reporting;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public class SalesInvoiceActions
    {
        private static IViewModelFactory _factory;
        private static Func<ReportingHost> _rptFactory;

        public SalesInvoiceActions(IViewModelFactory factory, Func<ReportingHost> rptFactory)
        {
            _factory = factory;
            _rptFactory = rptFactory;
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
            e.Result = obj.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified);
        }

        [Invocation]
        public static void CreateInvoiceDocumentCanExecReason(SalesInvoice obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "Object must be already created. Please save the invoice and try again.";
        }
    }
}
