namespace Zetbox.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;

    [ViewModelDescriptor]
    public class OpenReceiptsNavigationSearchViewModel : NavigationSearchScreenViewModel
    {
        public new delegate OpenReceiptsNavigationSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public OpenReceiptsNavigationSearchViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        protected override Func<IQueryable> InitializeQueryFactory()
        {
            return () => DataContext.GetQuery<Receipt>().Where(r => r.FulfillmentDate == null || r.OpenAmount != 0);
        }
    }
}
