using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class VATTypeActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Invoicing.VATType obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;
        }
    }
}
