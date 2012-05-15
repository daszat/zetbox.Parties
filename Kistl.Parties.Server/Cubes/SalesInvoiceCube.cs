using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using dasz.LinqCube;
using ZBox.Basic.Invoicing;

namespace Kistl.Parties.Server.Cubes
{
    public class SalesInvoiceCubeRecord
    {
        public DateTime Date { get; set; }
        public DateTime? FulfillmentDate { get; set; }
        public string Party { get; set; }
        public decimal Total { get; set; }
        public decimal TotalNet { get; set; }
        public decimal Fulfillment { get; set; }
    }

    public class SalesInvoiceCube
    {
        private readonly DateTime from, thru;

        public SalesInvoiceCube(IKistlContext ctx, DateTime from, DateTime thru)
        {
            this.from = from;
            this.thru = thru;

            DimDate = new Dimension<DateTime, SalesInvoiceCubeRecord>("Date", g => g.Date.Date)
                .BuildYearRange(from.Date, thru.Date)
                .BuildMonths()
                .Build<DateTime, SalesInvoiceCubeRecord>();

            DimFulfillmentDate = new Dimension<DateTime, SalesInvoiceCubeRecord>("Fulfillment date", g => (g.FulfillmentDate ?? DateTime.MinValue).Date)
                .BuildYearRange(from.Date, thru.Date)
                .BuildMonths()
                .Build<DateTime, SalesInvoiceCubeRecord>();

            DimParty = new Dimension<string, SalesInvoiceCubeRecord>("Party", p => p.Party)
                .BuildCustomerEnum(ctx)
                .Build<string, SalesInvoiceCubeRecord>();

            SumTotal = new DecimalSumMeasure<SalesInvoiceCubeRecord>("Total", g => g.Total);
            SumTotalNet = new DecimalSumMeasure<SalesInvoiceCubeRecord>("Total net", g => g.TotalNet);
            SumFulfillment = new DecimalSumMeasure<SalesInvoiceCubeRecord>("Fulfillment", g => g.Fulfillment);
            SumFulfillmentNet = new DecimalSumMeasure<SalesInvoiceCubeRecord>("Fulfillment net", g => g.Fulfillment * g.TotalNet / g.Total);
            AvgFulfillmentDuration = new FilteredMeasure<SalesInvoiceCubeRecord, double>(f => f.FulfillmentDate.HasValue, new DoubleSumMeasure<SalesInvoiceCubeRecord>("Avg. payment duration", g => (g.FulfillmentDate.Value - g.Date).TotalDays));

            QryInvoicesDate = new Query<SalesInvoiceCubeRecord>("Invoices / month")
                .WithSecondaryDimension(DimDate)
                .WithSecondaryDimension(DimParty)
                .WithMeasure(SumTotal)
                .WithMeasure(SumTotalNet)
                .WithMeasure(AvgFulfillmentDuration);

            QryInvoicesFulfillmentDate = new Query<SalesInvoiceCubeRecord>("Invoices fulfillment / month")
                .WithSecondaryDimension(DimFulfillmentDate)
                .WithSecondaryDimension(DimParty)
                .WithMeasure(SumFulfillment)
                .WithMeasure(SumFulfillmentNet)
                .WithMeasure(AvgFulfillmentDuration);
        }

        public readonly Dimension<DateTime, SalesInvoiceCubeRecord> DimDate;
        public readonly Dimension<DateTime, SalesInvoiceCubeRecord> DimFulfillmentDate;
        public readonly Dimension<string, SalesInvoiceCubeRecord> DimParty;

        public readonly DecimalSumMeasure<SalesInvoiceCubeRecord> SumTotal;
        public readonly DecimalSumMeasure<SalesInvoiceCubeRecord> SumTotalNet;
        public readonly DecimalSumMeasure<SalesInvoiceCubeRecord> SumFulfillment;
        public readonly DecimalSumMeasure<SalesInvoiceCubeRecord> SumFulfillmentNet;
        public readonly FilteredMeasure<SalesInvoiceCubeRecord, double> AvgFulfillmentDuration;

        public readonly Query<SalesInvoiceCubeRecord> QryInvoicesDate;
        public readonly Query<SalesInvoiceCubeRecord> QryInvoicesFulfillmentDate;

        private IQueryable<SalesInvoiceCubeRecord> GetQuery(IKistlContext ctx)
        {
            return ctx.GetQuery<SalesInvoice>()
                .ToList()
                .Select(g => new SalesInvoiceCubeRecord()
                {
                    Date = g.Date,
                    FulfillmentDate = g.FulfillmentDate,
                    Party = g.Customer.Party.ToString(),
                    Total = g.Total,
                    TotalNet = g.TotalNet,
                    Fulfillment = g.PaymentAmount,
                })
                .AsQueryable();
        }

        public CubeResult Result { get; private set; }

        public void Execute(IKistlContext ctx)
        {
            Result = Cube.Execute(GetQuery(ctx), QryInvoicesDate, QryInvoicesFulfillmentDate);
        }
    }
}
