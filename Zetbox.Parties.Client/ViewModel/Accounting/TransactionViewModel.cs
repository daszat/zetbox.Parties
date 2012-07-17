namespace Zetbox.Parties.Client.ViewModel.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
using Zetbox.Basic.Accounting;

    [ViewModelDescriptor]
    public class TransactionViewModel : DataObjectViewModel
    {
        public new delegate TransactionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public TransactionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Transaction obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Transaction = obj;
        }

        public Transaction Transaction { get; private set; }

        public override string Name
        {
            get { return Transaction.ToString(); }
        }
    }
}
