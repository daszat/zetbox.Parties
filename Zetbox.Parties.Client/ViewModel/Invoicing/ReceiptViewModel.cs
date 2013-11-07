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
    using Zetbox.API.Async;

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
                case "Status":
                case "DueDate":
                    OnHighlightChanged();
                    break;
            }
        }

        public abstract BaseValueViewModel Party { get; }
        public abstract BaseValueViewModel InternalOrganization { get; }

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
                switch (Receipt.Status)
                {
                    case ReceiptStatus.Canceled:
                    case ReceiptStatus.WriteOff:
                    case ReceiptStatus.Fulfilled:
                        return Highlight.Deactivated;
                    case ReceiptStatus.Partial:
                        return Highlight.Warning;
                    default:
                        if (Receipt.DueDate < DateTime.Today) return Highlight.Bad;
                        if (Receipt.DueDate.AddDays(-14) < DateTime.Today) return Highlight.Warning;
                        return base.Highlight;
                }
            }
        }

        public override Highlight HighlightAsync
        {
            get
            {
                switch (Receipt.Status)
                {
                    case ReceiptStatus.Canceled:
                    case ReceiptStatus.WriteOff:
                    case ReceiptStatus.Fulfilled:
                        return Highlight.Deactivated;
                    case ReceiptStatus.Partial:
                        return Highlight.Warning;
                    default:
                        if (Receipt.DueDate < DateTime.Today) return Highlight.Bad;
                        if (Receipt.DueDate.AddDays(-14) < DateTime.Today) return Highlight.Warning;
                        return base.Highlight;
                }
            }
        }

        public bool HasDocument
        {
            get
            {
                return Receipt.Document != null && Receipt.Document.Blob != null;
            }
        }
    }
}
