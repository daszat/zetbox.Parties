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
this.WriteObjects("        \\textframe [ RelativeVertical = Paragraph ] {\r\n");
this.WriteObjects("            ");
#line 24 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrg(); 
#line 25 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            ");
#line 26 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatIntOrgTaxNumber(); 
#line 27 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ",  GetToText() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        \\paragraph {\r\n");
this.WriteObjects("            ");
#line 34 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipient(); 
#line 35 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("            \\linebreak\r\n");
this.WriteObjects("            ");
#line 36 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
FormatRecipientTaxNumber(); 
#line 37 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Invoicing\SalesInvoice.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }

    }
}