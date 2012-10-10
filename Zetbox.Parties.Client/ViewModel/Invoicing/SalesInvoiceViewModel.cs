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
    public class SalesInvoiceViewModel : InvoiceViewModel
    {
        public new delegate SalesInvoiceViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public SalesInvoiceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, SalesInvoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();
            PropertyModelsByName["InvoiceID"].IsReadOnly = true;
        }

        public override string Name
        {
            get
            {
                return "Sales invoice: " + base.Name;
            }
        }

        public new SalesInvoice Invoice { get; private set; }

        private ViewModel _customerParty;
        public override ViewModel Party
        {
            get
            {
                if (_customerParty == null)
                {
                    var mdl = new ObjectReferenceValueModel("Customer", "", false, false, (ObjectClass)NamedObjects.Base.Classes.Zetbox.Basic.Parties.Party.Find(FrozenContext));
                    mdl.Value = Invoice.Customer != null ? Invoice.Customer.Party : null;
                    mdl.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "Value")
                        {
                            if (mdl.Value != null)
                            {
                                var party = (Party)mdl.Value;
                                var customer = party.PartyRole.OfType<Customer>().SingleOrDefault();
                                if (customer == null)
                                {
                                    customer = DataContext.Create<Customer>();
                                    customer.Party = party;
                                }
                                Invoice.Customer = customer;
                            }
                            else
                            {
                                Invoice.Customer = null;
                            }
                        }
                    };
                    _customerParty = ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(DataContext, this, mdl);
                }
                return _customerParty;
            }
        }

        public override ViewModel Issuer
        {
            get { return PropertyModelsByName["Issuer"]; }
        }

        public override bool IssuerVisible
        {
            get { return true; }
        }


        protected override IEnumerable<ViewModel> FetchReceiptActions()
        {
            return base.FetchReceiptActions().Concat(new[] { ActionViewModelsByName["CreateInvoiceDocument"], ActionViewModelsByName["FinalizeInvoice"] });
        }
    }
}
