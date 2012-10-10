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
    public class PurchaseInvoiceViewModel : InvoiceViewModel
    {
        public new delegate PurchaseInvoiceViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseInvoiceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, PurchaseInvoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public new PurchaseInvoice Invoice { get; private set; }

        public override string Name
        {
            get
            {
                return "Purchase invoice: " + base.Name;
            }
        }


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
