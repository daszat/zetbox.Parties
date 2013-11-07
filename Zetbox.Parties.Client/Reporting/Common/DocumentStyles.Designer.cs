using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.Parties.Client.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\DocumentStyles.cst")]
    public partial class DocumentStyles : Zetbox.Parties.Client.Reporting.ReportTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.DocumentStyles");
        }

        public DocumentStyles(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 8 "V:\Jenkins\workspace\zetbox.Parties-develop-update-nuget\Zetbox.Parties.Client\Reporting\Common\DocumentStyles.cst"
this.WriteObjects("  \\styles {\n");
this.WriteObjects("    Normal {\n");
this.WriteObjects("      Font { Name = \"Verdana\" Size = 10 }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        SpaceBefore = 6\n");
this.WriteObjects("        SpaceAfter = 6\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Compact : Normal {\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        SpaceBefore = \"0.07cm\"\n");
this.WriteObjects("        SpaceAfter = \"0.07cm\"\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Title : Normal {\n");
this.WriteObjects("      Font { Size = 16 Bold = true }\n");
this.WriteObjects("    }\n");
this.WriteObjects("	\n");
this.WriteObjects("    Heading1 {\n");
this.WriteObjects("      Font { Size = 14 Bold = true }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("		SpaceBefore = 6\n");
this.WriteObjects("        SpaceAfter = 6\n");
this.WriteObjects("        PageBreakBefore = false\n");
this.WriteObjects("        OutlineLevel = Level1\n");
this.WriteObjects("		KeepWithNext = True\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Heading2 {\n");
this.WriteObjects("      Font { Size = 12 Bold = true }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        SpaceBefore = 6\n");
this.WriteObjects("		SpaceAfter = 3\n");
this.WriteObjects("        OutlineLevel = Level2\n");
this.WriteObjects("		KeepWithNext = True\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Heading3 {\n");
this.WriteObjects("      Font { Size = 11 Bold = true }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        SpaceAfter = 3\n");
this.WriteObjects("        OutlineLevel = Level3\n");
this.WriteObjects("		KeepWithNext = True\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Heading4 {\n");
this.WriteObjects("      Font { Size = 10 Bold = true }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        SpaceAfter = 2\n");
this.WriteObjects("        OutlineLevel = Level4\n");
this.WriteObjects("		KeepWithNext = True\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Heading5 { ParagraphFormat { OutlineLevel = Level5 } }\n");
this.WriteObjects("	Heading6 { ParagraphFormat { OutlineLevel = Level6 } }\n");
this.WriteObjects("	Heading7 { ParagraphFormat { OutlineLevel = Level7 } }\n");
this.WriteObjects("	Heading8 { ParagraphFormat { OutlineLevel = Level8 } }\n");
this.WriteObjects("	Heading9 { ParagraphFormat { OutlineLevel = Level9 } }\n");
this.WriteObjects("\n");
this.WriteObjects("    Header {\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        TabStops += {\n");
this.WriteObjects("          Position = \"16cm\"\n");
this.WriteObjects("          Alignment = Right\n");
this.WriteObjects("        }\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    Footer {\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        TabStops += {\n");
this.WriteObjects("          Position = \"8cm\"\n");
this.WriteObjects("          Alignment = Center\n");
this.WriteObjects("        }\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    InvalidStyleName {\n");
this.WriteObjects("      Font { Bold = true Underline = Dash Color = Red }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    TextBox : Normal {\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        Alignment = Justify\n");
this.WriteObjects("        Borders\n");
this.WriteObjects("        {\n");
this.WriteObjects("          Width = 2.5\n");
this.WriteObjects("          DistanceFromTop = 3\n");
this.WriteObjects("          DistanceFromBottom = 3\n");
this.WriteObjects("          DistanceFromLeft = 3\n");
this.WriteObjects("          DistanceFromRight = 3\n");
this.WriteObjects("        }\n");
this.WriteObjects("        Shading { Color = SkyBlue }\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("\n");
this.WriteObjects("    TOC : Normal {\n");
this.WriteObjects("      Font { }\n");
this.WriteObjects("      ParagraphFormat {\n");
this.WriteObjects("        TabStops += {\n");
this.WriteObjects("          Position = \"16cm\"\n");
this.WriteObjects("          Alignment = Right\n");
this.WriteObjects("          Leader = Dots\n");
this.WriteObjects("        }\n");
this.WriteObjects("      }\n");
this.WriteObjects("    }\n");
this.WriteObjects("  }");

        }

    }
}