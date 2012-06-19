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
    [GuiTitle("Sales Invoice Report")]
    public class SalesInvoiceReport
    {
        public SalesInvoiceReport()
        {
            Date = new DtoDate();
            Fulfillment = new DtoFulfillment();
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
                return new List<object>() { Date, Fulfillment };
            }
        }

        [Browsable(false)]
        public DtoDate Date { get; set; }
        [Browsable(false)]
        public DtoFulfillment Fulfillment { get; set; }

        [Serializable]
        [GuiTitle("Date")]
        public class DtoDate
        {
            public DtoDate()
            {
                Details = new DtoDetails();
            }

            public decimal Total { get; set; }
            public decimal TotalNet { get; set; }

            [GuiTabbed]
            [Serializable]
            public class DtoDetails
            {
                public DtoDetails()
                {
                    Months = new List<InvoiceMonth>();
                    Customers = new List<InvoiceCustomer>();
                }

                [GuiTitle("Invoices / Month")]
                public List<InvoiceMonth> Months { get; set; }

                [GuiTitle("Invoices / Customer")]
                public List<InvoiceCustomer> Customers { get; set; }

                [Serializable]
                public class InvoiceMonth
                {
                    public string Year { get; set; }
                    public string Month { get; set; }

                    public decimal Total { get; set; }
                    public decimal TotalNet { get; set; }
                }

                [Serializable]
                public class InvoiceCustomer
                {
                    public string Customer { get; set; }

                    public decimal Total { get; set; }
                    public decimal TotalNet { get; set; }
                }
            }

            public DtoDetails Details { get; set; }
        }

        [Serializable]
        [GuiTitle("Fulfillment")]
        public class DtoFulfillment
        {
            public DtoFulfillment()
            {
                Details = new DtoDetails();
            }

            public decimal Total { get; set; }
            public decimal TotalNet { get; set; }
            public int AvgDuration { get; set; }

            [GuiTabbed]
            [Serializable]
            public class DtoDetails
            {
                public DtoDetails()
                {
                    Months = new List<FulfillmentMonth>();
                    Customers = new List<FulfillmentCustomer>();
                }

                [GuiTitle("Fulfillment / Month")]
                public List<FulfillmentMonth> Months { get; set; }

                [GuiTitle("Fulfillment / Customer")]
                public List<FulfillmentCustomer> Customers { get; set; }

                [Serializable]
                public class FulfillmentMonth
                {
                    public string Year { get; set; }
                    public string Month { get; set; }

                    public decimal Total { get; set; }
                    public decimal TotalNet { get; set; }
                }

                [Serializable]
                public class FulfillmentCustomer
                {
                    public string Customer { get; set; }
                    public int AvgDuration { get; set; }

                    public decimal Total { get; set; }
                    public decimal TotalNet { get; set; }
                }
            }

            public DtoDetails Details { get; set; }
        }
    }
}
