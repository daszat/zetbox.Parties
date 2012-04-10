namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Parties.Client.ViewModel.Accounting;

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
            _vmf.TriggerDelayedTask(null, () =>
            {
                var mdl = _vmf.CreateViewModel<LinkReceiptTransactionViewModel.Factory>().Invoke(obj.Context, null, obj);
                _vmf.ShowDialog(mdl);
            });
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
    }
}
