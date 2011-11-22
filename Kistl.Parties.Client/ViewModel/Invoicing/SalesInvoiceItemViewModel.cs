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
    public class SalesInvoiceItemViewModel : InvoiceItemViewModel
    {
        public new delegate SalesInvoiceItemViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public SalesInvoiceItemViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, SalesInvoiceItem obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.InvoiceItem = obj;
        }

        public new SalesInvoiceItem InvoiceItem { get; private set; }
    }
}
