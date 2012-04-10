using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class ReceiptActions
    {
        [Invocation]
        public static void ToString(Receipt obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}, Total {1}/{2}", obj.Description, obj.TotalNet, obj.Total);
        }

        [Invocation]
        public static void UpdateTotal(Receipt obj)
        {
            // Do nothing, work is done in derived classes
        }

        [Invocation]
        public static void postSet_Transactions(Receipt obj)
        {
            UpdateCalculatedProperties(obj);
        }

        [Invocation]
        public static void postSet_Total(Receipt obj, PropertyPostSetterEventArgs<decimal> e)
        {
            UpdateCalculatedProperties(obj);
        }

        [Invocation]
        public static void get_OpenAmount(Receipt obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Total - obj.PaymentAmount;
        }

        [Invocation]
        public static void get_PaymentAmount(Receipt obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Transactions.Sum(i => i.Amount);
        }

        private static void UpdateCalculatedProperties(Receipt obj)
        {
            obj.Recalculate("OpenAmount");
            obj.Recalculate("PaymentAmount");
        }  
    }
}
