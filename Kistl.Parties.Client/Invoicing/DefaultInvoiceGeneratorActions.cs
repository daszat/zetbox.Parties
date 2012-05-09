using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using at.dasz.DocumentManagement;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class DefaultInvoiceGeneratorActions
    {
        [Invocation]
        public static void CreateDocument(DefaultInvoiceGenerator obj, MethodReturnEventArgs<StaticFile> e, SalesInvoice invoice)
        {
        }
    }
}
