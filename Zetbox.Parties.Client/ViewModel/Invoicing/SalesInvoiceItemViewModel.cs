namespace Zetbox.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using System.ComponentModel;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class SalesInvoiceItemViewModel : InvoiceItemViewModel
    {
        public new delegate SalesInvoiceItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public SalesInvoiceItemViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, SalesInvoiceItem obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.InvoiceItem = obj;
        }

        public new SalesInvoiceItem InvoiceItem { get; private set; }
    }
}
