using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Accounting
{
    [Implementor]
    public static class CategoryActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Accounting.Category obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }
    }
}
