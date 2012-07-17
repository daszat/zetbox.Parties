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
    public class PersonViewModel : PartyViewModel
    {
        public new delegate PersonViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public PersonViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Party obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

    }
}
