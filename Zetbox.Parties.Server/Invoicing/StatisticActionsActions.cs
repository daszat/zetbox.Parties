
namespace Zetbox.Basic.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Parties.Server.Cubes;
    using Zetbox.API.Server;
    using Zetbox.Parties.Common.Dtos;

    [Implementor]
    public class StatisticActionsActions
    {
        private static Func<IZetboxServerContext> _srvCtxFactory;

        public StatisticActionsActions(Func<IZetboxServerContext> srvCtxFactory)
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
                    result.Issued.Details.Months.Add(new SalesQuotesMonth()
                    {
                        Year = month.Parent.Label,
                        Month = month.Label,
                        Total = entry[quoteCube.SumTotal].DecimalValue,
                        TotalCorrected = entry[quoteCube.SumTotalCorrected].DecimalValue,
                    });
                }

                foreach (var party in quoteCube.DimParty)
                {
                    var entry = quoteCube.Result[quoteCube.QrySalesQuotesMonth][quoteCube.DimIssueDate][party];
                    if (entry[quoteCube.SumTotal].DecimalValue != 0 && entry[quoteCube.SumTotalCorrected].DecimalValue != 0)
                    {
                        result.Issued.Details.Customers.Add(new SalesQuotesCustomer()
                        {
                            Customer = party.Label,
                            Total = entry[quoteCube.SumTotal].DecimalValue,
                            TotalCorrected = entry[quoteCube.SumTotalCorrected].DecimalValue,
                        });
                    }
                }

                // Delivery
                foreach (var year in itemCube.DimDeliveryDate)
                {
                    foreach (var month in year)
                    {
                        var entry = itemCube.Result[itemCube.QrySalesQuotesItemMonth][year][month];
                        result.Delivery.Details.Months.Add(new SalesQuotesMonth()
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
                    var entry = itemCube.Result[itemCube.QrySalesQuotesItemMonth][itemCube.DimDeliveryDate][party];
                    if (entry[itemCube.SumTotal].DecimalValue != 0 && entry[itemCube.SumTotalCorrected].DecimalValue != 0)
                    {
                        result.Delivery.Details.Customers.Add(new SalesQuotesCustomer()
                        {
                            Customer = party.Label,
                            Total = entry[itemCube.SumTotal].DecimalValue,
                            TotalCorrected = entry[itemCube.SumTotalCorrected].DecimalValue,
                        });
                    }
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
            using (var ctx = _srvCtxFactory())
            {
                var cube = new SalesInvoiceCube(ctx, from, until);
                cube.Execute(ctx);

                var result = new SalesInvoiceReport()
                {
                    From = from,
                    Until = until,
                    Date = new SalesInvoiceReport.DtoDate()
                    {
                        Total = cube.Result[cube.QryInvoicesDate][cube.DimDate][cube.SumTotal].DecimalValue,
                        TotalNet = cube.Result[cube.QryInvoicesDate][cube.DimDate][cube.SumTotalNet].DecimalValue,
                    },
                    Fulfillment = new SalesInvoiceReport.DtoFulfillment()
                    {
                        Total = cube.Result[cube.QryInvoicesFulfillmentDate][cube.DimFulfillmentDate][cube.SumFulfillment].DecimalValue,
                        TotalNet = cube.Result[cube.QryInvoicesFulfillmentDate][cube.DimFulfillmentDate][cube.SumFulfillmentNet].DecimalValue,
                        AvgDuration = (int)cube.Result[cube.QryInvoicesFulfillmentDate][cube.DimFulfillmentDate][cube.AvgFulfillmentDuration].DecimalValue,
                    },
                };

                // Date
                foreach (var month in cube.DimDate.SelectMany(y => y.Children))
                {
                    var entry = cube.Result[cube.QryInvoicesDate][month.Parent][month];
                    result.Date.Details.Months.Add(new SalesInvoiceReport.DtoDate.DtoDetails.InvoiceMonth()
                    {
                        Year = month.Parent.Label,
                        Month = month.Label,
                        Total = entry[cube.SumTotal].DecimalValue,
                        TotalNet = entry[cube.SumTotalNet].DecimalValue,
                    });
                }

                foreach (var party in cube.DimParty)
                {
                    var entry = cube.Result[cube.QryInvoicesDate][cube.DimDate][party];
                    if (entry[cube.SumTotal].DecimalValue != 0)
                    {
                        result.Date.Details.Customers.Add(new SalesInvoiceReport.DtoDate.DtoDetails.InvoiceCustomer()
                        {
                            Customer = party.Label,
                            Total = entry[cube.SumTotal].DecimalValue,
                            TotalNet = entry[cube.SumTotalNet].DecimalValue,
                        });
                    }
                }

                // Fulfillment
                foreach (var month in cube.DimFulfillmentDate.SelectMany(y => y.Children))
                {
                    var entry = cube.Result[cube.QryInvoicesFulfillmentDate][month.Parent][month];
                    result.Fulfillment.Details.Months.Add(new SalesInvoiceReport.DtoFulfillment.DtoDetails.FulfillmentMonth()
                    {
                        Year = month.Parent.Label,
                        Month = month.Label,
                        Total = entry[cube.SumFulfillment].DecimalValue,
                        TotalNet = entry[cube.SumFulfillmentNet].DecimalValue,                        
                    });
                }

                foreach (var party in cube.DimParty)
                {
                    var entry = cube.Result[cube.QryInvoicesFulfillmentDate][cube.DimFulfillmentDate][party];
                    if (entry[cube.SumFulfillment].DecimalValue != 0)
                    {
                        result.Fulfillment.Details.Customers.Add(new SalesInvoiceReport.DtoFulfillment.DtoDetails.FulfillmentCustomer()
                        {
                            Customer = party.Label,
                            AvgDuration = (int)entry[cube.AvgFulfillmentDuration].Average,
                            Total = entry[cube.SumFulfillment].DecimalValue,
                            TotalNet = entry[cube.SumFulfillmentNet].DecimalValue,
                        });
                    }
                }
                e.Result = result;
            }
        }

        [Invocation]
        public static void GetPurchaseInvoiceReport(StatisticActions obj, MethodReturnEventArgs<object> e, DateTime from, DateTime until)
        {
        }
    }
}
