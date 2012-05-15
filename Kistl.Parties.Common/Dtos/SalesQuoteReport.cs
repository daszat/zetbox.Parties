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
            SalesQuotesMonths = new List<SalesQuotesMonth>();
        }

        [Browsable(false)]
        public DateTime From { get; set; }
        [Browsable(false)]
        public DateTime Until { get; set; }

        public decimal Total { get; set; }
        public decimal TotalCorrected { get; set; }

        [GuiTitle("Months")]
        public List<SalesQuotesMonth> SalesQuotesMonths { get; set; }
    }

    [Serializable]
    public class SalesQuotesMonth
    {
        public string Year { get; set; }
        public string Month { get; set; }

        public decimal Total { get; set; }
        public decimal TotalCorrected { get; set; }
    }
}
