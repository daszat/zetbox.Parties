using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class AbstractInvoiceGeneratorActions
    {
        [Invocation]
        public static void ToString(AbstractInvoiceGenerator obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name.IfNullOrWhiteSpace("<no name>");
        }
    }
}
