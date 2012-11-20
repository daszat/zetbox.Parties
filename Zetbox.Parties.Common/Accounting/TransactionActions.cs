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

            obj.TransferedTo = newTx;

            e.Result = newTx;
            obj.NotifyPropertyChanged("Transfered", null, null);
        }

        [Invocation]
        public static void get_Transfered(Transaction obj, PropertyGetterEventArgs<Account> e)
        {
            e.Result = (obj.TransferedFrom != null ? obj.TransferedFrom.Account : null) ?? (obj.TransferedTo != null ? obj.TransferedTo.Account : null);
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

        [Invocation]
        public static void get_AmountNet(Transaction obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj.Receipts.Count == 0)
            {
                e.Result = obj.Amount;
            }
            else
            {
                var receipts = obj.Receipts.Where(r => r.Receipt != null).Select(r => r.Receipt).ToList();
                if (receipts.Sum(r => r.Total) == obj.Amount)
                {
                    e.Result = Math.Round(receipts.Sum(r => r.TotalNet), 2);
                }
                else if (receipts.Sum(r => r.TotalNet) != 0)
                {
                    var vatp = receipts.Sum(r => r.Total) / receipts.Sum(r => r.TotalNet);
                    e.Result = Math.Round(obj.Amount / vatp, 2);
                }
                else
                {
                    e.Result = obj.Amount;
                }
            }
        }

        [Invocation]
        public static void get_VAT(Transaction obj, PropertyGetterEventArgs<decimal> e)
        {
            if (obj.Receipts.Count == 0)
            {
                e.Result = 0;
            }
            else
            {
                var receipts = obj.Receipts.Where(r => r.Receipt != null).Select(r => r.Receipt).ToList();
                if (receipts.Sum(r => r.Total) == obj.Amount)
                {
                    e.Result = Math.Round(receipts.Sum(r => r.TotalNet), 2);
                }
                else
                {
                    var vatp = receipts.Sum(r => r.Total) / receipts.Sum(r => r.TotalNet);
                    e.Result = Math.Round(obj.Amount - (obj.Amount / vatp), 2);
                }
            }
        }

        private static void UpdateCalculatedProperties(Transaction obj)
        {
            obj.Recalculate("ChargedAmount");
            obj.Recalculate("OverPayment");
            obj.Recalculate("AmountNet");
            obj.Recalculate("VAT");
        }   
    }
}
