

namespace Zetbox.Parties.Client.Reporting
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
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.IO;
    using MigraDoc.Rendering;
    using MigraDoc.RtfRendering;
    using ControlKinds = Zetbox.NamedObjects.Gui.ControlKinds;
    using PdfSharp.Pdf;
    using PdfSharp.Pdf.IO;
    using Zetbox.API.Common.Reporting;

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the given assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ReportingHost : Zetbox.API.Common.Reporting.AbstractReportingHost
    {
        public ReportingHost(IFileOpener fileOpener, ITempFileService tmpService)
            : base(fileOpener, tmpService)
        {
        }
        public ReportingHost(string overrideTemplateNamespace, Assembly overrideTemplateAssembly, IFileOpener fileOpener, ITempFileService tmpService)
            : base(overrideTemplateNamespace, overrideTemplateAssembly, fileOpener, tmpService)
        {
        }
    }
}
