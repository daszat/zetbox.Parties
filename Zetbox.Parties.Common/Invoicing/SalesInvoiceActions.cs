using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Parties;
using Zetbox.Parties.Common.Invoicing;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class SalesInvoiceActions
    {
        [Invocation]
        public static void ObjectIsValid(SalesInvoice obj, ObjectIsValidEventArgs e)
        {
            if (obj.InternalOrganization == null)
            {
                e.IsValid = false;
                e.Errors.Add(SalesInvoiceResources.NoOrgSelected);
                return;
            }

            if (obj.InternalOrganization.InvoiceGenerator == null)
            {
                e.IsValid = false;
                e.Errors.Add(SalesInvoiceResources.NoInvoiceGenerator);
                return;
            }
        }

        [Invocation]
        public static void postSet_Items(SalesInvoice obj)
        {
            obj.UpdateTotal();
        }

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
        public static void UpdateTotal(Zetbox.Basic.Invoicing.SalesInvoice obj)
        {
            obj.Total = obj.Items.Sum(i => i.Amount);
            obj.TotalNet = obj.Items.Sum(i => i.AmountNet);
        }

        [Invocation]
        public static void GetPaymentAmount(SalesInvoice obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetIncomePaymentAmount(obj);
        }

        [Invocation]
        public static void FinalizeInvoiceCanExec(SalesInvoice obj, MethodReturnEventArgs<bool> e)
        {
            e.Result = obj.Context.IsModified == false && obj.FinalizedOn.HasValue == false;
        }

        [Invocation]
        public static void FinalizeInvoiceCanExecReason(SalesInvoice obj, MethodReturnEventArgs<string> e)
        {
            if (obj.FinalizedOn.HasValue)
            {
                e.Result = SalesInvoiceResources.IsAlreadyFinalized;
            }
            else if (obj.Context.IsModified)
            {
                e.Result = SalesInvoiceResources.IsNotSaved;
            }
        }

        [Invocation]
        public static void Duplicate(SalesInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<SalesInvoice>();

            result.Description = obj.Description;
            // result.Document = obj.Document; Don't copy document on reuse
            result.InternalOrganization = obj.InternalOrganization;
            result.Message = obj.Message;
            result.Customer = obj.Customer;

            foreach (var it in obj.Items)
            {
                var item = ctx.Create<SalesInvoiceItem>();
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
        public static void CreateTemplate(SalesInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.ReceiptTemplate> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<SalesInvoiceTemplate>();

            // TODO: migrate this: result.DueDate.DaysOffset = (obj.DueDate - obj.Date).TotalDays;
            result.Description = obj.Description;
            // result.Document = obj.Document; Don't copy document on reuse
            result.IntOrg = obj.InternalOrganization;
            result.Message = obj.Message;
            result.Customer = obj.Customer;

            foreach (var it in obj.Items)
            {
                var item = ctx.Create<SalesInvoiceItemTemplate>();
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
        public static void ChangeTypeTo(SalesInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e, ReceiptType newType)
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
                        newObj.Party = obj.Customer != null ? obj.Customer.Party : null;
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
                        newObj.Party = obj.Customer != null ? obj.Customer.Party : null;
                        ctx.Delete(obj);
                        e.Result = newObj;
                        break;
                    }
                case ReceiptType.PurchaseInvoice:
                    {
                        var newObj = ctx.Create<PurchaseInvoice>();
                        ReceiptHeper.CopyCommonData(obj, newObj);
                        ReceiptHeper.MoveTransactions(obj, newObj);
                        newObj.InternalOrganization = obj.InternalOrganization;
                        newObj.Supplier = obj.Customer != null ? obj.Customer.Party.PartyRole.OfType<Supplier>().FirstOrDefault() : null;
                        foreach (var item in obj.Items)
                        {
                            var newItem = ctx.Create<PurchaseInvoiceItem>();
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
                case ReceiptType.SalesInvoice:
                    {
                        e.Result = obj;
                        break;
                    }
                default:
                    break;
            }
        }

        [Invocation]
        public static void Cancel(SalesInvoice obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.SalesInvoice> e)
        {
            var reversal = obj.Context.Create<SalesInvoice>();
            foreach (var item in obj.Items)
            {
                var reversalItem = obj.Context.Create<SalesInvoiceItem>();
                reversalItem.Description = item.Description;
                reversalItem.Quantity = item.Quantity;
                reversalItem.UnitPrice = -item.UnitPrice; // negate price!
                reversalItem.VATType = item.VATType;

                reversal.Items.Add(reversalItem);
            }
            reversal.Customer = obj.Customer;
            reversal.Date = DateTime.Today;
            reversal.Description = string.Format(SalesInvoiceResources.ReversalDescriptionFmt, obj.InvoiceID);
            reversal.DueDate = DateTime.Today;
            // reversal.FulfillmentDate = ...; // should be set to the date when the reversal was actually transferred, or to today if no money has to be transferred
            reversal.InternalOrganization = obj.InternalOrganization;
            reversal.Period = obj.Period;

            obj.Reversal = reversal;
            obj.Status = ReceiptStatus.Canceled;
            obj.FulfillmentDate = null; // Reset FulfillmentDate as the invoice is canceled

            e.Result = reversal;
        }

        [Invocation]
        public static void CancelCanExec(SalesInvoice obj, MethodReturnEventArgs<bool> e)
        {
            e.Result = obj.FinalizedOn.HasValue && obj.CanceledInvoice == null && obj.Reversal == null;
        }

        [Invocation]
        public static void CancelCanExecReason(SalesInvoice obj, MethodReturnEventArgs<string> e)
        {
            if (!obj.FinalizedOn.HasValue)
            {
                e.Result = SalesInvoiceResources.IsNotFinalized;
                return;
            }

            if (obj.CanceledInvoice != null)
            {
                e.Result = SalesInvoiceResources.IsReversal;
                return;
            }

            if (obj.Reversal != null)
            {
                e.Result = SalesInvoiceResources.IsAlreadyCanceled;
                return;
            }
        }
    }
}
