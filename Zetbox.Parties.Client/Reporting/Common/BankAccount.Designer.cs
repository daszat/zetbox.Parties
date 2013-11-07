using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Parties;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst")]
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
#line 10 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\n");
#line 11 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
if(account != null) { 
#line 12 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\n");
this.WriteObjects("\\paragraph [ \n");
this.WriteObjects("            Format {\n");
this.WriteObjects("                TabStops +=\n");
this.WriteObjects("                {\n");
this.WriteObjects("                  Position = \"4cm\"\n");
this.WriteObjects("                }\n");
this.WriteObjects("            } ] {\n");
this.WriteObjects("\n");
this.WriteObjects("",  Format(account.Name) , "\n");
#line 22 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
if(!string.IsNullOrEmpty(account.BIC) && !string.IsNullOrEmpty(account.IBAN)) { 
#line 23 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\\linebreak\n");
this.WriteObjects("BIC: \\tab ",  Format(account.BIC) , "\n");
this.WriteObjects("\\linebreak\n");
this.WriteObjects("IBAN: \\tab ",  Format(account.IBAN) , "\n");
#line 27 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 
if(!string.IsNullOrEmpty(account.BankCodeNumber) && !string.IsNullOrEmpty(account.AccountNumber)) { 
#line 29 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("\\linebreak\n");
this.WriteObjects("",  BankAccountResources.BankCodeNumber , ": \\tab ",  Format(account.BankCodeNumber) , "\n");
this.WriteObjects("\\linebreak\n");
this.WriteObjects("",  BankAccountResources.AccountNumber , ": \\tab ",  Format(account.AccountNumber) , "\n");
#line 33 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 
#line 34 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
this.WriteObjects("}\n");
#line 35 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\BankAccount.cst"
} 

        }

    }
}