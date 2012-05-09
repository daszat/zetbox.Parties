
namespace Kistl.Parties.Client.Reporting.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class SalesInvoice
    {
        protected virtual void PageSetup()
        {
            Common.PageSetup.Call(Host, "Portrait");
        }

        protected virtual string GetStyleTitle()
        {
            return "Style = \"Title\"";
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
            if (invoice.Customer is ZBox.Basic.Parties.Organization)
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
            this.WriteObjects("UID: ", org.TaxIDNumber);
        }
    }
}
