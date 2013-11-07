using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Parties;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst")]
    public partial class Address : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected Zetbox.Basic.Parties.Party party;
		protected Zetbox.Basic.Parties.Address address;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.Party party, Zetbox.Basic.Parties.Address address)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.Address", party, address);
        }

        public Address(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.Party party, Zetbox.Basic.Parties.Address address)
            : base(_host)
        {
			this.party = party;
			this.address = address;

        }

        public override void Generate()
        {
#line 11 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("\n");
this.WriteObjects("",  FormatName(party) , "\n");
this.WriteObjects("\\linebreak\n");
#line 14 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
if(address != null) { 
#line 15 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("    ");
#line 15 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
if(!string.IsNullOrEmpty(address.Line1)) { 
#line 16 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\n");
this.WriteObjects("        ",  Format(address.Line1) , "\n");
this.WriteObjects("    ");
#line 18 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Line2)) { 
#line 20 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\n");
this.WriteObjects("        ",  Format(address.Line2) , "\n");
this.WriteObjects("    ");
#line 22 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.ZIPCode) || !string.IsNullOrEmpty(address.City)) { 
#line 24 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\n");
this.WriteObjects("        ",  Format(address.ZIPCode) , " ",  Format(address.City) , "\n");
this.WriteObjects("    ");
#line 26 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Country)) { 
#line 28 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\n");
this.WriteObjects("        ",  Format(address.Country) , "\n");
this.WriteObjects("    ");
#line 30 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
#line 31 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 

        }

    }
}