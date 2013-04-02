using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Parties;

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

            // TODO: migrate this: result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
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

        [Invocation]
        public static void ChangeTypeTo(PurchaseInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e, ReceiptType newType)
        {
            var ctx = obj.Context;
            switch (newType)
            {
                case ReceiptType.OtherExpenseReceipt:
                    {
                        var newObj = ctx.Create<OtherExpenseReceipt>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.IntOrg = obj.InternalOrganization;
                        newObj.Party = obj.Supplier != null ? obj.Supplier.Party : null;
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                case ReceiptType.OtherIncomeReceipt:
                    {
                        var newObj = ctx.Create<OtherIncomeReceipt>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.IntOrg = obj.InternalOrganization;
                        newObj.Party = obj.Supplier != null ? obj.Supplier.Party : null;
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                case ReceiptType.PurchaseInvoice:
                    {
                        e.Result = obj;
                        break;
                    }
                case ReceiptType.SalesInvoice:
                    {
                        var newObj = ctx.Create<SalesInvoice>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.InternalOrganization = obj.InternalOrganization;
                        newObj.Customer = obj.Supplier != null ? obj.Supplier.Party.PartyRole.OfType<Customer>().FirstOrDefault() : null;
                        foreach (var item in obj.Items)
                        {
                            var newItem = ctx.Create<SalesInvoiceItem>();
                            newItem.Quantity = item.Quantity;
                            newItem.UnitPrice = item.UnitPrice;
                            newItem.AmountNet = item.AmountNet;
                            newItem.VATType = item.VATType;
                            newItem.Amount = item.Amount;
                            newItem.Description = item.Description;
                            newObj.Items.Add(newItem);
                        }
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
