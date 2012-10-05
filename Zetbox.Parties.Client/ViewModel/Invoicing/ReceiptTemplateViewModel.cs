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

    /// <summary>
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class ReceiptTemplateViewModel : DataObjectViewModel
    {
        public new delegate ReceiptTemplateViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public ReceiptTemplateViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, ReceiptTemplate obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.ReceiptTemplate = obj;
        }

        public ReceiptTemplate ReceiptTemplate { get; private set; }

        public abstract ViewModel Party { get; }
        public abstract ViewModel InternalOrganization { get; }
    }
}
