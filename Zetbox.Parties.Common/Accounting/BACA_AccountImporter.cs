using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Accounting;
using System.Globalization;

namespace Zetbox.Parties.Common.Accounting
{
    public class BACA_AccountImporter : AccountImporter
    {
        // 0: Buchungsdatum; 1:Valutadatum; 2:Text1; 3:Interne Notiz; 4:EUR; 5:Betrag;?
        public const int COL_CSV_BUCHUNGSDATUM = 0;
        public const int COL_CSV_VALUTADATUM = 1;
        public const int COL_CSV_BEZ = 2;
        public const int COL_CSV_INT_NOTIZ = 3;
        public const int COL_CSV_BETRAG = 5;

        protected override List<ImportedTransaction> Read(string fileName)
        {
            var result = new List<ImportedTransaction>();

            using (var file = new System.IO.StreamReader(fileName, Encoding.Default))
            {
                if (!file.EndOfStream) file.ReadLine(); // Skip header
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    var columns = line.Split(';');

                    // not used: var buchungsdatum = Convert.ToDateTime(columns[COL_CSV_BUCHUNGSDATUM].Replace('/', '.'));
                    var valutadatum = Convert.ToDateTime(columns[COL_CSV_VALUTADATUM].Replace('/', '.'));
                    var bez = columns[COL_CSV_BEZ].Trim('"');
                    var notiz = columns[COL_CSV_INT_NOTIZ].Trim('"');
                    var betrag = Convert.ToDecimal(columns[COL_CSV_BETRAG]);

                    result.Add(new ImportedTransaction() { Date = valutadatum, Text = bez.Trim() + "\n" + notiz.Trim(), Ammount = betrag });
                }
            }
            return result;
        }
    }
}
