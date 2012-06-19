using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Parties;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst")]
    public partial class BankAccount : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected Zetbox.Basic.Parties.BankAccount account;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.BankAccount account)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.BankAccount", account);
        }

        public BankAccount(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.BankAccount account)
            : base(_host)
        {
			this.account = account;

        }

        public override void Generate()
        {
#line 10 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\r\n");
#line 11 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
if(account != null) { 
#line 12 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\paragraph [ \r\n");
this.WriteObjects("            Format {\r\n");
this.WriteObjects("                TabStops +=\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                  Position = \"4cm\"\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            } ] {\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("",  Format(account.Name) , "\r\n");
#line 22 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
if(!string.IsNullOrEmpty(account.BIC) && !string.IsNullOrEmpty(account.IBAN)) { 
#line 23 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("BIC: \\tab ",  Format(account.BIC) , "\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("IBAN: \\tab ",  Format(account.IBAN) , "\r\n");
#line 27 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 
if(!string.IsNullOrEmpty(account.BankCodeNumber) && !string.IsNullOrEmpty(account.AccountNumber)) { 
#line 29 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("",  BankAccountResources.BankCodeNumber , ": \\tab ",  Format(account.BankCodeNumber) , "\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("",  BankAccountResources.AccountNumber , ": \\tab ",  Format(account.AccountNumber) , "\r\n");
#line 33 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 
#line 34 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("}\r\n");
#line 35 "P:\zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 

        }

    }
}