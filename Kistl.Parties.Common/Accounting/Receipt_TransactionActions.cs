namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using ZBox.Basic.Invoicing;

    [Implementor]
    public static class Receipt_TransactionActions
    {
        [Invocation]
        public static void ToString(Receipt_Transaction obj, MethodReturnEventArgs<String> e)
        {
            e.Result = string.Format("{0:n2} - {1}", obj.Amount, obj.Receipt);
        }

        [Invocation]
        public static void postSet_Amount(Receipt_Transaction obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.Receipt != null)
            {
                obj.Receipt.Recalculate("OpenAmount");
                obj.Receipt.Recalculate("PaymentAmount");
            }
            if (obj.Transaction != null)
            {
                obj.Transaction.Recalculate("ChargedAmount");
                obj.Transaction.Recalculate("OverPayment");
            }
        }

        [Invocation]
        public static void postSet_Receipt(Receipt_Transaction obj, PropertyPostSetterEventArgs<Receipt> e)
        {
            if (e.OldValue != null)
            {
                e.OldValue.Recalculate("OpenAmount");
                e.OldValue.Recalculate("PaymentAmount");
            }
            if (e.NewValue != null)
            {
                e.NewValue.Recalculate("OpenAmount");
                e.NewValue.Recalculate("PaymentAmount");
            }
        }

        [Invocation]
        public static void postSet_Transaction(Receipt_Transaction obj, PropertyPostSetterEventArgs<Transaction> e)
        {
            if (e.OldValue != null)
            {
                e.OldValue.Recalculate("ChargedAmount");
                e.OldValue.Recalculate("OverPayment");
            }
            if (e.NewValue != null)
            {
                e.NewValue.Recalculate("ChargedAmount");
                e.NewValue.Recalculate("OverPayment");
            }
        }
    }
}
