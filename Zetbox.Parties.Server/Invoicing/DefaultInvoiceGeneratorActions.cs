using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using at.dasz.DocumentManagement;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class DefaultInvoiceGeneratorActions
    {
        [Invocation]
        public static void GetNextInvoiceID(DefaultInvoiceGenerator obj, MethodReturnEventArgs<string> e)
        {
            // TODO: not thread safe
            var ctx = obj.Context;
            if (obj.LastCreationDate.HasValue && obj.LastCreationDate.Value.Year != DateTime.Today.Year)
            {
                // Reset sequence to 0
                // TODO: This won't work as the squence number is retreived and updated by a stored procedure
                obj.NumberSequence.Data.CurrentNumber = 0;
            }
            int nextNum = ctx.GetSequenceNumber(obj.NumberSequence.ExportGuid);
            obj.LastCreationDate = DateTime.Now;
            e.Result = string.Format(obj.InvoiceIDFormatString, DateTime.Now, nextNum);
        }
    }
}
