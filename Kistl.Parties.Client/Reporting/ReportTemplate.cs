
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

    public abstract class ReportTemplate : Kistl.API.Common.Reporting.ReportTemplate // Derive from zetbox class
    {
        public ReportTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }


        #region Formathelper
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
            parts.Add(pers.PersonalTitle);
            parts.Add(pers.FirstName);
            parts.Add(pers.LastName);
            parts.Add(pers.Suffix);

            return string.Join(" ", parts.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        public static ZBox.Basic.Parties.Address Coalesce(params ZBox.Basic.Parties.Address[] addresses)
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
