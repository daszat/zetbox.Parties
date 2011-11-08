using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Accounting
{
    [Implementor]
    public static class TransactionActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Accounting.Transaction obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0:d}, {1:n2} - {2}", obj.Date, obj.Amount, "no category");
        }
    }
}
