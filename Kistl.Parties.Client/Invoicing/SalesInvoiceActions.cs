using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.Client.Presentables;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public class SalesInvoiceActions
    {
        private static IViewModelFactory _factory;

        public SalesInvoiceActions(IViewModelFactory factory)
        {
            _factory = factory;
        }

        [Invocation]
        public static void CreateInvoiceDocument(SalesInvoice obj)
        {
            _factory.ShowMessage("TODO: Create sales invoice document", "TODO");
        }
    }
}
