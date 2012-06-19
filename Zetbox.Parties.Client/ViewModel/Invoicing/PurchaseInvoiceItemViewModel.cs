namespace Kistl.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.API.Client;
    using ZBox.Basic.Invoicing;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;
    using System.ComponentModel;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class PurchaseInvoiceItemViewModel : InvoiceItemViewModel
    {
        public new delegate PurchaseInvoiceItemViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseInvoiceItemViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, PurchaseInvoiceItem obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.InvoiceItem = obj;
        }

        public new PurchaseInvoiceItem InvoiceItem { get; private set; }
    }
}
