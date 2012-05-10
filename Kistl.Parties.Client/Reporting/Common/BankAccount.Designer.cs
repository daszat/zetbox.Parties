using System;
using System.Collections.Generic;
using System.Linq;
using ZBox.Basic.Parties;


namespace Kistl.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\BankAccount.cst")]
    public partial class BankAccount : Kistl.Parties.Client.Reporting.ReportTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.BankAccount");
        }

        public BankAccount(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 9 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\\paragraph [ \r\n");
this.WriteObjects("            Format {\r\n");
this.WriteObjects("                TabStops +=\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                  Position = \"5cm\"\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            } ] {\r\n");
this.WriteObjects("Bankaustria\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("BLZ: \\tab 12000\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("Kontonummer: \\tab 123344345423\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("BIC: \\tab BKAUATWW\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("IBAN: \\tab AT55 1200 123344345423\r\n");
this.WriteObjects("}");

        }

    }
}