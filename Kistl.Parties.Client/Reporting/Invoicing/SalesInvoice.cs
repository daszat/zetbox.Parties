
namespace Kistl.Parties.Client.Reporting.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ZBox.Basic.Invoicing;

    public partial class SalesInvoice
    {
        protected virtual void PageSetup()
        {
            Common.PageSetup.Call(Host, "Portrait");
        }

        protected virtual string GetTitle()
        {
            return "Invoice";
        }

        protected virtual string GetToText()
        {
            return "To";
        }

        protected virtual void FormatRecipient()
        {
            Common.Address.Call(Host, invoice.Customer.Party);
        }

        protected virtual void FormatRecipientTaxNumber()
        {
            if (invoice.Customer.Party is ZBox.Basic.Parties.Organization)
            {
                var org = (ZBox.Basic.Parties.Organization)invoice.Customer.Party;
                this.WriteObjects("UID: ", org.TaxIDNumber);
            }
        }

        protected virtual void FormatIntOrg()
        {
            Common.Address.Call(Host, invoice.InternalOrganization.Party);
        }

        protected virtual void FormatIntOrgTaxNumber()
        {
            var org = (ZBox.Basic.Parties.Organization)invoice.InternalOrganization.Party;
            this.WriteObjects("UID: ", Format(org.TaxIDNumber));
        }

        protected virtual string GetSubject()
        {
            return Format(string.Format("Subject: Invoice {0}", invoice.InvoiceID));
        }

        protected virtual string GetCityAndDate()
        {
            return string.Format("Vienna, {0}", FormatDate(invoice.Date));
        }

        protected virtual string GetServicesHeading()
        {
            return "Services";
        }

        protected virtual string GetPeriod()
        {
            return Format(string.Format("For the period {0}", invoice.Period));
        }

        protected virtual IEnumerable<SalesInvoiceItem> GetItems()
        {
            return invoice.Items;
        }
        protected virtual bool RenderSubTotal()
        {
            return invoice.Items.Count > 1;
        }

        protected virtual string GetSubTotalDescription() 
        { 
            return "Sub total net"; 
        }
        protected virtual string GetSubTotalAmountNet() 
        { 
            return FormatEuro(invoice.Items.Sum(i => i.AmountNet)); 
        }

        protected virtual IEnumerable<VATType> GetVATTypes()
        {
            return invoice.Items.Select(i => i.VATType).Distinct().OrderBy(i => i.Description);
        }

        protected virtual string GetVATDescription(VATType vat)
        {
            return Format(string.Format("VAT {0}", vat.Description));
        }

        protected virtual string GetVATSum(VATType vat)
        {
            return FormatEuro(invoice.Items.Where(i => i.VATType == vat).Sum(i => i.Amount - i.AmountNet));
        }

        protected virtual string GetTotalDescription()
        {
            return "Total";
        }
        protected virtual string GetTotalAmount()
        {
            return FormatEuro(invoice.Items.Sum(i => i.Amount));
        }

        protected virtual void FormatMessage()
        {
            if (!string.IsNullOrEmpty(invoice.Message))
            {
                this.WriteLine("\\paragraph {");
                this.WriteLine(invoice.Message);
                this.WriteLine("}");
            }
        }
    }
}
