namespace Zetbox.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Basic.Parties;

    /// <summary>
    /// No viewmodel decriptor - Party is abstract
    /// </summary>
    public class PartyViewModel : DataObjectViewModel
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
                var vMdl = ViewModelFactory.CreateViewModel<DataObjectViewModel.Factory>().Invoke(DataContext, this, role);
                var lblMdl = ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, this, "Role", "", vMdl);
                var propGrpMdl = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, this, vMdl.GetInterfaceType().Type.Name, new ViewModel[] { lblMdl });
                groups.Add(propGrpMdl);
            }

            return groups;
        }
    }
}