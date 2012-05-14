using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using dasz.LinqCube;
using ZBox.Basic.Invoicing;

namespace Kistl.Parties.Server.Cubes
{
    public class SalesQuoteCubeRecord
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Chance { get; set; }
    }

    public class SalesQuoteCube
    {
        private readonly DateTime from, thru;

        public SalesQuoteCube(DateTime from, DateTime thru)
        {
            DimDate = new Dimension<DateTime, SalesQuoteCubeRecord>("Date", g => g.Date.Date)
                .BuildYearRange(from.Date, thru.Date)
                .BuildMonths()
                .Build<DateTime, SalesQuoteCubeRecord>();

            SumTotal = new DecimalSumMeasure<SalesQuoteCubeRecord>("Total", g => g.Total);
            SumTotalCorrected = new DecimalSumMeasure<SalesQuoteCubeRecord>("TotalCorrected", g => g.Total * g.Chance);

            QrySalesQuotesMonth = new Query<SalesQuoteCubeRecord>("Sales quotes / month")
                .WithPrimaryDimension(DimDate)
                .WithMeasure(SumTotal)
                .WithMeasure(SumTotalCorrected);
        }

        public readonly Dimension<DateTime, SalesQuoteCubeRecord> DimDate;

        public readonly DecimalSumMeasure<SalesQuoteCubeRecord> SumTotal;
        public readonly DecimalSumMeasure<SalesQuoteCubeRecord> SumTotalCorrected;

        public readonly Query<SalesQuoteCubeRecord> QrySalesQuotesMonth;

        private IQueryable<SalesQuoteCubeRecord> GetQuery(IKistlContext ctx)
        {
            return ctx.GetQuery<SalesQuote>()
                .Where(q => q.IssueDate >= from && q.IssueDate <= thru)
                .Select(g => new SalesQuoteCubeRecord()
                {
                    Date = g.IssueDate,
                    Total = g.Items.OfType<SalesQuoteItem>().Sum(i => i.AmountNet),
                    Chance = g.Chance ?? 1.0M
                });
        }

        public CubeResult Result { get; private set; }

        public void Execute(IKistlContext ctx)
        {
            Result = Cube.Execute(GetQuery(ctx), QrySalesQuotesMonth);
        }
    }
}
