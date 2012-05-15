using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Dtos;
using System.ComponentModel;

namespace Kistl.Parties.Common.Dtos
{
    [Serializable]
    [GuiPrintableRoot]
    [GuiTitle("Sales Quotes Report")]
    public class SalesQuoteReport
    {
        public SalesQuoteReport()
        {
            Issued = new DtoIssued();
            Delivery = new DtoDelivery();
        }

        [Browsable(false)]
        public DateTime From { get; set; }
        [Browsable(false)]
        public DateTime Until { get; set; }

        [GuiTabbed]
        public List<object> Tabs
        {
            get
            {
                return new List<object>() { Issued, Delivery };
            }
        }

        [Browsable(false)]
        public DtoIssued Issued { get; set; }
        [Browsable(false)]
        public DtoDelivery Delivery { get; set; }


        [Serializable]
        [GuiTitle("Issued")]
        public class DtoIssued
        {
            public DtoIssued()
            {
                Details = new DtoDetails();
            }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }

            [GuiTabbed]
            [Serializable]
            public class DtoDetails
            {
                public DtoDetails()
                {
                    SalesQuotesIssuedMonths = new List<SalesQuotesMonth>();
                    SalesQuotesIssuedCustomers = new List<SalesQuotesCustomer>();
                }
                [GuiTitle("Issued / Months")]
                public List<SalesQuotesMonth> SalesQuotesIssuedMonths { get; set; }

                [GuiTitle("Issued / Customer")]
                public List<SalesQuotesCustomer> SalesQuotesIssuedCustomers { get; set; }
            }

            public DtoDetails Details { get; set; }
        }

        [Serializable]
        [GuiTitle("Delivery")]
        public class DtoDelivery
        {
            public DtoDelivery()
            {
                Details = new DtoDetails();
            }

            [GuiTitle]
            public string Title { get; set; }

            public decimal Total { get; set; }
            public decimal TotalCorrected { get; set; }

            [GuiTabbed]
            [Serializable]
            public class DtoDetails
            {
                public DtoDetails()
                {
                    SalesQuotesDeliveryMonths = new List<SalesQuotesMonth>();
                    SalesQuotesDeliveryCustomers = new List<SalesQuotesCustomer>();
                }
                [GuiTitle("Delivery / Months")]
                public List<SalesQuotesMonth> SalesQuotesDeliveryMonths { get; set; }

                [GuiTitle("Delivery / Customer")]
                public List<SalesQuotesCustomer> SalesQuotesDeliveryCustomers { get; set; }
            }

            public DtoDetails Details { get; set; }
        }
    }

    [Serializable]
    public class SalesQuotesMonth
    {
        public string Year { get; set; }
        public string Month { get; set; }

        public decimal Total { get; set; }
        public decimal TotalCorrected { get; set; }
    }

    [Serializable]
    public class SalesQuotesCustomer
    {
        public string Customer { get; set; }

        public decimal Total { get; set; }
        public decimal TotalCorrected { get; set; }
    }
}
