using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class OtherIncomeReceiptTemplateActions
    {
        [Invocation]
        public static void CreateReceipt(OtherIncomeReceiptTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherIncomeReceipt>();

            result.Date = DateTime.Today;
            result.Description = obj.Description;
            result.Document = obj.Document;
            result.DueDate = obj.DueDate.GetCurrent(result.Date);
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
