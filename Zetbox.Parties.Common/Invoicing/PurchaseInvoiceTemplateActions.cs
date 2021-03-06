using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class PurchaseInvoiceTemplateActions
    {
        [Invocation]
        public static void CreateReceipt(PurchaseInvoiceTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<PurchaseInvoice>();

            result.Date = DateTime.Today;
            result.Description = obj.Description;
            result.Document = obj.Document;
            result.DueDate = obj.DueDate.GetRelative(result.Date);
            result.InternalOrganization = obj.IntOrg;
            result.Message = obj.Message;
            result.Supplier = obj.Supplier;
            result.Period = obj.Period;

            foreach (var it in obj.Items)
            {
                var item = ctx.Create<PurchaseInvoiceItem>();
                item.Amount = it.Amount;
                item.AmountNet = it.AmountNet;
                item.Description = it.Description;
                item.Quantity = it.Quantity;
                item.UnitPrice = it.UnitPrice;
                item.VATType = it.VATType;
                result.Items.Add(item);
            }

            e.Result = result;
        }

        [Invocation]
        public static void postSet_Items(PurchaseInvoiceTemplate obj)
        {
            obj.UpdateTotal();
        }

        [Invocation]
        public static void UpdateTotal(PurchaseInvoiceTemplate obj)
        {
            obj.Total = obj.Items.Sum(i => i.Amount);
            obj.TotalNet = obj.Items.Sum(i => i.AmountNet);
        }
    }
}
