

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

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the given assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public class ReportingHost : IGenerationHost, IDisposable
    {
        private Dictionary<string, TemplateInfo> templates;
        private NameValueCollection settings;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly Func<IKistlContext> _ctxFactory;
        private readonly IFrozenContext _frozenCtx;
        private readonly IFileOpener _fileOpener;

        private TextWriter contextWriter = null;
        //private List<string> _tempDirs = new List<string>();

        // TODO: Move that class into a common reporting assembly and create a own Ini50 derived class with configuration
        internal ReportingHost(string ns, Assembly a, Func<IKistlContext> ctxFactory, IViewModelFactory vmFactory, IFrozenContext frozenCtx, IFileOpener fileOpener)
        {
            if (string.IsNullOrEmpty(ns)) throw new ArgumentNullException("ns");
            if (a == null) throw new ArgumentNullException("a");
            if (vmFactory == null) throw new ArgumentNullException("vmFactory");
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");

            _viewModelFactory = vmFactory;
            _ctxFactory = ctxFactory;
            _frozenCtx = frozenCtx;
            _fileOpener = fileOpener;

            var settings = new NameValueCollection();
            settings["reporttemplatenamespace"] = ns;
            settings["reporttemplateassembly"] = a.FullName;

            // Default Inititalization
            Initialize(settings);

            ErrorsAreFatal = false;
        }

        public void Initialize(NameValueCollection settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            // Store settings:
            this.settings = settings;

            // Setup generation language settings:
            GenerationLanguage.DefaultNameSpace = "Arebis.DynamicAssembly";
            GenerationLanguage.DefaultBaseClass = "Arebis.CodeGeneration.CodeTemplate";
            GenerationLanguage.CodeBuilders["vb"] = typeof(VBCodeBuilder);
            GenerationLanguage.CodeBuilders["c#"] = typeof(CSCodeBuilder);
            GenerationLanguage.DefaultTemplateLanguage = "c#";

            // Default to T3 syntax:
            new Arebis.CodeGenerator.Templated.Syntax.T3Syntax().Setup(this.settings);

            // Run GenerationSetup if any:
            foreach (string generationSetupTypeName in settings.GetValues("generatorsetup") ?? new string[0])
            {
                try
                {
                    ((IGenerationSetup)Activator.CreateInstance(Type.GetType(generationSetupTypeName)))
                    .Setup(this.settings);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Failed to setup generator by '{0}': {1}", generationSetupTypeName, ex.Message));
                }
            }

            // Build initial context writer:
            this.contextWriter = new StringWriter();

            // Initialize compiled templates:
            this.templates = new Dictionary<string, TemplateInfo>();
        }

        public void CallTemplate(string templateClass, params object[] parameters)
        {
            this.CallTemplateToContext(templateClass, parameters);
        }

        public void CallTemplateToFile(string templateClass, string outputfile, params object[] parameters)
        {
            throw new NotSupportedException();
        }

        private void CallTemplateToContext(string templateClass, params object[] parameters)
        {
            var providerName = String.Format(
                "{0}.{1}, {2}",
                Settings["reporttemplatenamespace"],
                templateClass,
                Settings["reporttemplateassembly"]);
            Type t = Type.GetType(providerName);
            if (t == null)
            {
                throw new ArgumentOutOfRangeException("templateClass", String.Format("No class found for {0}", templateClass));
            }

            var template = (CodeTemplate)Activator.CreateInstance(t, new object[] { this }.Concat(parameters).ToArray());
            template.Generate();
        }

        public virtual void WriteFile(string filename, string content)
        {
            throw new NotSupportedException();
        }

        public NameValueCollection Settings
        {
            get { return this.settings; }
        }

        public void WriteOutput(string str)
        {
            this.contextWriter.Write(str);
        }

        public string NewLineString
        {
            get { return Environment.NewLine; }
        }

        void IGenerationHost.Log(string fmt, params object[] args)
        {
        }

        public void Dispose()
        {
            if (contextWriter != null) contextWriter.Dispose();

            // Dispose templates:
            if (this.templates != null)
            {
                foreach (TemplateInfo item in this.templates.Values)
                {
                    item.Dispose();
                }
                this.templates = null;
            }

            //foreach (var f in _tempDirs)
            //{
            //    try
            //    {
            //        // TODO: Move that to a global helper and delete files on shutdown
            //        // Cannot use that - Acrobat is too slow, file has been deleted in the meantime
            //        // System.IO.File.Delete(f);
            //    }
            //    catch
            //    {
            //        // dont care
            //    }
            //}
        }

        private Stream GetMDDLStream()
        {
            // Copy stream
            MemoryStream stream = new MemoryStream();
            StreamWriter sw = new StreamWriter(stream);
            System.Diagnostics.Debug.WriteLine(contextWriter.ToString());
            sw.Write(contextWriter.ToString());
            sw.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private Document GetDocument()
        {
            var s = GetMDDLStream();
            var errors = new DdlReaderErrors();
            DdlReader rd = new DdlReader(s, errors);
            try
            {
                var doc = rd.ReadDocument();
                LogErrors(errors, null, s);
                ReportErrors(errors, null, s);
                return doc;
            }
            catch (Exception ex)
            {
                LogErrors(errors, ex, s);
                ReportErrors(errors, ex, s);
                throw;
            }
        }

        /// <summary>
        /// Whether or not errors are fatal. Set to true to fail with an exception, else the user receives a report and the document is processed as is.
        /// </summary>
        public bool ErrorsAreFatal
        {
            get;
            set;
        }

        private void LogErrors(DdlReaderErrors errors, Exception ex, System.IO.Stream mddl)
        {
            if (ex != null)
            {
                Kistl.API.Utils.Logging.Log.Error("Exception during report creation", ex);
            }

            if (errors != null)
            {
                foreach (DdlReaderError e in errors)
                {
                    switch (e.ErrorLevel)
                    {
                        case DdlErrorLevel.Error:
                            Kistl.API.Utils.Logging.Log.Error(e.ToString());
                            break;
                        case DdlErrorLevel.Warning:
                            Kistl.API.Utils.Logging.Log.Warn(e.ToString());
                            break;
                        default:
                            Kistl.API.Utils.Logging.Log.Info(e.ToString());
                            break;
                    }
                }
            }
        }

        private void ReportErrors(DdlReaderErrors errors, Exception ex, System.IO.Stream mddl)
        {
            if (ex != null || (errors != null && errors.ErrorCount > 0))
            {
                if (ErrorsAreFatal)
                {
                    var msg = new StringBuilder("Errors while rendering document:");
                    if (ex != null)
                    {
                        msg.Append("\n" + ex.ToString());
                    }
                    foreach (var error in errors)
                    {
                        msg.Append("\n" + error.ToString());
                    }
                    throw new ApplicationException(msg.ToString());
                }

                var ctx = _ctxFactory();
                var valueModels = new List<BaseValueViewModel>();

                if (ex != null)
                {
                    var exMdl = new ClassValueModel<string>("Exception", "", false, false);
                    exMdl.Value = ex.ToString();
                    var exVMdl = _viewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, null, exMdl);
                    exVMdl.RequestedKind = ControlKinds.Kistl_App_GUI_MultiLineTextboxKind.Find(_frozenCtx);
                    valueModels.Add(exVMdl);
                }

                if (errors != null && errors.ErrorCount > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (DdlReaderError e in errors)
                    {
                        switch (e.ErrorLevel)
                        {
                            case DdlErrorLevel.Error:
                                sb.Append("E: ");
                                break;
                            case DdlErrorLevel.Warning:
                                sb.Append("W: ");
                                break;
                            default:
                                sb.Append("?: ");
                                break;
                        }
                        sb.AppendLine(e.ToString());
                    }
                    var errorsMdl = new ClassValueModel<string>("Errors", "", false, false);
                    errorsMdl.Value = sb.ToString();
                    var errorsVMdl = _viewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, null, errorsMdl);
                    errorsVMdl.RequestedKind = ControlKinds.Kistl_App_GUI_MultiLineTextboxKind.Find(_frozenCtx);
                    valueModels.Add(errorsVMdl);
                }

                if (mddl != null)
                {
                    mddl.Position = 0;
                    StringBuilder sb = new StringBuilder();
                    var sr = new StreamReader(mddl);
                    int counter = 0;
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        sb.AppendLine(string.Format("{0:000}: {1}", ++counter, line));
                    }
                    var mddlMdl = new ClassValueModel<string>("MDDL", "", false, false);
                    mddlMdl.Value = sb.ToString();
                    var mddlVMdl = _viewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, null, mddlMdl);
                    mddlVMdl.RequestedKind = ControlKinds.Kistl_App_GUI_MultiLineTextboxKind.Find(_frozenCtx);
                    valueModels.Add(mddlVMdl);
                }

                var dlg = _viewModelFactory.CreateViewModel<Kistl.Client.Presentables.ValueInputTaskViewModel.Factory>().Invoke(ctx, null, "Fehler beim erstellen des Reports", valueModels, (args) => { });
                _viewModelFactory.ShowModel(dlg, true);
            }
        }

        public virtual void Save(string filename)
        {
            if (filename.ToLower().EndsWith(".rtf"))
            {
                RtfDocumentRenderer rtf = new RtfDocumentRenderer();
                var workingDir = Path.GetDirectoryName(filename);
                rtf.Render(GetDocument(), filename, workingDir);
            }
            else
            {
                PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
                pdf.Document = GetDocument();
                pdf.RenderDocument();
                pdf.Save(filename);
            }
        }

        public virtual Stream GetStream()
        {
            PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = GetDocument();
            pdf.RenderDocument();
            MemoryStream s = new MemoryStream();
            pdf.Save(s, false);
            s.Position = 0;
            return s;
        }

        private class NonClosingMemStream : MemoryStream
        {
            public override void Close()
            {
                // DO NOTHING!
            }
        }

        public virtual Stream GetStreamRtf()
        {
            var rtf = new RtfDocumentRenderer();
            var workingDir = Path.GetTempPath();

            var s = new NonClosingMemStream();
            rtf.Render(GetDocument(), s, workingDir);
            s.Position = 0;
            return s;
        }

        public static string CreateTempFile(string ext, string filename)
        {
            // TODO: Move that to a global helper and delete files on shutdown
            var tmp = Path.GetTempFileName();
            if (File.Exists(tmp)) File.Delete(tmp);
            Directory.CreateDirectory(tmp);
            //_tempDirs.Add(tmp);
            return Path.Combine(tmp, filename);
        }

        public virtual string SaveTemp(string filename)
        {
            var tmp = CreateTempFile("pdf", filename);
            Save(tmp);
            return tmp;
        }

        public virtual string Open(string filename)
        {
            var tmp = CreateTempFile("pdf", filename);
            Save(tmp);
            _fileOpener.ShellExecute(tmp);
            return tmp;
        }

        public virtual string OpenRtf(string filename)
        {
            var tmp = CreateTempFile("rtf", filename);
            Save(tmp);
            _fileOpener.ShellExecute(tmp);
            return tmp;
        }
    }
}
