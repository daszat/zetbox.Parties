using System;
using System.Collections.Generic;
using System.Linq;


namespace Kistl.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\PageSetup.cst")]
    public partial class PageSetup : Kistl.Parties.Client.Reporting.ReportTemplate
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
#line 9 "P:\Kistl.Parties\Kistl.Parties.Client\Reporting\Common\PageSetup.cst"
this.WriteObjects("\r\n");
this.WriteObjects("PageSetup\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("	Orientation = ",  Orientation , "\r\n");
this.WriteObjects("	PageFormat = A4\r\n");
this.WriteObjects("	TopMargin = 40\r\n");
this.WriteObjects("	StartingNumber = 1\r\n");
this.WriteObjects("}\r\n");

        }

    }
}