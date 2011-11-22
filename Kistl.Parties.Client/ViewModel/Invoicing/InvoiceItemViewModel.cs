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
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class InvoiceItemViewModel : DataObjectViewModel
    {
        public new delegate InvoiceItemViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceItemViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, InvoiceItem obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.InvoiceItem = obj;
        }

        public InvoiceItem InvoiceItem { get; private set; }
    }
}
