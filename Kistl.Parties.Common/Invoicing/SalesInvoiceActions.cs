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
        public static void ObjectIsValid(SalesInvoice obj, ObjectIsValidEventArgs e)
        {
            if (obj.InternalOrganization == null)
            {
                e.IsValid = false;
                e.Errors.Add("No Internal Organization was selected");
                return;
            }

            if (obj.InternalOrganization.InvoiceGenerator == null)
            {
                e.IsValid = false;
                e.Errors.Add("Internal Organization has no invoce generator. Unable to create invoice id");
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
        public static void UpdateTotal(ZBox.Basic.Invoicing.SalesInvoice obj)
        {
            obj.Total = obj.Items.Sum(i => i.Amount);
            obj.TotalNet = obj.Items.Sum(i => i.AmountNet);
        }

        [Invocation]
        public static void GetOpenAmount(SalesInvoice obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetIncomeOpenAmount(obj);
        }

        [Invocation]
        public static void GetPaymentAmount(SalesInvoice obj, MethodReturnEventArgs<decimal> e)
        {
            e.Result = ReceiptAmountCalculator.GetIncomePaymentAmount(obj);
        }
    }
}
