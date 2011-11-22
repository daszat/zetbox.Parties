namespace Kistl.Parties.Client.ViewModel.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.App.Extensions;
    using Kistl.API;
    using ZBox.Basic.Accounting;
    using Kistl.Client.Presentables.KistlBase;

    [ViewModelDescriptor]
    public class AccountViewModel : DataObjectViewModel
    {
        public new delegate AccountViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public AccountViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Account obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Account = obj;
        }

        public Account Account { get; private set; }

        public override string Name
        {
            get { return Account.Name; }
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();
            var lst = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, this, 
                () => DataContext, 
                typeof(Transaction).GetObjectClass(FrozenContext), 
                () => DataContext.GetQuery<Transaction>().Where(i => i.Account == this.Account));
            lst.AllowAddNew = true;
            var grp = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Transactions", new[] { lst });
            result.Add(grp);
            return result;
        }
    }
}
