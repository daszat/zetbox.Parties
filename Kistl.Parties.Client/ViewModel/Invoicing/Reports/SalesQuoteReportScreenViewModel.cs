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

    [ViewModelDescriptor]
    public class SalesQuoteReportScreenViewModel : ReportScreenViewModel
    {
        public new delegate SalesQuoteReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public SalesQuoteReportScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, ctxFactory, dataCtx, parent, screen)
        {
        }

        protected override object LoadStatistic()
        {
            return null;
        }
    }
}
