using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using ZBox.Basic.Accounting;
using System.Globalization;

namespace Kistl.Parties.Common.Accounting
{
    public interface IAccountImporter
    {
        void Import(IKistlContext ctx, Account account, string fileName);
    }

    public class ImportedTransaction
    {
        public DateTime Date {get;set;}
        public decimal Ammount {get;set;}
        public string Text {get;set;}
        public string ImportHash
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(Date.ToString("u"));
                sb.Append("|");
                sb.Append(Ammount.ToString("n", CultureInfo.InvariantCulture));
                sb.Append("|");
                sb.Append(Text.Replace(" ", ""));
                if (sb.Length > 500)
                    sb.Remove(500, sb.Length - 500);
                return sb.ToString();
            }
        }
    }

    public abstract class AccountImporter : IAccountImporter
    {
        public void Import(IKistlContext ctx, Account account, string fileName)
        {
            List<ImportedTransaction> importedTransactions = Read(fileName);
            var min = importedTransactions.Min(i => i.Date).AddDays(-7);
            var max = importedTransactions.Max(i => i.Date).AddDays(7);

            var transactions = ctx.GetQuery<Transaction>()
                .Where(i => i.Account == account)
                .Where(i => i.Date >= min && i.Date <= max)
                .ToLookup(k => k.ImportHash);

            foreach (var impTx in importedTransactions)
            {
                if (!transactions.Contains(impTx.ImportHash))
                {
                    var newTx = ctx.Create<Transaction>();
                    newTx.Account = account;
                    newTx.Date = impTx.Date;
                    newTx.Amount = impTx.Ammount;
                    newTx.Comment = impTx.Text;
                    newTx.ImportHash = impTx.ImportHash;
                }
            }
        }

        protected abstract List<ImportedTransaction> Read(string fileName);
    }

    public class BACA_AccountImporter : AccountImporter
    {
        // 0: Buchungsdatum; 1:Valutadatum; 2:Auszugsnummer; 3:Text1; 4:?; 5:Betrag; 6:EUR; 7:?
        public const int COL_CSV_BUCHUNGSDATUM = 0;
        public const int COL_CSV_VALUTADATUM = 1;
        public const int COL_CSV_AUSZUGSNUMMER = 2;
        public const int COL_CSV_BEZ1 = 3;
        public const int COL_CSV_BETRAG = 5;

        protected override List<ImportedTransaction> Read(string fileName)
        {
            var result = new List<ImportedTransaction>();

            using (var file = new System.IO.StreamReader(fileName, Encoding.Default))
            {
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    var columns = line.Split(';');

                    var buchungsdatum = Convert.ToDateTime(columns[COL_CSV_BUCHUNGSDATUM]);
                    var valutadatum = Convert.ToDateTime(columns[COL_CSV_VALUTADATUM]);
                    var auszugsnummer = columns[COL_CSV_AUSZUGSNUMMER];
                    var bez = columns[COL_CSV_BEZ1].Trim('"');
                    var betrag = Convert.ToDecimal(columns[COL_CSV_BETRAG]);

                    result.Add(new ImportedTransaction() { Date = valutadatum, Text = bez.Trim(), Ammount = betrag });                    
                }
            }
            return result;
        }
    }
}
