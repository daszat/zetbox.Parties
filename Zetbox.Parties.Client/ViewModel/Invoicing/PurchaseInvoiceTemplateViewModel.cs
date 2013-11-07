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
    using Zetbox.App.Base;
    using Zetbox.Basic.Parties;
    using Zetbox.Parties.Client.ViewModel.Invoicing.Utils;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class PurchaseInvoiceTemplateViewModel : InvoiceTemplateViewModel
    {
        public new delegate PurchaseInvoiceTemplateViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseInvoiceTemplateViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, PurchaseInvoiceTemplate obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public PurchaseInvoiceTemplate Invoice { get; private set; }

        public override string Name
        {
            get
            {
                return "Purchase invoice template: " + base.Name;
            }
        }

        private ViewModel _supplierParty;
        public override ViewModel Party
        {
            get
            {
                if (_supplierParty == null)
                {
                    _supplierParty = PartyRoleReferenceViewModelFactory.Create<PurchaseInvoiceTemplate, Supplier>(ViewModelFactory, DataContext, FrozenContext, this, "Supplier", Invoice, i => i.Supplier);
                }
                return _supplierParty;
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
