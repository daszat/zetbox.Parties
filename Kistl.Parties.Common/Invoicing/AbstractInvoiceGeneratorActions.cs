using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class AbstractInvoiceGeneratorActions
    {
        [Invocation]
        public static void ToString(AbstractInvoiceGenerator obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.IsNullOrEmpty(obj.Name) ? "<no name>" : obj.Name;
        }
    }
}
