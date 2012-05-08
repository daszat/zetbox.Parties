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
            e.Result = string.Format("{0}, {1}, Total {2}/{3}", obj.Description, obj.Period, obj.TotalNet, obj.Total);
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
            e.Result = obj.GetOpenAmount();
        }

        [Invocation]
        public static void get_PaymentAmount(Receipt obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.GetPaymentAmount();
        }

        private static void UpdateCalculatedProperties(Receipt obj)
        {
            obj.Recalculate("OpenAmount");
            obj.Recalculate("PaymentAmount");
        }  
    }

    public static class ReceiptAmountCalculator
    {
        public static decimal GetIncomeOpenAmount(Receipt obj)
        {
            return obj.Total - obj.Transactions.Sum(i => i.Amount);
        }

        public static decimal GetExpenseOpenAmount(Receipt obj)
        {
            return obj.Total - obj.Transactions.Sum(i => -i.Amount);
        }

        public static decimal GetIncomePaymentAmount(Receipt obj)
        {
            return obj.Transactions.Sum(i => i.Amount);
        }

        public static decimal GetExpensePaymentAmount(Receipt obj)
        {
            return obj.Transactions.Sum(i => -i.Amount);
        }
    }
}
