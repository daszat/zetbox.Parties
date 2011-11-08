using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Accounting
{
    [Implementor]
    public static class AccountActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Accounting.Account obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }
    }
}
