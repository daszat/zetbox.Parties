
namespace Zetbox.Parties.Client.Reporting
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
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.Calendar;
    using Zetbox.Client.Presentables;

    public abstract class ReportTemplate : Zetbox.API.Common.Reporting.ReportTemplate
    {
        public ReportTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }


        #region Formathelper
        public static string FormatName(Zetbox.Basic.Parties.Party party)
        {
            if (party is Zetbox.Basic.Parties.Organization)
            {
                return FormatName((Zetbox.Basic.Parties.Organization)party);
            }
            else if (party is Zetbox.Basic.Parties.Person)
            {
                return FormatName((Zetbox.Basic.Parties.Person)party);
            }
            return string.Empty;
        }

        public static string FormatName(Zetbox.Basic.Parties.Organization org)
        {
            return org.Name;
        }
        public static string FormatName(Zetbox.Basic.Parties.Person pers)
        {
            List<string> parts = new List<string>();
            parts.Add(pers.PersonalTitle);
            parts.Add(pers.FirstName);
            parts.Add(pers.LastName);
            parts.Add(pers.Suffix);

            return string.Join(" ", parts.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        public static Zetbox.Basic.Parties.Address Coalesce(params Zetbox.Basic.Parties.Address[] addresses)
        {
            foreach (var a in addresses)
            {
                if (a != null && !string.IsNullOrEmpty(a.Line1))
                {
                    return a;
                }
            }

            return null;
        }

        #endregion
    }
}
