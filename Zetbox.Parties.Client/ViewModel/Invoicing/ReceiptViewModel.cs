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
    public abstract class ReceiptViewModel : DataObjectViewModel
    {
        public new delegate ReceiptViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public ReceiptViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Receipt obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Receipt = obj;
        }

        public Receipt Receipt { get; private set; }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);

            switch (propName)
            {
                case "FulfillmentDate":
                case "DueDate":
                    OnHighlightChanged();
                    break;
            }
        }

        public abstract ViewModel Party { get; }
        public abstract ViewModel InternalOrganization { get; }

        private IEnumerable<ViewModel> _receiptActions;
        public IEnumerable<ViewModel> ReceiptActions
        {
            get
            {
                if (_receiptActions == null)
                {
                    _receiptActions = FetchReceiptActions();
                }
                return _receiptActions;
            }
        }

        protected virtual IEnumerable<ViewModel> FetchReceiptActions()
        {
            return new ViewModel[] { };
        }

        public override Highlight Highlight
        {
            get
            {
                if (Receipt.FulfillmentDate.HasValue) return Highlight.Deactivated;
                if (Receipt.DueDate < DateTime.Today) return Highlight.Bad;
                if (Receipt.DueDate.AddDays(-14) < DateTime.Today) return Highlight.Warning;
                return base.Highlight;
            }
        }
    }
}
