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
        }

        [Invocation]
        public static void CreateTemplate(OtherIncomeReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
        }
    }
}
