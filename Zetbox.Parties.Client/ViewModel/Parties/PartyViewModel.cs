namespace Zetbox.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;
    using Zetbox.Basic.Accounting;
    using Zetbox.Basic.Parties;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.API.Common;

    /// <summary>
    /// No viewmodel decriptor - Party is abstract
    /// </summary>
    public abstract class PartyViewModel : DataObjectViewModel
    {
        public new delegate PartyViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public PartyViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Party obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Party = obj;
        }

        public Party Party { get; private set; }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var groups = base.CreatePropertyGroups();

            foreach (var role in Party.PartyRole)
            {
                var vMdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, role);
                var roleCls = role.GetObjectClass(FrozenContext);
                var propGrpMdl = ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>().Invoke(
                    DataContext, 
                    this,
                    "Roles",
                    Assets.GetString(roleCls.Module, ZetboxAssetKeys.DataTypes, ZetboxAssetKeys.ConstructNameKey(roleCls), roleCls.Name), 
                    new ViewModel[] { vMdl });
                groups.Add(propGrpMdl);
            }

            var lst = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, this,
                typeof(Transaction).GetObjectClass(FrozenContext),
                () => DataContext.GetQuery<Transaction>().Where(i => i.Party == this.Party));
            lst.AllowAddNew = false;
            lst.AllowDelete = false;
            //lst.IsEditable = false;
            lst.ViewMethod = App.GUI.InstanceListViewMethod.Details;
            lst.RequestedKind = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceGridKind.Find(FrozenContext);
            lst.SetInitialSort("Date");

            var grp = ViewModelFactory.CreateViewModel<CustomPropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Transactions", "Transactions", new[] { lst });
            groups.Add(grp);

            return groups;
        }
    }
}
