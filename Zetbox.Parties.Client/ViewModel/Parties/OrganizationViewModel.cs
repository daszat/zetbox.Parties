namespace Zetbox.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Basic.Parties;

    [ViewModelDescriptor]
    public class OrganizationViewModel : PartyViewModel
    {
        public new delegate OrganizationViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public OrganizationViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Organization obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }
    }
}
