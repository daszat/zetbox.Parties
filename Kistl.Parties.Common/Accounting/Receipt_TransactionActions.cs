namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

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
    }
}
