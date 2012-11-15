
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
    using at.dasz.DocumentManagement;
    using Zetbox.App.Base;
    using System.IO;

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
        public bool CanApply()
        {
            return Receipts != null && Receipts.Any(r => r.IsSelected) && Transaction.Amount != 0;
        }

        public void Apply()
        {
            if (!CanApply()) return;

            var receipts = Receipts
                .Where(i => i.IsSelected)
                .Select(i => i.Receipt.Object)
                .Cast<Receipt>()
                .OrderBy(r => r.Date)
                .ToList();

            bool didTransfered = LinkReceiptsTransaction(receipts);
            if (!didTransfered)
            {
                ViewModelFactory.ShowMessage("No link was created, please check, if the right receipts where selected.", "Warning");
                return;
            }

            Show = false;
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
        #endregion

        #region Templates
        public class TemplateSelectionViewModel : ViewModel
        {
            public new delegate TemplateSelectionViewModel Factory(IZetboxContext dataCtx, LinkReceiptTransactionViewModel parent, ReceiptTemplate template);

            public TemplateSelectionViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, LinkReceiptTransactionViewModel parent, ReceiptTemplate template)
                : base(appCtx, dataCtx, parent)
            {
                _template = template;
            }

            private ReceiptTemplate _template;

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

            private DataObjectViewModel _templateViewModel;
            public DataObjectViewModel Template
            {
                get
                {
                    if (_templateViewModel == null)
                    {
                        _templateViewModel = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, _template);
                    }
                    return _templateViewModel;
                }
            }

            public override string Name
            {
                get { return _template.ToString(); }
            }
        }

        private List<TemplateSelectionViewModel> _templates;
        public List<TemplateSelectionViewModel> Templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = DataContext
                        .GetQuery<ReceiptTemplate>()
                        .ToList()
                        .Select(t => ViewModelFactory.CreateViewModel<TemplateSelectionViewModel.Factory>().Invoke(DataContext, this, t))
                        .ToList();
                }
                return _templates;
            }
        }

        private ICommandViewModel _CreateFromTemplateCommand = null;
        public ICommandViewModel CreateFromTemplateCommand
        {
            get
            {
                if (_CreateFromTemplateCommand == null)
                {
                    _CreateFromTemplateCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        "Create",
                        "Creates a new receipt from a template",
                        CreateFromTemplate,
                        CanCreateFromTemplate,
                        null);
                }
                return _CreateFromTemplateCommand;
            }
        }

        public void CreateFromTemplate()
        {
            if (!CanCreateFromTemplate()) return;

            var template = (ReceiptTemplate)Templates.Single(i => i.IsSelected).Template.Object;
            var newReceipt = template.CreateReceipt();

            if (newReceipt is OtherExpenseReceipt)
            {
                var oe = (OtherExpenseReceipt)newReceipt;
                if (oe.Total == 0 && oe.TotalNet == 0)
                {
                    oe.Total = oe.TotalNet = -Transaction.Amount;
                }
            }
            else if (newReceipt is OtherIncomeReceipt)
            {
                var oe = (OtherIncomeReceipt)newReceipt;
                if (oe.Total == 0 && oe.TotalNet == 0)
                {
                    oe.Total = oe.TotalNet = Transaction.Amount;
                }
            }

            LinkAndSow(newReceipt);
            Show = false;
        }

        public bool CanCreateFromTemplate()
        {
            return Templates != null && Templates.Count(r => r.IsSelected) == 1 && Transaction.Amount != 0;
        }
        #endregion

        #region Create new receipt
        private ICommandViewModel _CreatePurchaseInvoiceCommand = null;
        public ICommandViewModel CreatePurchaseInvoiceCommand
        {
            get
            {
                if (_CreatePurchaseInvoiceCommand == null)
                {
                    _CreatePurchaseInvoiceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Purchase invoice", "Creates a new purchase invoice", CreatePurchaseInvoice, null, null);
                }
                return _CreatePurchaseInvoiceCommand;
            }
        }

        public void CreatePurchaseInvoice()
        {
            var newReceipt = DataContext.Create<PurchaseInvoice>();
            newReceipt.Supplier = Transaction.Party.PartyRole.OfType<Supplier>().FirstOrDefault();
            newReceipt.FulfillmentDate = Transaction.Date;
            
            var newItem = DataContext.Create<PurchaseInvoiceItem>();
            newItem.Amount = -Transaction.Amount;
            newReceipt.Items.Add(newItem);

            newReceipt.Document = CreateDocumentFromTransaction();

            LinkAndSow(newReceipt);
            Show = false;
        }

        private ICommandViewModel _CreateOtherExpenseCommand = null;
        public ICommandViewModel CreateOtherExpenseCommand
        {
            get
            {
                if (_CreateOtherExpenseCommand == null)
                {
                    _CreateOtherExpenseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Other expense", "Creates a other expence receipts", CreateOtherExpense, null, null);
                }
                return _CreateOtherExpenseCommand;
            }
        }

        public void CreateOtherExpense()
        {
            var newReceipt = DataContext.Create<OtherExpenseReceipt>();
            newReceipt.Party = Transaction.Party;
            newReceipt.FulfillmentDate = Transaction.Date;
            newReceipt.Total = -Transaction.Amount;
            newReceipt.TotalNet = -Transaction.Amount;

            newReceipt.Document = CreateDocumentFromTransaction();
            
            LinkAndSow(newReceipt);
            Show = false;
        }

        private ICommandViewModel _CreateOtherIncomeCommand = null;
        public ICommandViewModel CreateOtherIncomeCommand
        {
            get
            {
                if (_CreateOtherIncomeCommand == null)
                {
                    _CreateOtherIncomeCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Other income", "Creates a other income receipts", CreateOtherIncome, null, null);
                }
                return _CreateOtherIncomeCommand;
            }
        }

        public void CreateOtherIncome()
        {
            var newReceipt = DataContext.Create<OtherIncomeReceipt>();
            newReceipt.Party = Transaction.Party;
            newReceipt.FulfillmentDate = Transaction.Date;
            newReceipt.Total = Transaction.Amount;
            newReceipt.TotalNet = Transaction.Amount;

            newReceipt.Document = CreateDocumentFromTransaction();

            LinkAndSow(newReceipt);
            Show = false;
        }

        private ICommandViewModel _CreateSalesInvoiceCommand = null;
        public ICommandViewModel CreateSalesInvoiceCommand
        {
            get
            {
                if (_CreateSalesInvoiceCommand == null)
                {
                    _CreateSalesInvoiceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Sales invoice", "Creates a sales invoice", CreateSalesInvoice, null, null);
                }
                return _CreateSalesInvoiceCommand;
            }
        }

        public void CreateSalesInvoice()
        {
            var newReceipt = DataContext.Create<SalesInvoice>();
            newReceipt.Customer = Transaction.Party.PartyRole.OfType<Customer>().FirstOrDefault();
            newReceipt.FulfillmentDate = Transaction.Date;

            var newItem = DataContext.Create<SalesInvoiceItem>();
            newItem.Amount = Transaction.Amount;
            newReceipt.Items.Add(newItem);

            newReceipt.Document = CreateDocumentFromTransaction();
        
            LinkAndSow(newReceipt);
            Show = false;
        }

        private StaticFile CreateDocumentFromTransaction()
        {
            var document = DataContext.Create<StaticFile>();
            var stream = new MemoryStream();
            var sw = new StreamWriter(stream);
            sw.Write(Transaction.Comment);
            sw.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            document.Blob = DataContext.Find<Blob>(DataContext.CreateBlob(stream, "Receipt.txt", "text/plain"));
            return document;
        }
        #endregion

        #region Link receipts and transaction
        private void LinkAndSow(Receipt newReceipt)
        {
            ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, newReceipt), true);
            var didTransfered = LinkReceiptsTransaction(new List<Receipt>(new[] { newReceipt }));
            if (!didTransfered)
            {
                ViewModelFactory.ShowMessage("No link was created, please check, if the new receipt is filled out correctly.", "Warning");
            }
        }

        private bool LinkReceiptsTransaction(List<Receipt> receipts)
        {
            var trans = Transaction;
            var party = trans.Party;
            var otherOverpayments = DataContext.GetQuery<Transaction>()
                .Where(t => t.Party == party && !(t == trans))
                .Where(t => t.OverPayment != 0)
                .OrderBy(z => z.Date)
                .ToList();
            // aktuelle Zahlung explizit als letzte einfügen
            otherOverpayments.Insert(0, Transaction);

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
            return didTransfered;
        }

        private Receipt_Transaction Transfer(Transaction payment, Receipt receipt, decimal amount)
        {
            var r_z = DataContext.Create<Receipt_Transaction>();
            r_z.Receipt = receipt;
            r_z.Transaction = payment;
            r_z.Amount = amount;
            return r_z;
        }
        #endregion
    }
}
