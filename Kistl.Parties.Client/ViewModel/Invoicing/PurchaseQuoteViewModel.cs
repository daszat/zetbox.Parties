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
    public class PurchaseQuoteViewModel : QuoteViewModel
    {
        public new delegate PurchaseQuoteViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public PurchaseQuoteViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, PurchaseQuote obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Quote = obj;
        }

        public new PurchaseQuote Quote { get; private set; }

        public override ViewModel Party
        {
            get
            {
                return PropertyModelsByName["Supplier"];
            }
        }
    }
}
