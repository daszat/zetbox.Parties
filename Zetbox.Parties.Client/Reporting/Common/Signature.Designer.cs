using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.Basic.Parties;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Signature.cst")]
    public partial class Signature : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected Zetbox.Basic.Parties.Party party;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.Party party)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.Signature", party);
        }

        public Signature(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.Basic.Parties.Party party)
            : base(_host)
        {
			this.party = party;

        }

        public override void Generate()
        {
#line 10 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\Signature.cst"
this.WriteObjects("\n");
this.WriteObjects("\\paragraph [ Format { SpaceBefore = \"3cm\" } ] {\n");
this.WriteObjects("    ",  FormatName(party) , "\n");
this.WriteObjects("}");

        }

    }
}