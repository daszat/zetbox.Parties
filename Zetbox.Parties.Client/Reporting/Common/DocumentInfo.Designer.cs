using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\DocumentInfo.cst")]
    public partial class DocumentInfo : Zetbox.Parties.Client.Reporting.ReportTemplate
    {
		protected string Subject;
		protected string Author;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string Subject, string Author)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.DocumentInfo", Subject, Author);
        }

        public DocumentInfo(Arebis.CodeGeneration.IGenerationHost _host, string Subject, string Author)
            : base(_host)
        {
			this.Subject = Subject;
			this.Author = Author;

        }

        public override void Generate()
        {
#line 10 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\DocumentInfo.cst"
this.WriteObjects("Info\n");
this.WriteObjects("{\n");
this.WriteObjects("	Title = \"",  Subject , "\"\n");
this.WriteObjects("	Subject = \"",  Subject , "\"\n");
this.WriteObjects("	Author = \"",  Author , "\"\n");
this.WriteObjects("}\n");

        }

    }
}