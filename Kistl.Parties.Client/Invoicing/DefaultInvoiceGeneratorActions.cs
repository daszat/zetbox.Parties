using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using at.dasz.DocumentManagement;
using Kistl.Parties.Client.Reporting;
using Kistl.App.Base;

namespace ZBox.Basic.Invoicing
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
        public static void CreateDocument(DefaultInvoiceGenerator obj, MethodReturnEventArgs<StaticFile> e, SalesInvoice invoice)
        {
            using (var rpt = _rptFactory())
            {
                var ctx = obj.Context;
                Kistl.Parties.Client.Reporting.Invoicing.SalesInvoice.Call(rpt, invoice);
                using (var s = rpt.GetStream())
                {
                    var name = "Invoice.pdf";
                    var blobID = ctx.CreateBlob(s, name, "application/pdf");
                    var file = ctx.Create<StaticFile>();
                    file.Name = name;
                    file.Blob = ctx.Find<Blob>(blobID);
                    e.Result = file;
                }
            }
        }
    }
}
