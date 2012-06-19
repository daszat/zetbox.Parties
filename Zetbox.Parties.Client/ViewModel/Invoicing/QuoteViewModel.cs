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
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class QuoteViewModel : DataObjectViewModel
    {
        public new delegate QuoteViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public QuoteViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Quote obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Quote = obj;
        }

        public Quote Quote { get; private set; }

        public abstract ViewModel Party { get; }
    }
}
