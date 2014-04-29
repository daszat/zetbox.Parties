using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Products
{
    [Implementor]
    public static class ProductDetailActions
    {
        [Invocation]
        public static void postSet_Quantity(ProductDetail obj, PropertyPostSetterEventArgs<decimal> e)
        {
            obj.PriceNet = Math.Round(obj.Quantity * obj.UnitPriceNet, 2);
        }

        [Invocation]
        public static void postSet_UnitPriceNet(ProductDetail obj, PropertyPostSetterEventArgs<decimal> e)
        {
            obj.PriceNet = Math.Round(obj.Quantity * obj.UnitPriceNet, 2);
        }

        [Invocation]
        public static void postSet_PriceNet(ProductDetail obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj.VAT != null)
            {
                var percent = (obj.VAT.Percentage ?? 0m) / 100m;
                obj.Price = Math.Round(obj.PriceNet + (obj.PriceNet * percent) + (obj.VAT.Absolute ?? 0m), 2);
            }
        }

        [Invocation]
        public static void postSet_VAT(ProductDetail obj, PropertyPostSetterEventArgs<Zetbox.Basic.Invoicing.VATType> e)
        {
            if (obj.VAT != null)
            {
                var percent = (obj.VAT.Percentage ?? 0m) / 100m;
                obj.Price = Math.Round(obj.PriceNet + (obj.PriceNet * percent) + (obj.VAT.Absolute ?? 0m), 2);
            }
        }

        [Invocation]
        public static void postSet_Price(ProductDetail obj, PropertyPostSetterEventArgs<decimal> e)
        {
        }
    }
}
