namespace Zetbox.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;
    using Zetbox.Basic.Invoicing;

    [ViewModelDescriptor]
    public class OpenReceiptsNavigationSearchViewModel : NavigationSearchScreenViewModel
    {
        public new delegate OpenReceiptsNavigationSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public OpenReceiptsNavigationSearchViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, ctxFactory, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        protected override Func<IQueryable> InitializeQueryFactory()
        {
            return () => DataContext.GetQuery<Receipt>().Where(r => r.FulfillmentDate == null || r.OpenAmount != 0);
        }
    }
}
