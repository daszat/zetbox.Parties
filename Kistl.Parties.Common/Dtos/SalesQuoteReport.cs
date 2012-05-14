using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Parties.Common.Dtos
{
    [Serializable]
    public class SalesQuoteReport
    {
        public SalesQuoteReport()
        {
            SalesQuotesMonths = new List<SalesQuotesMonth>();
        }

        //public DateTime From { get; set; }
        //public DateTime Until { get; set; }

        public decimal Total { get; set; }
        public decimal TotalCorrected { get; set; }

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
