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
    public class PurchaseInvoiceViewModel : InvoiceViewModel
    {
        public new delegate PurchaseInvoiceViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseInvoiceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, PurchaseInvoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public new PurchaseInvoice Invoice { get; private set; }

        public override ViewModel Party
        {
            get
            {
                return PropertyModelsByName["Supplier"];
            }
        }

        public override ViewModel Issuer
        {
            get { return null; }
        }

        public override bool IssuerVisible
        {
            get { return false; }
        }

    }
}
