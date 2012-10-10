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

        public override ViewModel Party
        {
            get
            {
                return PropertyModelsByName["Customer"];
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
