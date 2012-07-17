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

    [ViewModelDescriptor]
    public class PurchaseInvoiceReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate PurchaseInvoiceReportScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public PurchaseInvoiceReportScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener, ITempFileService tmpService)
            : base(appCtx, dataCtx, parent, screen, fileOpener, tmpService)
        {
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            return null;
        }
    }
}
