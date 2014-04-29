using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Products
{
    [Implementor]
    public static class OrderItemActions
    {
        [Invocation]
        public static void ToString(OrderItem obj, MethodReturnEventArgs<System.String> e)
        {

        }
        
        [Invocation]
        public static void ObjectIsValid(OrderItem obj, ObjectIsValidEventArgs e)
        {
        }

        [Invocation]
        public static void postSet_Product(OrderItem obj, PropertyPostSetterEventArgs<Zetbox.Basic.Products.Product> e)
        {
        }
    }
}
