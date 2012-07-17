namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class AccountActions
    {
        [Invocation]
        public static void ToString(Account obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }
    }
}
