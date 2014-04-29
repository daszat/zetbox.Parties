using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Products
{
    [Implementor]
    public static class ProductActions
    {
        [Invocation]
        public static void ToString(Product obj, MethodReturnEventArgs<System.String> e)
        {
            var endMessage = string.Empty;
            if (obj.AvailableUntil.HasValue)
            {
                if (obj.AvailableUntil > DateTime.Now)
                {
                    endMessage = string.Format(" until {0:d}", obj.AvailableUntil.Value);
                }
                else
                {
                    endMessage = string.Format("; expired on {0:d}", obj.AvailableUntil.Value);
                }
            }
            e.Result = string.Format("{0} ({1} since {2:d}{3})", obj.Name, obj.BillingPeriod, obj.AvailableFrom, endMessage);
        }

        [Invocation]
        public static void ObjectIsValid(Product obj, ObjectIsValidEventArgs e)
        {
            
        }

        [Invocation]
        public static void get_CurrentDetail(Product obj, PropertyGetterEventArgs<Zetbox.Basic.Products.ProductDetail> e)
        {
        }

        [Invocation]
        public static void postSet_IsDeactivated(Product obj, PropertyPostSetterEventArgs<bool> e)
        {
        }
    }
}
