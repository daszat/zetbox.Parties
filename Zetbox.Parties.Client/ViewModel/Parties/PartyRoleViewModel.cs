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

    [ViewModelDescriptor]
    public class PartyRoleViewModel : DataObjectViewModel
    {
        public new delegate PartyRoleViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public PartyRoleViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            PartyRole obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.PartyRole = obj;
        }

        public PartyRole PartyRole { get; private set; }
    }
}
