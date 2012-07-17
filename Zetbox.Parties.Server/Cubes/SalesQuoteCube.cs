using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using dasz.LinqCube;
using Zetbox.Basic.Invoicing;

namespace Zetbox.Parties.Server.Cubes
{
    public class SalesQuoteCubeRecord
    {
        public DateTime Date { get; set; }
        public string Party { get; set; }
        public decimal Total { get; set; }
        public decimal Chance { get; set; }
    }

    public class SalesQuoteCube
    {
        private readonly DateTime from, thru;

        public SalesQuoteCube(IZetboxContext ctx, DateTime from, DateTime thru)
        {
            this.from = from;
            this.thru = thru;

            DimIssueDate = new Dimension<DateTime, SalesQuoteCubeRecord>("IssueDate", g => g.Date.Date)
                .BuildYearRange(from.Date, thru.Date)
                .BuildMonths()
                .Build<DateTime, SalesQuoteCubeRecord>();

            DimParty = new Dimension<string, SalesQuoteCubeRecord>("Party", p => p.Party)
                .BuildCustomerEnum(ctx)
                .Build<string, SalesQuoteCubeRecord>();

            SumTotal = new DecimalSumMeasure<SalesQuoteCubeRecord>("Total", g => g.Total);
            SumTotalCorrected = new DecimalSumMeasure<SalesQuoteCubeRecord>("TotalCorrected", g => g.Total * g.Chance / 100.0M);

            QrySalesQuotesMonth = new Query<SalesQuoteCubeRecord>("Sales quotes / month")
                .WithCrossingDimension(DimIssueDate)
                .WithCrossingDimension(DimParty)
                .WithMeasure(SumTotal)
                .WithMeasure(SumTotalCorrected);
        }

        public readonly Dimension<DateTime, SalesQuoteCubeRecord> DimIssueDate;
        public readonly Dimension<string, SalesQuoteCubeRecord> DimParty;

        public readonly DecimalSumMeasure<SalesQuoteCubeRecord> SumTotal;
        public readonly DecimalSumMeasure<SalesQuoteCubeRecord> SumTotalCorrected;

        public readonly Query<SalesQuoteCubeRecord> QrySalesQuotesMonth;

        private IQueryable<SalesQuoteCubeRecord> GetQuery(IZetboxContext ctx)
        {
            return ctx.GetQuery<SalesQuote>()
                .ToList()
                .Select(g => new SalesQuoteCubeRecord()
                {
                    Date = g.IssueDate,
                    Party = g.Customer.Party.ToString(),
                    Total = g.Items.OfType<SalesQuoteItem>().Sum(i => i.AmountNet),
                    Chance = g.Chance ?? 100.0M
                })
                .AsQueryable();
        }

        public CubeResult Result { get; private set; }

        public void Execute(IZetboxContext ctx)
        {
            Result = Cube.Execute(GetQuery(ctx), QrySalesQuotesMonth);
        }
    }
}
