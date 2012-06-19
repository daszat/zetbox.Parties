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
    public abstract class ReceiptViewModel : DataObjectViewModel
    {
        public new delegate ReceiptViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public ReceiptViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Receipt obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Receipt = obj;
        }

        public Receipt Receipt { get; private set; }

        public abstract ViewModel Party { get; }
        public abstract ViewModel InternalOrganization { get; }
    }
}
