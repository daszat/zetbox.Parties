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
    public abstract class InvoiceViewModel : ReceiptViewModel
    {
        public new delegate InvoiceViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Invoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public Invoice Invoice { get; private set; }

        public override BaseValueViewModel InternalOrganization
        {
            get { return PropertyModelsByName["InternalOrganization"]; }
        }

        public abstract BaseValueViewModel Issuer { get; }
        public abstract bool IssuerVisible { get; }

        public virtual bool CanceledInvoiceVisible { get { return false; } }
        public virtual bool ReversalVisible { get { return false; } }
    }
}
