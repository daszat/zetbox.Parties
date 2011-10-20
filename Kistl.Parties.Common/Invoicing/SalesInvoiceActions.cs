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
            item.Taxable = taxable;

            obj.Items.Add(item);
            e.Result = item;
        }
    }
}
