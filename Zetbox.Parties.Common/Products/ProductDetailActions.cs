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
        public static void ToString(Zetbox.Basic.Products.ProductDetail obj, MethodReturnEventArgs<System.String> e)
        {

        }
    }
}
