
namespace Zetbox.Parties.Client.ViewModel.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Basic.Accounting;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Basic.Parties;

    [ViewModelDescriptor]
    public class LinkReceiptTransactionViewModel : WindowViewModel
    {
        public new delegate LinkReceiptTransactionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Transaction obj);

        public LinkReceiptTransactionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Transaction obj)
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
            return Receipts != null && Receipts.Any(r => r.IsSelected) && Transaction.Amount != 0;
        }

        public void Apply()
        {
            if (!CanApply()) return;
            var trans = Transaction;
            var party = trans.Party;
            var otherOverpayments = DataContext.GetQuery<Transaction>()
                .Where(t => t.Party == party && !(t == trans))
                .Where(t => t.OverPayment != 0)
                .OrderBy(z => z.Date)
                .ToList();
            // aktuelle Zahlung explizit als letzte einfügen
            otherOverpayments.Insert(0, Transaction);

            var receipts = Receipts
                .Where(i => i.IsSelected)
                .Select(i => i.Receipt.Object)
                .Cast<Receipt>()
                .OrderBy(r => r.Date)
                .ToList();

            bool didTransfered = false;
            foreach (var payment in otherOverpayments)
            {
            // Zahlungen von alt->neu anrechnen; Rücküberweisungen neu->alt
                foreach (var receipt in (payment.OverPayment > 0 ? receipts.AsEnumerable() : receipts.AsEnumerable().Reverse()))
                {
                    // fertig mit der zahlung?
                    if (payment.OverPayment == 0)
                        break;

                    if (receipt is SalesInvoice || receipt is OtherIncomeReceipt)
                    {
                        // Verkauf -> unsere einkünfte, transaktion ist positiv
                        if (receipt.OpenAmount > 0 && payment.OverPayment >= receipt.OpenAmount)
                        {
                            // komplett Zahlung
                            Transfer(payment, receipt, receipt.OpenAmount);
                            didTransfered = true;
                        }
                        else if (payment.OverPayment > 0 && payment.OverPayment < receipt.OpenAmount)
                        {
                            // teilzahlung
                            Transfer(payment, receipt, payment.OverPayment);
                            didTransfered = true;
                        }
                        else if (payment.OverPayment < 0 && -payment.OverPayment < receipt.PaymentAmount)
                        {
                            // teil-rücküberweisung
                            Transfer(payment, receipt, payment.OverPayment);
                            didTransfered = true;
                        }
                        else if (receipt.PaymentAmount > 0 && -payment.OverPayment >= receipt.PaymentAmount)
                        {
                            // komplett rücküberweisung
                            // transfer z => r
                            Transfer(payment, receipt, -receipt.PaymentAmount);
                            didTransfered = true;
                        }
                    }
                    else
                    {
                        // Einkauf -> unsere ausgaben, transaktion ist negativ
                        if (receipt.OpenAmount > 0 && -payment.OverPayment >= receipt.OpenAmount)
                        {
                            // komplett Zahlung
                            Transfer(payment, receipt, -receipt.OpenAmount);
                            didTransfered = true;
                        }
                        else if (-payment.OverPayment > 0 && -payment.OverPayment < receipt.OpenAmount)
                        {
                            // teilzahlung
                            Transfer(payment, receipt, payment.OverPayment);
                            didTransfered = true;
                        }
                        else if (-payment.OverPayment < 0 && payment.OverPayment < receipt.PaymentAmount)
                        {
                            // teil-rücküberweisung
                            Transfer(payment, receipt, payment.OverPayment);
                            didTransfered = true;
                        }
                        else if (receipt.PaymentAmount > 0 && payment.OverPayment >= receipt.PaymentAmount)
                        {
                            // komplett rücküberweisung
                            // transfer z => r
                            Transfer(payment, receipt, receipt.PaymentAmount);
                            didTransfered = true;
                        }
                    }
                }
            }
            if (!didTransfered)
            {
                ViewModelFactory.ShowMessage("No link was created, please check, if the right receipts where selected.", "Warning");
                return;
            }

            Show = false;
        }

        private Receipt_Transaction Transfer(Transaction payment, Receipt receipt, decimal amount)
        {
            var r_z = DataContext.Create<Receipt_Transaction>();
            r_z.Receipt = receipt;
            r_z.Transaction = payment;
            r_z.Amount = amount;
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

        #region Receipts

        public class ReceiptsSelectionViewModel : ViewModel
        {
            public new delegate ReceiptsSelectionViewModel Factory(IZetboxContext dataCtx, LinkReceiptTransactionViewModel parent, Receipt receipt);

            public ReceiptsSelectionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, LinkReceiptTransactionViewModel parent, Receipt receipt)
                : base(appCtx, dataCtx, parent)
            {
                _receipt = receipt;
            }

            private Receipt _receipt;

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
            public DataObjectViewModel Receipt
            {
                get
                {
                    if (_rechnungViewModel == null)
                    {
                        _rechnungViewModel = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, _receipt);
                    }
                    return _rechnungViewModel;
                }
            }

            public override string Name
            {
                get { return _receipt.ToString(); }
            }
        }

        private List<ReceiptsSelectionViewModel> _receipts;
        public List<ReceiptsSelectionViewModel> Receipts
        {
            get
            {
                if (_receipts == null && Transaction.Party != null)
                {
                    var party = Transaction.Party;
                    _receipts = party.PartyRole.OfType<Customer>().SelectMany(c => c.SalesInvoices).Cast<Receipt>()
                        .Concat(party.PartyRole.OfType<Supplier>().SelectMany(c => c.PurchaseInvoices).Cast<Receipt>())
                        .Concat(party.OtherExpenses.Cast<Receipt>())
                        .Concat(party.OtherIncomes.Cast<Receipt>())
                        .OrderBy(r => r.Date)
                        .Select(r => ViewModelFactory.CreateViewModel<ReceiptsSelectionViewModel.Factory>().Invoke(DataContext, this, r))
                        .ToList();
                }
                return _receipts;
            }
        }
        #endregion
    }
}
