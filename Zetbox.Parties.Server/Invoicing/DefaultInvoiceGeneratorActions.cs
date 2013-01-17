using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using at.dasz.DocumentManagement;
using Zetbox.API.Server;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public class DefaultInvoiceGeneratorActions
    {
        private static Func<IZetboxServerContext> _srvCtxFactory;
        public DefaultInvoiceGeneratorActions(Func<IZetboxServerContext> srvCtxFactory)
        {
            _srvCtxFactory = srvCtxFactory;
        }

        [Invocation]
        public static void GetNextInvoiceID(DefaultInvoiceGenerator obj, MethodReturnEventArgs<string> e)
        {
            // TODO: not thread safe
            var ctx = obj.Context;
            if (obj.LastCreationDate.HasValue && obj.LastCreationDate.Value.Year != DateTime.Today.Year)
            {
                // Reset sequence to 0
                using (var srvCtx = _srvCtxFactory())
                {
                    var srvObj = srvCtx.Find<DefaultInvoiceGenerator>(obj.ID);
                    srvObj.NumberSequence.Data.CurrentNumber = 0;
                    srvCtx.SubmitChanges();
                }
            }
            int nextNum = ctx.GetSequenceNumber(obj.NumberSequence.ExportGuid);
            obj.LastCreationDate = DateTime.Now;
            e.Result = string.Format(obj.InvoiceIDFormatString, DateTime.Now, nextNum);
        }
    }
}
