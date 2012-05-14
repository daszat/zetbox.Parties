
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
            return SalesInvoiceResources.Title;
        }

        protected virtual string GetToText()
        {
            return SalesInvoiceResources.To;
        }

        protected virtual void FormatRecipient()
        {
            Common.Address.Call(Host, invoice.Customer.Party, Coalesce(invoice.Customer.Party.InvoiceAddress, invoice.Customer.Party.Address));
        }

        protected virtual void FormatRecipientTaxNumber()
        {
            if (invoice.Customer.Party is ZBox.Basic.Parties.Organization)
            {
                var org = (ZBox.Basic.Parties.Organization)invoice.Customer.Party;
                this.WriteObjects(string.Format(SalesInvoiceResources.UID, org.TaxIDNumber));
            }
        }

        protected virtual void FormatIntOrg()
        {
            Common.Address.Call(Host, invoice.InternalOrganization.Party, invoice.InternalOrganization.Party.Address);
        }

        protected virtual void FormatIntOrgTaxNumber()
        {
            var org = (ZBox.Basic.Parties.Organization)invoice.InternalOrganization.Party;
            this.WriteObjects(string.Format(SalesInvoiceResources.UID, org.TaxIDNumber));
        }

        protected virtual string GetSubject()
        {
            return Format(string.Format(SalesInvoiceResources.Subject, invoice.FinalizedOn.HasValue ? invoice.InvoiceID : "----/--"));
        }

        protected virtual string GetCityAndDate()
        {
            return string.Format(SalesInvoiceResources.CityAndDate, invoice.InternalOrganization.Party.Address.City, FormatDate(invoice.Date));
        }

        protected virtual string GetServicesHeading()
        {
            return SalesInvoiceResources.ServicesHeading;
        }

        protected virtual string GetPeriod()
        {
            return Format(string.Format(SalesInvoiceResources.Period, invoice.Period));
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
            return SalesInvoiceResources.SubTotalDescription;
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
            return Format(string.Format(SalesInvoiceResources.VATDescription, vat.Description));
        }

        protected virtual string GetVATSum(VATType vat)
        {
            return FormatEuro(invoice.Items.Where(i => i.VATType == vat).Sum(i => i.Amount - i.AmountNet));
        }

        protected virtual string GetTotalDescription()
        {
            return SalesInvoiceResources.TotalDescription;
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

        protected virtual string GetPaymentTitle()
        {
            return SalesInvoiceResources.PaymentTitle;
        }

        protected virtual string GetPaymentIntroduction()
        {
            return string.Format(SalesInvoiceResources.PaymentIntroduction, FormatDate(invoice.DueDate));
        }

        protected virtual void FormatBankAccount()
        {
            Common.BankAccount.Call(Host, invoice.InternalOrganization.Party.BankAccount);
        }

        protected virtual string GetGreetingsLine()
        {
            return SalesInvoiceResources.GreetingsLine;
        }

        protected virtual void FormatSignature()
        {
            if (invoice.Issuer != null)
            {
                Common.Signature.Call(Host, invoice.Issuer);
            }
        }

        protected virtual string GetSubjectHeader() { return SalesInvoiceResources.SubjectHeader; }
        protected virtual string GetQuantityHeader() { return SalesInvoiceResources.QuantityHeader; }
        protected virtual string GetUnitPriceHeader() { return SalesInvoiceResources.UnitPriceHeader; }
        protected virtual string GetVATHeader() { return SalesInvoiceResources.VATHeader; }
        protected virtual string GetAmountHeader() { return SalesInvoiceResources.AmountHeader; }
    }
}
