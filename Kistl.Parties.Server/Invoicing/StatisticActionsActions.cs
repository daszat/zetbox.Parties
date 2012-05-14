
namespace ZBox.Basic.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using ZBox.Basic.Invoicing;
    using Kistl.Parties.Server.Cubes;
    using Kistl.API.Server;
    using Kistl.Parties.Common.Dtos;

    [Implementor]
    public class StatisticActionsActions
    {
        public static Func<IKistlServerContext> _srvCtxFactory;

        public StatisticActionsActions(Func<IKistlServerContext> srvCtxFactory)
        {
            _srvCtxFactory = srvCtxFactory;
        }

        [Invocation]
        public static void GetSalesQuoteReport(StatisticActions obj, MethodReturnEventArgs<object> e, DateTime from, DateTime until)
        {
            using (var ctx = _srvCtxFactory())
            {
                var cube = new SalesQuoteCube(from, until);
                cube.Execute(ctx);

                var result = new SalesQuoteReport()
                {
                    //From = from,
                    //Until = until,
                    Total = cube.Result[cube.QrySalesQuotesMonth][cube.DimDate][cube.SumTotal].DecimalValue,
                    TotalCorrected = cube.Result[cube.QrySalesQuotesMonth][cube.DimDate][cube.SumTotalCorrected].DecimalValue,
                };

                foreach (var year in cube.DimDate)
                foreach (var month in year)
                {
                    var entry = cube.Result[cube.QrySalesQuotesMonth][year][month];
                    result.SalesQuotesMonths.Add(new SalesQuotesMonth()
                    {
                        Year = year.Label,
                        Month = month.Label,
                        Total = entry[cube.SumTotal].DecimalValue,
                        TotalCorrected = entry[cube.SumTotalCorrected].DecimalValue,
                    });
                }

                e.Result = result;
            }
        }

        [Invocation]
        public static void GetPurchaseQuoteReport(StatisticActions obj, MethodReturnEventArgs<object> e, DateTime from, DateTime until)
        {
        }

        [Invocation]
        public static void GetSalesInvoiceReport(StatisticActions obj, MethodReturnEventArgs<object> e, DateTime from, DateTime until)
        {
        }

        [Invocation]
        public static void GetPurchaseInvoiceReport(StatisticActions obj, MethodReturnEventArgs<object> e, DateTime from, DateTime until)
        {
        }
    }
}
