using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using at.dasz.DocumentManagement;
using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.Parties.Client.Reporting;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public class DefaultInvoiceGeneratorActions
    {
        private static Func<ReportingHost> _rptFactory;

        public DefaultInvoiceGeneratorActions(Func<ReportingHost> rptFactory)
        {
            _rptFactory = rptFactory;
        }

        [Invocation]
        public static void CreateDocument(DefaultInvoiceGenerator obj, MethodReturnEventArgs<File> e, SalesInvoice invoice)
        {
            using (var rpt = _rptFactory())
            {
                var ctx = obj.Context;
                Zetbox.Parties.Client.Reporting.Invoicing.SalesInvoice.Call(rpt, invoice);
                using (var s = rpt.GetStream())
                {
                    var name = Helper.GetLegalFileName(string.Format("{0} - Invoice {1}.pdf", invoice.Date.ToString("yyyy-MM-dd"), invoice.Description));
                    var blobID = ctx.CreateBlob(s, name, "application/pdf");
                    var file = ctx.Create<File>();
                    file.IsFileReadonly = true;
                    file.Name = name;
                    file.Blob = ctx.Find<Blob>(blobID);
                    e.Result = file;
                }
            }
        }
    }
}
