namespace Zetbox.Parties.Client.ViewModel.Invoicing.Reports
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
    public class SalesQuoteReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate SalesQuoteReportScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public SalesQuoteReportScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener, ITempFileService tmpService)
            : base(appCtx, dataCtx, parent, screen, fileOpener, tmpService)
        {
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            var stat = DataContext.GetQuery<StatisticActions>().SingleOrDefault();
            if (stat == null)
            {
                stat = DataContext.Create<StatisticActions>();
            }
            return stat.GetSalesQuoteReport(from, until);
        }
    }
}
