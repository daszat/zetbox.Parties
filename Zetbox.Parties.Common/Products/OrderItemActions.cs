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
            e.Result = string.Format("{0} {1}", obj.Quantity ?? 1, obj.Product);
        }
        
        [Invocation]
        public static void ObjectIsValid(OrderItem obj, ObjectIsValidEventArgs e)
        {
            if (obj.ProductDetail == null)
            {
                e.Errors.Add("Unable to find a current product detail.");
            }
        }

        [Invocation]
        public static void postSet_Product(OrderItem obj, PropertyPostSetterEventArgs<Zetbox.Basic.Products.Product> e)
        {
            obj.ProductDetail = obj.Product != null ? obj.Product.CurrentDetail : null;
            UpdateOrder(obj);
        }

        [Invocation]
        public static void postSet_PriceNet(OrderItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            UpdateOrder(obj);
        }

        [Invocation]
        public static void postSet_Price(OrderItem obj, PropertyPostSetterEventArgs<decimal> e)
        {
            UpdateOrder(obj);
        }

        private static void UpdateOrder(OrderItem obj)
        {
            if (obj.Order != null)
            {
                obj.Order.Recalculate("Total");
                obj.Order.Recalculate("TotalNet");
            }
        }
    }
}
