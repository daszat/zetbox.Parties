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
    public class PurchaseInvoiceReportScreenViewModel : ReportScreenViewModel
    {
        public new delegate PurchaseInvoiceReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public PurchaseInvoiceReportScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener)
            : base(appCtx, ctxFactory, dataCtx, parent, screen, fileOpener)
        {
        }

        protected override object LoadStatistic()
        {
            return null;
        }
    }
}
