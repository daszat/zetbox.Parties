namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Autofac;
    using Kistl.Parties.Common.Accounting;
    using Kistl.Client.Presentables;

    [Implementor]
    public class Receipt_TransactionActions
    {
        [Invocation]
        public static void NotifyDeleting(Receipt_Transaction obj)
        {
            // Workaround - remove from lists
            obj.Receipt = null;
            obj.Transaction = null;
        }
    }
}
