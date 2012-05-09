
namespace Kistl.Parties.Client.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Text.RegularExpressions;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.Calendar;
    using Kistl.Client.Presentables;

    public abstract class ReportTemplate : CodeTemplate
    {
        public ReportTemplate(IGenerationHost host)
            : base(host)
        {
        }

        protected ReportingHost ReportingHost
        {
            get
            {
                return (ReportingHost)Host;
            }
        }

        public override void Generate()
        {
        }

        #region Images
        protected string ResolveTemplateResourceUrl(string template)
        {
            return String.Format("res://{0}/{1}.{2}", Settings["reporttemplateassembly"], Settings["reporttemplatenamespace"], template);
        }

        protected string GetResourceImageFile(System.Reflection.Assembly assembly,  string image)
        {
            var img = System.Drawing.Bitmap.FromStream(assembly.GetManifestResourceStream(image));
            var result = ReportingHost.CreateTempFile("png", "tmp.png");
            img.Save(result, ImageFormat.Png); // Always convert to a PNG
            return result.Replace('\\', '/');
        }

        protected string GetBlobImageFile(Blob image)
        {
            var imageStream = image.GetStream();
            var ext = "bmp";
            switch (image.MimeType)
            {
                case "image/png":
                    ext = "png";
                    break;
                case "image/bmp":
                    ext = "bmp";
                    break;
                case "image/jpg":
                    ext = "jpg";
                    break;
            }

            using (var tmpFile = File.OpenWrite(ReportingHost.CreateTempFile(ext, "tmp." + ext)))
            {
                imageStream.WriteAllTo(tmpFile);
                return tmpFile.Name.Replace('\\', '/');
            }
        }
        #endregion

        #region Formathelper
        public static string Format(object text)
        {
            return text != null ? text.ToString().Replace("\\", "\\\\") : string.Empty;
        }

        public static string FormatTextfield(string text)
        {
            if (text == null) return "";
            else
            {
                string[] lines = Regex.Split(text, "\r\n");
                string formattedText = "";
                foreach (string line in lines)
                {
                    formattedText += " \\linebreak " + line.Replace("\\", "\\\\");
                }

                return formattedText;
            }
        }

        public static string FormatDate(DateTime? dt)
        {
            return dt != null ? FormatDate(dt.Value) : string.Empty;
        }

        public static string FormatDate(DateTime dt)
        {
            return dt.ToShortDateString();
        }

        public static string Today()
        {
            return FormatDate(DateTime.Today);
        }

        public static string FormatTime(DateTime? dt)
        {
            return dt != null ? FormatTime(dt.Value) : string.Empty;
        }

        public static string FormatTime(DateTime dt)
        {
            return dt.ToShortTimeString();
        }

        public static string FormatWeekday(DateTime? dt)
        {
            return dt != null ? FormatWeekday(dt.Value) : string.Empty;
        }

        public static string FormatWeekday(DateTime dt)
        {
            return dt.ToString("dddd");
        }

        public static string FormatDateRange(string von, DateTime? v, string bis, DateTime? b)
        {
            if (v == null)
            {
                if (b == null) return "";
                else return bis + " " + FormatDate(b);
            }
            else
            {
                if (b == null) return von + " " + FormatDate(v);
                else return von + " " + FormatDate(v) + " " + bis + " " + FormatDate(b);
            }
        }

        public static string FormatPercent(float? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n1}%", betrag.Value) : String.Format("{0:n1}%", "0");
        }

        public static string FormatPercent(double? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n1}%", betrag.Value) : String.Format("{0:n1}%", "0");
        }

        public static string FormatEuro(decimal? betrag)
        {
            return betrag.HasValue ? String.Format("{0:n2} €", betrag.Value) : String.Format("{0:n2} €", "0");
        }

        public static string FormatErrorMessage(string message)
        {
            return @"\bold{\fontcolor(red){" + message + "}}";
        }

        public static string FormatNonBreaking(string s)
        {
            return s.Replace(" ", "\u00A0");
        }

        public static string FormatName(ZBox.Basic.Parties.Party party)
        {
            if (party is ZBox.Basic.Parties.Organization)
            {
                return FormatName((ZBox.Basic.Parties.Organization)party);
            }
            else if (party is ZBox.Basic.Parties.Person)
            {
                return FormatName((ZBox.Basic.Parties.Person)party);
            }
            return string.Empty;
        }

        public static string FormatName(ZBox.Basic.Parties.Organization org)
        {
            return org.Name;
        }
        public static string FormatName(ZBox.Basic.Parties.Person pers)
        {
            List<string> parts = new List<string>();
            parts.Add(FormatAddressGender(pers.Gender));
            parts.Add(pers.PersonalTitle);
            parts.Add(pers.FirstName);
            parts.Add(pers.LastName);
            parts.Add(pers.Suffix);

            return string.Join(" ", parts.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        private static string FormatAddressGender(ZBox.Basic.Parties.Gender? g)
        {
            switch (g)
            {
                case ZBox.Basic.Parties.Gender.Male:
                    return "Herrn";
                case ZBox.Basic.Parties.Gender.Female:
                    return "Frau";
                default:
                    return "Herrn/Frau";
            }
        }
        #endregion

        #region ToDo
        protected string ToDo(string message)
        {
            return @"\bold{\fontcolor(red){TODO: " + message + "!}}";
        }
        #endregion
    }
}
