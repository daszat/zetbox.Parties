using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class SalesInvoiceActions
    {
        [Invocation]
        public static void CreateItem(SalesInvoice obj, MethodReturnEventArgs<InvoiceItem> e, decimal quantity, decimal amount, string description, bool taxable)
        {
            var item = obj.Context.Create<SalesInvoiceItem>();

            item.Quantity = quantity;
            item.Amount = amount;
            item.Description = description;

            obj.Items.Add(item);
            e.Result = item;
        }

        [Invocation]
        public static void get_Total(ZBox.Basic.Invoicing.SalesInvoice obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Items.Sum(i => i.Amount);
        }

        [Invocation]
        public static void get_TotalNet(ZBox.Basic.Invoicing.SalesInvoice obj, PropertyGetterEventArgs<decimal> e)
        {
            e.Result = obj.Items.Sum(i => i.AmountNet);
        }
    }
}
