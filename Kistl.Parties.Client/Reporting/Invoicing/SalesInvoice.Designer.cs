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
this.WriteObjects("        \\paragraph [ ",  GetStyleTitle() , " ] {\r\n");
this.WriteObjects("            ",  GetTitle() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\textframe [ ",  GetIntOrgTextFrame() , " ] {\r\n");
this.WriteObjects("            \\paragraph [ Format { Alignment = Right } ] {\r\n");
this.WriteObjects("                ");
#line 25 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrg(); 
#line 26 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\linebreak\r\n");
this.WriteObjects("                ");
#line 27 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrgTaxNumber(); 
#line 28 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ",  GetToText() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ");
#line 36 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipient(); 
#line 37 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            ");
#line 38 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipientTaxNumber(); 
#line 39 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
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
this.WriteObjects("            Services\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\table [ Style = \"Compact\" Borders { Width = 0.75 } ] {\r\n");
this.WriteObjects("            \\columns {\r\n");
this.WriteObjects("                \\column [ Width = \"5cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"2cm\" ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("                \\column [ Width = \"3cm\" Format { Alignment = Right } ]\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            \\rows {\r\n");
this.WriteObjects("                \\row [ HeadingFormat = True Format { Font { Bold = True } }] {\r\n");
this.WriteObjects("                    \\cell { Subject }\r\n");
this.WriteObjects("                    \\cell { Time }\r\n");
this.WriteObjects("                    \\cell { EUR / h }\r\n");
this.WriteObjects("                    \\cell { Quantity }\r\n");
this.WriteObjects("                    \\cell { Amount }\r\n");
this.WriteObjects("                }\r\n");
#line 74 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
foreach(var item in invoice.Items) { 
#line 75 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("                \\row {\r\n");
this.WriteObjects("                    \\cell { ",  Format(item.Description) , " }\r\n");
this.WriteObjects("                    \\cell { - }\r\n");
this.WriteObjects("                    \\cell { - }\r\n");
this.WriteObjects("                    \\cell { ",  Format(item.Quantity) , " }\r\n");
this.WriteObjects("                    \\cell { ",  Format(item.AmountNet) , " }\r\n");
this.WriteObjects("                }\r\n");
#line 82 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
} 
#line 83 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Style = \"Heading1\" Format { SpaceBefore = \"1cm\" } ] {\r\n");
this.WriteObjects("            Conditions\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \\paragraph [ \r\n");
this.WriteObjects("            Format {\r\n");
this.WriteObjects("                TabStops +=\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                  Position = \"5cm\"\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            } ] {\r\n");
this.WriteObjects("            Payment within 30 days to:\r\n");
this.WriteObjects("            \\linebreak \\linebreak\r\n");
this.WriteObjects("            Bankaustria\r\n");
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            BLZ: \\tab 12000\r\n");
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            Kontonummer: \\tab 123344345423\r\n");
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            BIC: \\tab BKAUATWW\r\n");
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            IBAN: \\tab AT55 1200 123344345423\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Format { SpaceBefore = \"1cm\" } ] {\r\n");
this.WriteObjects("            We thank you for your order!\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph [ Format { SpaceBefore = \"3cm\" } ] {\r\n");
this.WriteObjects("            My name\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }

    }
}