namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class TransactionActions
    {
        [Invocation]
        public static void ToString(Transaction obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0:d}, {1:n2} - {2}", obj.Date, obj.Amount, "no category");
        }
    }
}
