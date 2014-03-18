namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;
    using Zetbox.App.Base;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Client.Presentables;
    using Zetbox.Basic.Parties;
    using Zetbox.Basic.Accounting;

    [Implementor]
    public class ImportedFileActions
    {
        private static IViewModelFactory _factory;
        private static IFrozenContext _frozenCtx;
        public ImportedFileActions(IViewModelFactory factory, IFrozenContext frozenCtx)
        {
            _factory = factory;
            _frozenCtx = frozenCtx;
        }

        [Invocation]
        public static void MakeInvoice(ImportedFile obj, MethodReturnEventArgs<PurchaseInvoice> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(PurchaseInvoice).GetObjectClass(_frozenCtx), null, (sel) =>
            {
                if (sel != null)
                {
                    var invoice = (PurchaseInvoice)sel.First().Object;
                    invoice.Document = obj.MakeReadonlyFile();
                    invoice.Document.AttachedTo.SetObject(invoice);
                    e.Result = invoice;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void MakeOtherExpense(ImportedFile obj, MethodReturnEventArgs<OtherExpenseReceipt> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(OtherExpenseReceipt).GetObjectClass(_frozenCtx), null, (sel) =>
            {
                if (sel != null)
                {
                    var receipt = (OtherExpenseReceipt)sel.First().Object;
                    receipt.Document = obj.MakeReadonlyFile();
                    receipt.Document.AttachedTo.SetObject(receipt);
                    e.Result = receipt;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void MakeQuote(ImportedFile obj, MethodReturnEventArgs<PurchaseQuote> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(PurchaseQuote).GetObjectClass(_frozenCtx), null, (sel) =>
            {
                if (sel != null)
                {
                    var quote = (PurchaseQuote)sel.First().Object;
                    quote.Document = obj.MakeFile();
                    quote.Document.AttachedTo.SetObject(quote);
                    e.Result = quote;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void AddToParty(ImportedFile obj, MethodReturnEventArgs<at.dasz.DocumentManagement.File> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(Party).GetObjectClass(_frozenCtx), null, (sel) =>
            {
                if (sel != null)
                {
                    var party = (Party)sel.First().Object;
                    var file = obj.MakeFile();
                    file.AttachedTo.SetObject(party);
                    party.Files.Add(file);
                    e.Result = file;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void MakeAccountStatement(ImportedFile obj, MethodReturnEventArgs<AccountStatement> e)
        {
            var ctx = obj.Context;
            var stmt = ctx.Create<AccountStatement>();
            stmt.File = obj.MakeReadonlyFile();
            stmt.File.AttachedTo.SetObject(stmt);
            e.Result = stmt;
        }
    }
}