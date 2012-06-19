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
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class InvoiceItemViewModel : DataObjectViewModel
    {
        public new delegate InvoiceItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceItemViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, InvoiceItem obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.InvoiceItem = obj;
        }

        public InvoiceItem InvoiceItem { get; private set; }
    }
}
