using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class OtherExpenseReceiptActions
    {
        [Invocation]
        public static void GetOpenAmount(OtherExpenseReceipt obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetExpenseOpenAmount(obj);
        }

        [Invocation]
        public static void GetPaymentAmount(OtherExpenseReceipt obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetExpensePaymentAmount(obj);
        }

        [Invocation]
        public static void Duplicate(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherExpenseReceipt>();

            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }

        [Invocation]
        public static void CreateTemplate(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherExpenseReceiptTemplate>();

            result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }
    }
}
