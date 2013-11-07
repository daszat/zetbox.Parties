using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\PageSetup.cst")]
    public partial class PageSetup : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected string Orientation;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.PageSetup", Orientation);
        }

        public PageSetup(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
            : base(_host)
        {
			this.Orientation = Orientation;

        }

        public override void Generate()
        {
#line 9 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\PageSetup.cst"
this.WriteObjects("\n");
this.WriteObjects("PageSetup\n");
this.WriteObjects("{\n");
this.WriteObjects("	Orientation = ",  Orientation , "\n");
this.WriteObjects("	PageFormat = A4\n");
this.WriteObjects("	TopMargin = 40\n");
this.WriteObjects("	StartingNumber = 1\n");
this.WriteObjects("}\n");

        }

    }
}