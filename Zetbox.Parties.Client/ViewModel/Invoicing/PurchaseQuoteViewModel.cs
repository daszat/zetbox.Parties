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
    using Zetbox.Parties.Client.ViewModel.Invoicing.Utils;
    using Zetbox.Basic.Parties;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class PurchaseQuoteViewModel : QuoteViewModel
    {
        public new delegate PurchaseQuoteViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseQuoteViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, PurchaseQuote obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Quote = obj;
        }

        public new PurchaseQuote Quote { get; private set; }

        private BaseValueViewModel _supplierParty;
        public override ViewModel Party
        {
            get
            {
                if (_supplierParty == null)
                {
                    _supplierParty = PartyRoleReferenceViewModelFactory.Create<PurchaseInvoice, Supplier>(ViewModelFactory, DataContext, FrozenContext, this, "Supplier", Quote, i => i.Supplier);
                }
                return _supplierParty;
            }
        }
    }
}
