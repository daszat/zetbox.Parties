
namespace Kistl.Parties.Client.ViewModel.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using ZBox.Basic.Accounting;
    using ZBox.Basic.Invoicing;
    using ZBox.Basic.Parties;

    [ViewModelDescriptor]
    public class LinkReceiptTransactionViewModel : WindowViewModel
    {
        public new delegate LinkReceiptTransactionViewModel Factory(IKistlContext dataCtx, ViewModel parent, Transaction obj);

        public LinkReceiptTransactionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Transaction obj)
            : base(appCtx, dataCtx, parent)
        {
            this.Transaction = obj;
        }

        public Transaction Transaction
        {
            get;
            private set;
        }

        private DataObjectViewModel _transactionViewModel;
        public DataObjectViewModel TransactionViewModel
        {
            get
            {
                if (_transactionViewModel == null)
                {
                    _transactionViewModel = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, null, Transaction);
                }
                return _transactionViewModel;
            }
        }

        public override string Name
        {
            get { return "Link receipt & transaction"; }
        }

        #region Commands
        public bool CanApply()
        {
            return Rechnungen != null && Rechnungen.Any(r => r.IsSelected) && Transaction.Amount != 0;
        }

        public void Apply()
        {
            if (!CanApply()) return;

            //var andereÜberzahlungen = NeueZahlung.Klient.Zahlungen
            //    .Where(z => z != NeueZahlung && z.Ueberzahlung != 0)
            //    .OrderBy(z => z.Eingangsdatum)
            //    .ToList();
            var andereÜberzahlungen = new List<Transaction>();
            // aktuelle Zahlung explizit als letzte einfügen
            andereÜberzahlungen.Insert(0, Transaction);

            var gewählteRechnungen = Rechnungen
                .Where(i => i.IsSelected)
                .Select(i => i.Rechnung.Object)
                .Cast<Receipt>()
                .OrderBy(r => r.Date)
                .ToList();

            bool didTransfered = false;
            foreach (var zahlung in andereÜberzahlungen)
            {
                // Zahlungen von alt->neu anrechnen; Rücküberweisungen neu->alt
                foreach (var rechnung in (zahlung.OverPayment > 0 ? gewählteRechnungen.AsEnumerable() : gewählteRechnungen.AsEnumerable().Reverse()))
                {
                    // fertig mit der zahlung?
                    if (zahlung.OverPayment == 0)
                        break;

                    if (rechnung.OpenAmount > 0 && zahlung.OverPayment >= rechnung.OpenAmount)
                    {
                        // komplett Zahlung
                        Transfer(zahlung, rechnung, rechnung.OpenAmount);
                        didTransfered = true;
                    }
                    else if (zahlung.OverPayment > 0 && zahlung.OverPayment < rechnung.OpenAmount)
                    {
                        // teilzahlung
                        Transfer(zahlung, rechnung, zahlung.OverPayment);
                        didTransfered = true;
                    }
                    else if (zahlung.OverPayment < 0 && -zahlung.OverPayment < rechnung.PaymentAmount)
                    {
                        // teil-rücküberweisung
                        Transfer(zahlung, rechnung, zahlung.OverPayment);
                        didTransfered = true;
                    }
                    else if (rechnung.PaymentAmount > 0 && -zahlung.OverPayment >= rechnung.PaymentAmount)
                    {
                        // komplett rücküberweisung
                        // transfer z => r
                        Transfer(zahlung, rechnung, -rechnung.PaymentAmount);
                        didTransfered = true;
                    }
                }
            }
            if (!didTransfered)
            {
                ViewModelFactory.ShowMessage("Es wurde keine Verknüpfung durchgeführt, bitte überprüfen Sie, ob die richigen Rechnungen gewählt wurden", "Warnung");
                return;
            }

            Show = false;
        }

        private Receipt_Transaction Transfer(Transaction zahlung, Receipt rechnung, decimal betrag)
        {
            var r_z = DataContext.Create<Receipt_Transaction>();
            r_z.Receipt = rechnung;
            r_z.Transaction = zahlung;
            r_z.Amount = betrag;
            return r_z;
        }

        private ICommandViewModel _ApplyCommand = null;
        public ICommandViewModel ApplyCommand
        {
            get
            {
                if (_ApplyCommand == null)
                {
                    _ApplyCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        "New",
                        "Creates new links",
                        Apply, CanApply, null);
                }
                return _ApplyCommand;
            }
        }

        public void Cancel()
        {
            Show = false;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, null,
                        "Cancel",
                        "Create new Transaction without links.",
                        Cancel, null, null);
                }
                return _CancelCommand;
            }
        }
        #endregion

        #region Rechnungen

        public class RechnungenSelectionViewModel : ViewModel
        {
            public new delegate RechnungenSelectionViewModel Factory(IKistlContext dataCtx, LinkReceiptTransactionViewModel parent, Receipt rechnung);

            public RechnungenSelectionViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, LinkReceiptTransactionViewModel parent, Receipt rechnung)
                : base(appCtx, dataCtx, parent)
            {
                _rechnung = rechnung;
            }

            private Receipt _rechnung;

            private bool _isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        OnPropertyChanged("IsSelected");
                    }
                }
            }

            private DataObjectViewModel _rechnungViewModel;
            public DataObjectViewModel Rechnung
            {
                get
                {
                    if (_rechnungViewModel == null)
                    {
                        _rechnungViewModel = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, _rechnung);
                    }
                    return _rechnungViewModel;
                }
            }

            public override string Name
            {
                get { return _rechnung.ToString(); }
            }
        }

        private List<RechnungenSelectionViewModel> _rechnungen;
        public List<RechnungenSelectionViewModel> Rechnungen
        {
            get
            {
                if (_rechnungen == null && Transaction.Party != null)
                {
                    _rechnungen = Transaction.Party.PartyRole.OfType<Customer>().SelectMany(c => c.SalesInvoices).Cast<Receipt>()
                          .Concat(Transaction.Party.PartyRole.OfType<Supplier>().SelectMany(c => c.PurchaseInvoices).Cast<Receipt>())
                          .OrderBy(r => r.Date)
                          .Select(r => ViewModelFactory.CreateViewModel<RechnungenSelectionViewModel.Factory>().Invoke(DataContext, this, r))
                          .ToList();
                }
                return _rechnungen;
            }
        }
        #endregion
    }
}
