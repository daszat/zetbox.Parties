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
            e.Result = string.Format("{0} - {1} ({2})", obj.OrderID, obj.Customer, obj.TotalNet);
        }

        [Invocation]
        public static void get_Total(Order obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.OrderItems.Sum(i => i.Price ?? i.Quantity ?? 1 * (i.ProductDetail != null ? i.ProductDetail.Price : 0));
        }

        [Invocation]
        public static void get_TotalNet(Order obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.OrderItems.Sum(i => i.PriceNet ?? i.Quantity ?? 1 * (i.ProductDetail != null ? i.ProductDetail.PriceNet : 0));
        }

        [Invocation]
        public static void postSet_IsDeactivated(Order obj, PropertyPostSetterEventArgs<bool> e)
        {
            obj.OrderItems.ForEach(i => i.IsDeactivated = obj.IsDeactivated);
        }

        [Invocation]
        public static void postSet_OrderItems(Order obj)
        {
            obj.Recalculate("Total");
            obj.Recalculate("TotalNet");
        }
    }
}
