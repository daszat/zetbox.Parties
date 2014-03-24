namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Basic.Parties;

    [Implementor]
    public static class AccountStatementActions
    {
        [Invocation]
        public static void ToString(AccountStatement obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} - {1} ({2})", obj.Number, obj.Account, obj.Date);
        }
    }
}
