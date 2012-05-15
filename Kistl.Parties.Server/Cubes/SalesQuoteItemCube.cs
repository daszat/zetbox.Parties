using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using dasz.LinqCube;
using ZBox.Basic.Invoicing;

namespace Kistl.Parties.Server.Cubes
{
    public class SalesQuoteItemCubeRecord
    {
        public DateTime Date { get; set; }
        public string Party { get; set; }
        public decimal Total { get; set; }
        public decimal Chance { get; set; }
    }

    public class SalesQuoteItemCube
    {
        private readonly DateTime from, thru;

        public SalesQuoteItemCube(IKistlContext ctx, DateTime from, DateTime thru)
        {
            this.from = from;
            this.thru = thru;

            DimDeliveryDate = new Dimension<DateTime, SalesQuoteItemCubeRecord>("Delivery date", g => g.Date.Date)
                .BuildYearRange(from.Date, thru.Date)
                .BuildMonths()
                .Build<DateTime, SalesQuoteItemCubeRecord>();

            DimParty = new Dimension<string, SalesQuoteItemCubeRecord>("Party", p => p.Party)
                .BuildCustomerEnum(ctx)
                .Build<string, SalesQuoteItemCubeRecord>();

            SumTotal = new DecimalSumMeasure<SalesQuoteItemCubeRecord>("Total", g => g.Total);
            SumTotalCorrected = new DecimalSumMeasure<SalesQuoteItemCubeRecord>("Total corrected", g => g.Total * g.Chance / 100.0M);

            QrySalesQuotesItemMonth = new Query<SalesQuoteItemCubeRecord>("Sales quote items / month")
                .WithSecondaryDimension(DimDeliveryDate)
                .WithSecondaryDimension(DimParty)
                .WithMeasure(SumTotal)
                .WithMeasure(SumTotalCorrected);
        }

        public readonly Dimension<DateTime, SalesQuoteItemCubeRecord> DimDeliveryDate;
        public readonly Dimension<string, SalesQuoteItemCubeRecord> DimParty;

        public readonly DecimalSumMeasure<SalesQuoteItemCubeRecord> SumTotal;
        public readonly DecimalSumMeasure<SalesQuoteItemCubeRecord> SumTotalCorrected;

        public readonly Query<SalesQuoteItemCubeRecord> QrySalesQuotesItemMonth;

        private IQueryable<SalesQuoteItemCubeRecord> GetQuery(IKistlContext ctx)
        {
            return ctx.GetQuery<SalesQuoteItem>()
                .Where(q => q.EstimatedDelivery >= from && q.EstimatedDelivery <= thru)
                .ToList()
                .Select(g => new SalesQuoteItemCubeRecord()
                {
                    Date = g.EstimatedDelivery,
                    Party = g.SalesQuote.Customer.Party.ToString(),
                    Total = g.AmountNet,
                    Chance = g.SalesQuote.Chance ?? 100.0M
                })
                .AsQueryable();
        }

        public CubeResult Result { get; private set; }

        public void Execute(IKistlContext ctx)
        {
            Result = Cube.Execute(GetQuery(ctx), QrySalesQuotesItemMonth);
        }
    }
}
