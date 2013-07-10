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
    public class SalesQuoteViewModel : QuoteViewModel
    {
        public new delegate SalesQuoteViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public SalesQuoteViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, SalesQuote obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Quote = obj;
        }

        public new SalesQuote Quote { get; private set; }

        private BaseValueViewModel _customerParty;
        public override ViewModel Party
        {
            get
            {
                if (_customerParty == null)
                {
                    _customerParty = PartyRoleReferenceViewModelFactory.Create<SalesQuote, Customer>(ViewModelFactory, DataContext, FrozenContext, this, "Customer", Quote, i => i.Customer);

                }
                return _customerParty;
            }
        }
    }
}
