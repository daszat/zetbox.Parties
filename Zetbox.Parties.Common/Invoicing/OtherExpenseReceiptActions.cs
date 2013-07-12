using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Parties;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class OtherExpenseReceiptActions
    {
        [Invocation]
        public static void GetPaymentAmount(OtherExpenseReceipt obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetExpensePaymentAmount(obj);
        }

        [Invocation]
        public static void Duplicate(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherExpenseReceipt>();

            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }

        [Invocation]
        public static void CreateTemplate(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<OtherExpenseReceiptTemplate>();

            // TODO: migrate this: result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
            result.Description = obj.Description;
            result.IntOrg = obj.IntOrg;
            result.Message = obj.Message;
            result.Party = obj.Party;
            result.Total = obj.Total;
            result.TotalNet = obj.TotalNet;

            e.Result = result;
        }

        [Invocation]
        public static void ChangeTypeTo(OtherExpenseReceipt obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e, ReceiptType newType)
        {
            var ctx = obj.Context;
            switch (newType)
            {
                case ReceiptType.OtherExpenseReceipt:
                    {
                        e.Result = obj;
                        break;
                    }
                case ReceiptType.OtherIncomeReceipt:
                    {
                        var newObj = ctx.Create<OtherIncomeReceipt>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.IntOrg = obj.IntOrg;
                        newObj.Party = obj.Party;
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                case ReceiptType.PurchaseInvoice:
                    {
                        var newObj = ctx.Create<PurchaseInvoice>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.InternalOrganization = obj.IntOrg;
                        newObj.Supplier = obj.Party.PartyRole.OfType<Supplier>().FirstOrDefault();
                        var newItem = ctx.Create<PurchaseInvoiceItem>();
                        newItem.Quantity = 1;
                        newItem.UnitPrice = obj.Total;
                        newItem.Amount = obj.Total;
                        newItem.Description = obj.Description;
                        newObj.Items.Add(newItem);
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                case ReceiptType.SalesInvoice:
                    {
                        var newObj = ctx.Create<SalesInvoice>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.InternalOrganization = obj.IntOrg;
                        newObj.Customer = obj.Party.PartyRole.OfType<Customer>().FirstOrDefault();
                        var newItem = ctx.Create<SalesInvoiceItem>();
                        newItem.Quantity = 1;
                        newItem.UnitPrice = obj.Total;
                        newItem.Amount = obj.Total;
                        newItem.Description = obj.Description;
                        newObj.Items.Add(newItem);
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
