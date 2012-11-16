using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class OtherIncomeReceiptActions
    {
        [Invocation]
        public static void GetOpenAmount(OtherIncomeReceipt obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetIncomeOpenAmount(obj);
        }

        [Invocation]
        public static void GetPaymentAmount(OtherIncomeReceipt obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetIncomePaymentAmount(obj);
        }

        [Invocation]
        public static void Duplicate(OtherIncomeReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherIncomeReceipt>();

            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }

        [Invocation]
        public static void CreateTemplate(OtherIncomeReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherIncomeReceiptTemplate>();

            result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }

        [Invocation]
        public static void ChangeTypeTo(OtherIncomeReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e, ReceiptType newType)
        {
        }
    }
}
