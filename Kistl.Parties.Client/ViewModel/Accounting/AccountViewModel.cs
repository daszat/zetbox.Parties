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
    using Kistl.App.GUI;
    using Kistl.Client;

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
            lst.IsEditable = true;
            lst.ViewMethod = App.GUI.InstanceListViewMethod.Details;
            lst.RequestedKind = Kistl.NamedObjects.Gui.ControlKinds.Kistl_App_GUI_InstanceGridKind.Find(FrozenContext);
            lst.DisplayedColumnsCreated += new InstanceListViewModel.DisplayedColumnsCreatedHandler(lst_DisplayedColumnsCreated);
            lst.ObjectCreated += (obj) =>
            {
                var t = (Transaction)obj;
                t.Account = Account;
                t.Date = DateTime.Today;
            };

            var grp = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Transactions", new[] { lst });
            result.Add(grp);
            return result;
        }

        void lst_DisplayedColumnsCreated(Kistl.Client.Models.GridDisplayConfiguration cols)
        {
            var col = cols.Columns.SingleOrDefault(i => i.Property != null && i.Property.Name == "Category");
            if (col != null)
            {
                var kind = NamedObjects.Gui.ControlKinds.Kistl_App_GUI_ObjectRefDropdownKind.Find(FrozenContext);
                col.GridPreEditKind = kind;
                col.ControlKind = kind;
            }
            col = cols.Columns.SingleOrDefault(i => i.Property != null && i.Property.Name == "Invoices");
            if (col != null)
            {
                col.GridPreEditKind = NamedObjects.Gui.ControlKinds.Kistl_App_GUI_TextKind.Find(FrozenContext);
            }
        }
    }
}
