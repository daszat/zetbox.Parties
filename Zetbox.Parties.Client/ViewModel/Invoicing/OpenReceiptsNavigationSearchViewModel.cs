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
        public new delegate OpenReceiptsNavigationSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen);

        public OpenReceiptsNavigationSearchViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        protected override Func<IQueryable> InitializeQueryFactory()
        {
            var today = DateTime.Today;
            return () => DataContext
                .GetQuery<Receipt>()
                .Where(r => r.Status == ReceiptStatus.Open || r.Status == ReceiptStatus.Partial)
                .Where(r => r.Date <= today);
        }
    }
}
