namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class TransactionActions
    {
        [Invocation]
        public static void ToString(Transaction obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0:d}, {1:n2} - {2}", obj.Date, obj.Amount, obj.Party);
        }

        [Invocation]
        public static void Transfer(Transaction obj, MethodReturnEventArgs<Transaction> e, Account account)
        {
            var newTx = obj.Context.Create<Transaction>();
            newTx.Account = account;
            newTx.Amount = -obj.Amount;
            newTx.Comment = obj.Comment;
            newTx.Date = obj.Date;
            newTx.ImportHash = obj.ImportHash;

            obj.TranferedTo = newTx;

            e.Result = newTx;
            obj.NotifyPropertyChanged("Tranfered", null, null);
        }

        [Invocation]
        public static void get_Tranfered(Transaction obj, PropertyGetterEventArgs<Account> e)
        {
            e.Result = (obj.TranferedFrom != null ? obj.TranferedFrom.Account : null) ?? (obj.TranferedTo != null ? obj.TranferedTo.Account : null);
        }

        [Invocation]
        public static void postSet_Amount(Transaction obj, PropertyPostSetterEventArgs<decimal> e)
        {
            UpdateCalculatedProperties(obj);
        }

        [Invocation]
        public static void postSet_Receipts(Transaction obj)
        {
            UpdateCalculatedProperties(obj);
        }

        [Invocation]
        public static void get_ChargedAmount(Transaction obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Receipts.Sum(r => r.Amount);
        }

        [Invocation]
        public static void get_OverPayment(Transaction obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Amount - obj.Receipts.Sum(r => r.Amount);
        }

        private static void UpdateCalculatedProperties(Transaction obj)
        {
            obj.Recalculate("ChargedAmount");
            obj.Recalculate("OverPayment");
        }   
    }
}
