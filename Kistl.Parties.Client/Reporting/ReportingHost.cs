

namespace Kistl.Parties.Client.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using Arebis.CodeGeneration;
    using Arebis.CodeGenerator.Templated;
    using Kistl.API;
    using Kistl.App.GUI;
    using Kistl.Client;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.IO;
    using MigraDoc.Rendering;
    using MigraDoc.RtfRendering;
    using ControlKinds = Kistl.NamedObjects.Gui.ControlKinds;
    using PdfSharp.Pdf;
    using PdfSharp.Pdf.IO;
    using Kistl.API.Common.Reporting;

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the given assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ReportingHost : Kistl.API.Common.Reporting.AbstractReportingHost
    {
        public ReportingHost(IFileOpener fileOpener)
            : base(fileOpener)
        {
        }
        public ReportingHost(string overrideTemplateNamespace, Assembly overrideTemplateAssembly, IFileOpener fileOpener)
            : base(overrideTemplateNamespace, overrideTemplateAssembly, fileOpener)
        {
        }
    }
}
