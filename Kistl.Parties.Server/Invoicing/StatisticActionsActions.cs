
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
                var quoteCube = new SalesQuoteCube(ctx, from, until);
                quoteCube.Execute(ctx);

                var itemCube = new SalesQuoteItemCube(ctx, from, until);
                itemCube.Execute(ctx);

                var result = new SalesQuoteReport()
                {
                    From = from,
                    Until = until,
                    Issued = new SalesQuoteReport.DtoIssued()
                    {
                        Total = quoteCube.Result[quoteCube.QrySalesQuotesMonth][quoteCube.DimIssueDate][quoteCube.SumTotal].DecimalValue,
                        TotalCorrected = quoteCube.Result[quoteCube.QrySalesQuotesMonth][quoteCube.DimIssueDate][quoteCube.SumTotalCorrected].DecimalValue,
                    },
                    Delivery = new SalesQuoteReport.DtoDelivery()
                    {
                        Total = itemCube.Result[itemCube.QrySalesQuotesItemMonth][itemCube.DimDeliveryDate][itemCube.SumTotal].DecimalValue,
                        TotalCorrected = itemCube.Result[itemCube.QrySalesQuotesItemMonth][itemCube.DimDeliveryDate][itemCube.SumTotalCorrected].DecimalValue,
                    },
                };

                // Issue
                foreach (var month in quoteCube.DimIssueDate.SelectMany(y => y.Children))
                {
                    var entry = quoteCube.Result[quoteCube.QrySalesQuotesMonth][month.Parent][month];
                    result.Issued.Details.SalesQuotesIssuedMonths.Add(new SalesQuotesMonth()
                    {
                        Year = month.Parent.Label,
                        Month = month.Label,
                        Total = entry[quoteCube.SumTotal].DecimalValue,
                        TotalCorrected = entry[quoteCube.SumTotalCorrected].DecimalValue,
                    });
                }

                foreach (var party in quoteCube.DimParty)
                {
                    var entry = quoteCube.Result[quoteCube.QrySalesQuotesMonth][party];
                    result.Issued.Details.SalesQuotesIssuedCustomers.Add(new SalesQuotesCustomer()
                    {
                        Customer = party.Label,
                        Total = entry[quoteCube.SumTotal].DecimalValue,
                        TotalCorrected = entry[quoteCube.SumTotalCorrected].DecimalValue,
                    });
                }

                // Delivery
                foreach (var year in itemCube.DimDeliveryDate)
                {
                    foreach (var month in year)
                    {
                        var entry = itemCube.Result[itemCube.QrySalesQuotesItemMonth][year][month];
                        result.Delivery.Details.SalesQuotesDeliveryMonths.Add(new SalesQuotesMonth()
                        {
                            Year = year.Label,
                            Month = month.Label,
                            Total = entry[itemCube.SumTotal].DecimalValue,
                            TotalCorrected = entry[itemCube.SumTotalCorrected].DecimalValue,
                        });
                    }
                }

                foreach (var party in itemCube.DimParty)
                {
                    var entry = itemCube.Result[itemCube.QrySalesQuotesItemMonth][party];
                    result.Delivery.Details.SalesQuotesDeliveryCustomers.Add(new SalesQuotesCustomer()
                    {
                        Customer = party.Label,
                        Total = entry[itemCube.SumTotal].DecimalValue,
                        TotalCorrected = entry[itemCube.SumTotalCorrected].DecimalValue,
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
