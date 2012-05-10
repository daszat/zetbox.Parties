using System;
using System.Collections.Generic;
using System.Linq;
using ZBox.Basic.Invoicing;


namespace Kistl.Parties.Client.Reporting.Invoicing
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst")]
    public partial class SalesInvoice : Kistl.Parties.Client.Reporting.ReportTemplate
    {
		protected ZBox.Basic.Invoicing.SalesInvoice invoice;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Invoicing.SalesInvoice invoice)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Invoicing.SalesInvoice", invoice);
        }

        public SalesInvoice(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Invoicing.SalesInvoice invoice)
            : base(_host)
        {
			this.invoice = invoice;

        }

        public override void Generate()
        {
#line 10 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\document [\r\n");
#line 12 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
Common.DocumentInfo.Call(Host, "Invoice", null); 
#line 13 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("] {\r\n");
#line 14 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
Common.DocumentStyles.Call(Host); 
#line 15 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\section [\r\n");
#line 17 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
PageSetup(); 
#line 18 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("	] {\r\n");
this.WriteObjects("        \\paragraph [ Style = \"Title\" ] {\r\n");
this.WriteObjects("            ",  GetTitle() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\textframe [ RelativeVertical = Paragraph\r\n");
this.WriteObjects("                     RelativeHorizontal = Margin                     \r\n");
this.WriteObjects("                     Width = \"5cm\"\r\n");
this.WriteObjects("                     Left = \"11cm\"\r\n");
this.WriteObjects("                     WrapFormat { Style = Through } ] {\r\n");
this.WriteObjects("            \\paragraph [ Format { Alignment = Right } ] {\r\n");
this.WriteObjects("                ");
#line 29 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrg(); 
#line 30 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\linebreak\r\n");
this.WriteObjects("                ");
#line 31 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrgTaxNumber(); 
#line 32 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ",  GetToText() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ");
#line 40 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipient(); 
#line 41 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            ");
#line 42 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipientTaxNumber(); 
#line 43 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ \r\n");
this.WriteObjects("            Format {\r\n");
this.WriteObjects("                SpaceBefore = \"1cm\"\r\n");
this.WriteObjects("                TabStops +=\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                  Position = \"16cm\"\r\n");
this.WriteObjects("                  Alignment = Right\r\n");
this.WriteObjects("                } } ] {\r\n");
this.WriteObjects("            ",  GetSubject() , "\r\n");
this.WriteObjects("            \\tab \r\n");
this.WriteObjects("            ",  GetCityAndDate() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Style = \"Heading1\" Format { SpaceBefore = \"1cm\" } ] {\r\n");
this.WriteObjects("            ",  GetServicesHeading() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ",  GetPeriod() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\table [ Style = \"Compact\" Borders { Width = 0.75 } ] {\r\n");
this.WriteObjects("            \\columns {\r\n");
this.WriteObjects("                \\column [ Width = \"6cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"2cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"2cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            \\rows {\r\n");
this.WriteObjects("                \\row [ HeadingFormat = True Format { Font { Bold = True } }] {\r\n");
this.WriteObjects("                    \\cell { ",  GetSubjectHeader() , " }\r\n");
this.WriteObjects("                    \\cell { ",  GetQuantityHeader() , " }\r\n");
this.WriteObjects("                    \\cell { ",  GetUnitPriceHeader() , " }\r\n");
this.WriteObjects("                    \\cell { ",  GetVATHeader() , " }\r\n");
this.WriteObjects("                    \\cell { ",  GetAmountHeader() , " }\r\n");
this.WriteObjects("                }\r\n");
#line 82 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
foreach(var item in GetItems()) { 
#line 83 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell { ",  Format    (item.Description) , " }\r\n");
this.WriteObjects("                    \\cell { ",  Format    (item.Quantity) , " }\r\n");
this.WriteObjects("                    \\cell { ",  FormatEuro(item.UnitPrice) , " }\r\n");
this.WriteObjects("                    \\cell { ",  Format    (item.VATType.Description) , " }\r\n");
this.WriteObjects("                    \\cell { ",  FormatEuro(item.AmountNet) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 90 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 91 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
if(RenderSubTotal()) { 
#line 92 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row [ Height = \"0.1cm\" HeightRule = Exactly ] {\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\row [ Format { Font { Bold = True } } ] {\r\n");
this.WriteObjects("                    \\cell { ",  GetSubTotalDescription() , " }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell { ",  GetSubTotalAmountNet() , " }\r\n");
this.WriteObjects("                }\r\n");
#line 106 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 107 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
foreach(var vat in GetVATTypes()) { 
#line 108 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell { ",  GetVATDescription(vat) , " }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell { ",  GetVATSum(vat) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 115 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 116 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row [ Height = \"0.1cm\" HeightRule = Exactly ] {\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                \\row [ Format { Font { Bold = True } } ] {\r\n");
this.WriteObjects("                    \\cell { ",  GetTotalDescription() , " }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell {  }\r\n");
this.WriteObjects("                    \\cell { ",  GetTotalAmount() , " }\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        ");
#line 133 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatMessage(); 
#line 134 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Style = \"Heading1\" Format { SpaceBefore = \"1cm\" } ] {\r\n");
this.WriteObjects("            Conditions\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ",  GetPaymentIntroduction() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        ");
#line 141 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatBankAccount(); 
#line 142 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Format { SpaceBefore = \"1cm\" } ] {\r\n");
this.WriteObjects("            ",  GetGreetingsLine() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        ");
#line 147 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatSignature(); 
#line 148 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }

    }
}