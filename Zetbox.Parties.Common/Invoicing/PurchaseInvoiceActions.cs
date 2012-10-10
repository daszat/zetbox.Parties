using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
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

        [Invocation]
        public static void Duplicate(PurchaseInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<PurchaseInvoice>();

            result.Description = obj.Description;
            result.Document = obj.Document;
            result.InternalOrganization = obj.InternalOrganization;
            result.Message = obj.Message;
            result.Supplier = obj.Supplier;

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
        public static void CreateTemplate(PurchaseInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<PurchaseInvoiceTemplate>();

            result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
            result.Description = obj.Description;
            result.Document = obj.Document;
            result.IntOrg = obj.InternalOrganization;
            result.Message = obj.Message;
            result.Supplier = obj.Supplier;

            foreach (var it in obj.Items)
            {
                var item = ctx.Create<PurchaseInvoiceItemTemplate>();
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
    }
}
