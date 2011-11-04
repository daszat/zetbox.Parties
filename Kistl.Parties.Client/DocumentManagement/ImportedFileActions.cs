namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Extensions;
    using Kistl.App.Base;
    using ZBox.Basic.Invoicing;
    using Kistl.Client.Presentables;

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
                    var invoice = (PurchaseInvoice)sel.Object;
                    invoice.Document = obj.MakeStaticFile();
                    e.Result = invoice;
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
                    var quote = (PurchaseQuote)sel.Object;
                    quote.Document = obj.MakeDocument();
                    e.Result = quote;
                }
            }, null);
            _factory.ShowDialog(dlg);
        }
    }
}