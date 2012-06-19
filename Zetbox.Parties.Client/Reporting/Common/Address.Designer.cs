using System;
using System.Collections.Generic;
using System.Linq;
using ZBox.Basic.Parties;


namespace Kistl.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst")]
    public partial class Address : Kistl.Parties.Client.Reporting.ReportTemplate
    {
		protected ZBox.Basic.Parties.Party party;
		protected ZBox.Basic.Parties.Address address;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Parties.Party party, ZBox.Basic.Parties.Address address)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.Address", party, address);
        }

        public Address(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Parties.Party party, ZBox.Basic.Parties.Address address)
            : base(_host)
        {
			this.party = party;
			this.address = address;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("\r\n");
this.WriteObjects("",  FormatName(party) , "\r\n");
this.WriteObjects("\\linebreak\r\n");
#line 14 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
if(address != null) { 
#line 15 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("    ");
#line 15 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
if(!string.IsNullOrEmpty(address.Line1)) { 
#line 16 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Line1) , "\r\n");
this.WriteObjects("    ");
#line 18 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Line2)) { 
#line 20 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Line2) , "\r\n");
this.WriteObjects("    ");
#line 22 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.ZIPCode) || !string.IsNullOrEmpty(address.City)) { 
#line 24 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.ZIPCode) , " ",  Format(address.City) , "\r\n");
this.WriteObjects("    ");
#line 26 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
} 
    if(!string.IsNullOrEmpty(address.Country)) { 
#line 28 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("        \\linebreak\r\n");
this.WriteObjects("        ",  Format(address.Country) , "\r\n");
this.WriteObjects("    ");
#line 30 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
} 
#line 31 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
} 

        }

    }
}