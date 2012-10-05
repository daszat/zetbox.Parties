using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class SalesInvoiceTemplateActions
    {
        [Invocation]
        public static void CreateReceipt(SalesInvoiceTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<SalesInvoice>();

            result.Date = DateTime.Today;
            result.Description = obj.Description;
            result.Document = obj.Document;
            result.DueDate = obj.DueDate.GetCurrent(result.Date);
            result.InternalOrganization = obj.IntOrg;
            result.Message = obj.Message;
            result.Customer = obj.Customer;
            result.Period = obj.Period;

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
    }
}
