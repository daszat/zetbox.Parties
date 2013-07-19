using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class OtherExpenseReceiptTemplateActions
    {
        [Invocation]
        public static void CreateReceipt(OtherExpenseReceiptTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherExpenseReceipt>();

            result.Date = DateTime.Today;
            result.Description = obj.Description;
            result.Document = obj.Document;
            result.DueDate = obj.DueDate.GetRelative(result.Date);
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Period = obj.Period;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }
    }
}
