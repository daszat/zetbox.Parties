namespace Zetbox.Parties.Client.ViewModel.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Basic.Accounting;
    using Zetbox.Client;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class AccountViewModel : DataObjectViewModel
    {
        public new delegate AccountViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public AccountViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Account obj)
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
                typeof(Transaction).GetObjectClass(FrozenContext),
                () => DataContext.GetQuery<Transaction>().Where(i => i.Account == this.Account));
            lst.AllowAddNew = true;
            lst.AllowDelete = true;
            //lst.IsEditable = true;
            lst.ViewMethod = App.GUI.InstanceListViewMethod.Details;
            lst.RequestedKind = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceGridKind.Find(FrozenContext);
            lst.DisplayedColumnsCreated += new InstanceListViewModel.DisplayedColumnsCreatedHandler(lst_DisplayedColumnsCreated);
            lst.SetInitialSort("Date");
            lst.ObjectCreated += (obj) =>
            {
                var t = (Transaction)obj;
                t.Account = Account;
                t.Date = DateTime.Today;
            };

            var grp = ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Transactions", "Transactions", new[] { lst });
            result.Add(grp);
            return result;
        }

        void lst_DisplayedColumnsCreated(Zetbox.Client.Models.GridDisplayConfiguration cols)
        {
            var col = cols.Columns.SingleOrDefault(i => i.Property != null && i.Property.Name == "Receipts");
            if (col != null)
            {
                col.GridPreEditKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(FrozenContext);
            }
            col = cols.Columns.SingleOrDefault(i => i.Property != null && i.Property.Name == "Documents");
            if (col != null)
            {
                col.GridPreEditKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(FrozenContext);
            }
        }
    }
}
