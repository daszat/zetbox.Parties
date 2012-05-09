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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Parties.Party party)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.Address", party);
        }

        public Address(Arebis.CodeGeneration.IGenerationHost _host, ZBox.Basic.Parties.Party party)
            : base(_host)
        {
			this.party = party;

        }

        public override void Generate()
        {
#line 10 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\Address.cst"
this.WriteObjects("\r\n");
this.WriteObjects("",  FormatName(party) , "\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("Test Street 12234\r\n");
this.WriteObjects("\\linebreak\r\n");
this.WriteObjects("1234 Vienna");

        }

    }
}