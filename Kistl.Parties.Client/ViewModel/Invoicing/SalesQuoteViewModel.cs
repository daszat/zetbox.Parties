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
    public class SalesQuoteViewModel : QuoteViewModel
    {
        public new delegate SalesQuoteViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public SalesQuoteViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, SalesQuote obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Quote = obj;
        }

        public new SalesQuote Quote { get; private set; }

        public override ViewModel Party
        {
            get
            {
                return PropertyModelsByName["Customer"];
            }
        }
    }
}
