using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class PurchaseInvoiceActions
    {
        [Invocation]
        public static void postSet_Items(PurchaseInvoice obj)
        {
            obj.UpdateTotal();
        }

        [Invocation]
        public static void CreateItem(PurchaseInvoice obj, MethodReturnEventArgs<InvoiceItem> e, decimal quantity, decimal amount, string description, bool taxable)
        {
            var item = obj.Context.Create<PurchaseInvoiceItem>();

            item.Quantity = quantity;
            item.Amount = amount;
            item.Description = description;

            obj.Items.Add(item);
            e.Result = item;
        }

        [Invocation]
        public static void UpdateTotal(PurchaseInvoice obj)
        {
            obj.Total = obj.Items.Sum(i => i.Amount);
            obj.TotalNet = obj.Items.Sum(i => i.AmountNet);
        }

        [Invocation]
        public static void GetOpenAmount(PurchaseInvoice obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetExpenseOpenAmount(obj);
        }

        [Invocation]
        public static void GetPaymentAmount(PurchaseInvoice obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetExpensePaymentAmount(obj);
        }
    }
}
