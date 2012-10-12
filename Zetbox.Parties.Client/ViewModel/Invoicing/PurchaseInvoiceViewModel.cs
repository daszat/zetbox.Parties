namespace Zetbox.Parties.Client.ViewModel.Invoicing
{
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Basic.Invoicing;
    using Zetbox.App.GUI;
    using Zetbox.Basic.Parties;
    using Zetbox.Parties.Client.ViewModel.Invoicing.Utils;

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

        private ViewModel _supplierParty;
        public override ViewModel Party
        {
            get
            {
                if (_supplierParty == null)
                {
                    _supplierParty = PartyRoleReferenceViewModelFactory.Create<PurchaseInvoice, Supplier>(ViewModelFactory, DataContext, FrozenContext, this, "Supplier", Invoice, i => i.Supplier);
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
