using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Products
{
    [Implementor]
    public static class OrderActions
    {
        [Invocation]
        public static void ToString(Order obj, MethodReturnEventArgs<System.String> e)
        {
        }

        [Invocation]
        public static void get_Total(Order obj, PropertyGetterEventArgs<decimal> e)
        {
        }

        [Invocation]
        public static void get_TotalNet(Order obj, PropertyGetterEventArgs<decimal> e)
        {
        }

        [Invocation]
        public static void postSet_IsDeactivated(Order obj, PropertyPostSetterEventArgs<bool> e)
        {
        }
    }
}
