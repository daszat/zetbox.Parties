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

        public new PurchaseInvoiceTemplate Invoice { get; private set; }

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
                    var mdl = new ObjectReferenceValueModel("Supplier", "", false, false, (ObjectClass)NamedObjects.Base.Classes.Zetbox.Basic.Parties.Party.Find(FrozenContext));
                    mdl.Value = Invoice.Supplier != null ? Invoice.Supplier.Party : null;
                    mdl.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "Value")
                        {
                            if (mdl.Value != null)
                            {
                                var party = (Party)mdl.Value;
                                var supplier = party.PartyRole.OfType<Supplier>().SingleOrDefault();
                                if (supplier == null)
                                {
                                    supplier = DataContext.Create<Supplier>();
                                    supplier.Party = party;
                                }
                                Invoice.Supplier = supplier;
                            }
                            else
                            {
                                Invoice.Supplier = null;
                            }
                        }
                    };
                    _supplierParty = ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(DataContext, this, mdl);
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
