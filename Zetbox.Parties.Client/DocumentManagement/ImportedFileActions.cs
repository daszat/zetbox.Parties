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

    [Implementor]
    public class ImportedFileActions
    {
        private static IViewModelFactory _factory;
        public ImportedFileActions(IViewModelFactory factory)
        {
            _factory = factory;
        }

        [Invocation]
        public static void MakeInvoice(ImportedFile obj, MethodReturnEventArgs<PurchaseInvoice> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(PurchaseInvoice).GetObjectClass(obj.ReadOnlyContext), null, (sel) =>
            {
                if (sel != null)
                {
                    var invoice = (PurchaseInvoice)sel.First().Object;
                    invoice.Document = obj.MakeStaticFile();
                    e.Result = invoice;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void MakeOtherExpense(ImportedFile obj, MethodReturnEventArgs<OtherExpenseReceipt> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(OtherExpenseReceipt).GetObjectClass(obj.ReadOnlyContext), null, (sel) =>
            {
                if (sel != null)
                {
                    var receipt = (OtherExpenseReceipt)sel.First().Object;
                    receipt.Document = obj.MakeStaticFile();
                    e.Result = receipt;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }

        [Invocation]
        public static void MakeQuote(ImportedFile obj, MethodReturnEventArgs<PurchaseQuote> e)
        {
            var ctx = obj.Context;
            var dlg = _factory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(ctx, null, typeof(PurchaseQuote).GetObjectClass(obj.ReadOnlyContext), null, (sel) =>
            {
                if (sel != null)
                {
                    var quote = (PurchaseQuote)sel.First().Object;
                    quote.Document = obj.MakeDocument();
                    e.Result = quote;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }
    }
}