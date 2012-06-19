namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Parties.Client.ViewModel.Accounting;

    [Implementor]
    public class TransactionActions
    {
        private static IViewModelFactory _vmf;

        public TransactionActions(IViewModelFactory vmf)
        {
            _vmf = vmf;
        }

        [Invocation]
        public static void NotifyCreated(Transaction obj)
        {
        }

        [Invocation]
        public static void NotifyDeleting(Transaction obj)
        {
            // Workaround - remove from lists
            foreach (Receipt_Transaction r_z in obj.Receipts.ToList())
            {
                obj.Context.Delete(r_z);
            }
        }

        [Invocation]
        public static void LinkReceipts(Transaction obj)
        {
            var mdl = _vmf.CreateViewModel<LinkReceiptTransactionViewModel.Factory>().Invoke(obj.Context, null, obj);
            _vmf.ShowDialog(mdl);
        }
    }
}
