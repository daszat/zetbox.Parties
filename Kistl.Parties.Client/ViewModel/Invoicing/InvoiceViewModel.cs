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
    public abstract class InvoiceViewModel : ReceiptViewModel
    {
        public new delegate InvoiceViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Invoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public Invoice Invoice { get; private set; }

        public override ViewModel InternalOrganization
        {
            get { return PropertyModelsByName["InternalOrganization"]; }
        }

        public abstract ViewModel Issuer { get; }
        public abstract bool IssuerVisible { get; }
    }
}
