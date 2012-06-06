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
    public class PurchaseInvoiceReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate PurchaseInvoiceReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public PurchaseInvoiceReportScreenViewModel(IViewModelDependencies appCtx,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener, ITempFileService tmpService)
            : base(appCtx, dataCtx, parent, screen, fileOpener, tmpService)
        {
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            return null;
        }
    }
}
