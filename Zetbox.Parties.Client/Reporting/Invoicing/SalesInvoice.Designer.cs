using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Invoicing;


namespace Zetbox.Parties.Client.Reporting.Invoicing
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst")]
    public partial class SalesInvoice : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected Zetbox.Basic.Invoicing.SalesInvoice invoice;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Invoicing.SalesInvoice invoice)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Invoicing.SalesInvoice", invoice);
        }

        public SalesInvoice(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Invoicing.SalesInvoice invoice)
            : base(_host)
        {
			this.invoice = invoice;

        }

        public override void Generate()
        {
#line 10 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\n");
this.WriteObjects("\\document [\n");
#line 12 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
Common.DocumentInfo.Call(Host, "Invoice", null); 
#line 13 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("] {\n");
#line 14 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
Common.DocumentStyles.Call(Host); 
#line 15 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\n");
this.WriteObjects("\\section [\n");
#line 17 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
PageSetup(); 
#line 18 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("	] {\n");
this.WriteObjects("        \\paragraph [ Style = \"Title\" Format { SpaceBefore = \"2cm\" } ] {\n");
this.WriteObjects("            ",  GetTitle() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\textframe [ RelativeVertical = Paragraph\n");
this.WriteObjects("                     RelativeHorizontal = Margin                     \n");
this.WriteObjects("                     Width = \"5cm\"\n");
this.WriteObjects("                     Left = \"11cm\"\n");
this.WriteObjects("                     WrapFormat { Style = Through } ] {\n");
this.WriteObjects("            \\paragraph [ Format { Alignment = Right } ] {\n");
this.WriteObjects("                ");
#line 29 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrg(); 
#line 30 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\linebreak \\linebreak\n");
this.WriteObjects("                ");
#line 31 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrgTaxNumber(); 
#line 32 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph {\n");
this.WriteObjects("            ",  GetToText() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph {\n");
this.WriteObjects("            ");
#line 40 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipient(); 
#line 41 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            \\linebreak \\linebreak\n");
this.WriteObjects("            ");
#line 42 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipientTaxNumber(); 
#line 43 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph [ \n");
this.WriteObjects("            Format {\n");
this.WriteObjects("                SpaceBefore = \"1cm\"\n");
this.WriteObjects("                TabStops +=\n");
this.WriteObjects("                {\n");
this.WriteObjects("                  Position = \"16cm\"\n");
this.WriteObjects("                  Alignment = Right\n");
this.WriteObjects("                } } ] {\n");
this.WriteObjects("            ",  GetSubject() , "\n");
this.WriteObjects("            \\tab \n");
this.WriteObjects("            ",  GetCityAndDate() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph [ Style = \"Heading1\" Format { SpaceBefore = \"1cm\" } ] {\n");
this.WriteObjects("            ",  GetServicesHeading() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph {\n");
this.WriteObjects("            ",  GetPeriod() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        \\table [ Style = \"Compact\" Borders { Width = 0.25 Color = 0xFFAAAAAA } ] {\n");
this.WriteObjects("            \\columns {\n");
this.WriteObjects("                \\column [ Width = \"6cm\" ]\n");
this.WriteObjects("                \\column [ Width = \"2cm\" Format { Alignment = Right } ]\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\n");
this.WriteObjects("                \\column [ Width = \"2cm\" Format { Alignment = Right } ]\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\n");
this.WriteObjects("            }\n");
this.WriteObjects("            \\rows {\n");
this.WriteObjects("                \\row [ HeadingFormat = True Format { Font { Bold = True } }] {\n");
this.WriteObjects("                    \\cell { ",  GetSubjectHeader() , " }\n");
this.WriteObjects("                    \\cell { ",  GetQuantityHeader() , " }\n");
this.WriteObjects("                    \\cell { ",  GetUnitPriceHeader() , " }\n");
this.WriteObjects("                    \\cell { ",  GetVATHeader() , " }\n");
this.WriteObjects("                    \\cell { ",  GetAmountHeader() , " }\n");
this.WriteObjects("                }\n");
#line 82 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
foreach(var item in GetItems()) { 
#line 83 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row {\n");
this.WriteObjects("                    \\cell { ",  Format    (item.Description) , " }\n");
this.WriteObjects("                    \\cell { ",  Format    (item.Quantity) , " }\n");
this.WriteObjects("                    \\cell { ",  FormatEuro(item.UnitPrice) , " }\n");
this.WriteObjects("                    \\cell { ",  Format    (item.VATType.Description) , " }\n");
this.WriteObjects("                    \\cell { ",  FormatEuro(item.AmountNet) , " }\n");
this.WriteObjects("                }\n");
#line 90 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 91 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
if(RenderSubTotal()) { 
#line 92 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row [ Height = \"0.1cm\" HeightRule = Exactly ] {\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                }\n");
this.WriteObjects("                \\row [ Format { Font { Bold = True } } ] {\n");
this.WriteObjects("                    \\cell { ",  GetSubTotalDescription() , " }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell { ",  GetSubTotalAmountNet() , " }\n");
this.WriteObjects("                }\n");
#line 106 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 107 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
foreach(var vat in GetVATTypes()) { 
#line 108 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row {\n");
this.WriteObjects("                    \\cell { ",  GetVATDescription(vat) , " }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell { ",  GetVATSum(vat) , " }\n");
this.WriteObjects("                }\n");
#line 115 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 116 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row [ Height = \"0.1cm\" HeightRule = Exactly ] {\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                }\n");
this.WriteObjects("                \\row [ Format { Font { Bold = True } } ] {\n");
this.WriteObjects("                    \\cell { ",  GetTotalDescription() , " }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell {  }\n");
this.WriteObjects("                    \\cell { ",  GetTotalAmount() , " }\n");
this.WriteObjects("                }\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        ");
#line 133 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatMessage(); 
#line 134 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\n");
#line 135 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
if (invoice.CanceledInvoice == null) { // is not reversal 
#line 136 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("        \\paragraph [ Style = \"Heading1\" Format { SpaceBefore = \"1cm\" } ] {\n");
this.WriteObjects("            ",  GetPaymentTitle() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("        \\paragraph {\n");
this.WriteObjects("            ",  GetPaymentIntroduction() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("        ");
#line 142 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatBankAccount(); 
#line 143 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 144 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\n");
this.WriteObjects("        \\paragraph [ Format { SpaceBefore = \"1cm\" } ] {\n");
this.WriteObjects("            ",  GetGreetingsLine() , "\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        ");
#line 149 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatSignature(); 
#line 150 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("    }\n");
this.WriteObjects("}");

        }

    }
}