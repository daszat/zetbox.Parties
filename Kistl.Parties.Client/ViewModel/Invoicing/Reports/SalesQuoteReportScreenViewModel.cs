namespace Kistl.Parties.Client.ViewModel.Invoicing.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.GUI;
    using ZBox.Basic.Invoicing;

    [ViewModelDescriptor]
    public class SalesQuoteReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate SalesQuoteReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public SalesQuoteReportScreenViewModel(IViewModelDependencies appCtx,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener)
            : base(appCtx, dataCtx, parent, screen, fileOpener)
        {
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            var stat = DataContext.GetQuery<StatisticActions>().Single();
            return stat.GetSalesQuoteReport(from, until);
        }
    }
}
