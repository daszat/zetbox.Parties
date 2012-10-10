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
        }

        [Invocation]
        public static void CreateTemplate(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
        }
    }
}
