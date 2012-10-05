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
    public abstract class InvoiceTemplateViewModel : ReceiptTemplateViewModel
    {
        public new delegate InvoiceTemplateViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceTemplateViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, ReceiptTemplate obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

        public override ViewModel InternalOrganization
        {
            get { return PropertyModelsByName["InternalOrganization"]; }
        }

        public abstract ViewModel Issuer { get; }
        public abstract bool IssuerVisible { get; }
    }
}
