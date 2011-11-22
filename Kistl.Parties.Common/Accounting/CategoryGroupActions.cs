namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CategoryGroupActions
    {
        [Invocation]
        public static void ToString(CategoryGroup obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }
    }
}
