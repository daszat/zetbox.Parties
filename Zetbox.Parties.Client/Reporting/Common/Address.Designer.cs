using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Parties;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst")]
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
#line 11 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("\r\n");
this.WriteObjects("",  FormatName(party) , "\r\n");
this.WriteObjects("\\linebreak\r\n");
#line 14 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
if(address != null) { 
#line 15 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("    ");
#line 15 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
if(!string.IsNullOrEmpty(address.Line1)) { 
#line 16 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Line1) , "\r\n");
this.WriteObjects("    ");
#line 18 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Line2)) { 
#line 20 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Line2) , "\r\n");
this.WriteObjects("    ");
#line 22 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.ZIPCode) || !string.IsNullOrEmpty(address.City)) { 
#line 24 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.ZIPCode) , " ",  Format(address.City) , "\r\n");
this.WriteObjects("    ");
#line 26 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Country)) { 
#line 28 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Country) , "\r\n");
this.WriteObjects("    ");
#line 30 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 
#line 31 "C:\Users\arthur\Documents\Visual Studio 2010\Projects\Zetbox.Parties\Zetbox.Parties.Client\Reporting\Common\Address.cst"
} 

        }

    }
}