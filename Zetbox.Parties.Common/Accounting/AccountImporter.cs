using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Accounting;
using System.Globalization;
using at.dasz.DocumentManagement;
using Zetbox.App.Base;
using System.IO;

namespace Zetbox.Parties.Common.Accounting
{
    public interface IAccountImporter
    {
        void Import(IZetboxContext ctx, Account account, string fileName);
    }

    public class ImportedTransaction
    {
        public DateTime Date {get;set;}
        public decimal Ammount {get;set;}
        public string Text {get;set;}

        private List<string> _receipts = new List<string>();
        public List<string> Receipts
        {
            get
            {
                return _receipts;
            }
        }

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
        public void Import(IZetboxContext ctx, Account account, string fileName)
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

                    foreach (var receipt in impTx.Receipts)
                    {
                        var file = ctx.Create<at.dasz.DocumentManagement.File>();
                        file.IsFileReadonly = true;
                        file.Name = string.Format("Receipt {0}.txt", impTx.Date.ToShortDateString());
                        using(var stream = new MemoryStream())
                        using (var sw = new StreamWriter(stream))
                        {
                            sw.Write(receipt);
                            sw.Flush();
                            stream.Seek(0, SeekOrigin.Begin);
                            file.Blob = ctx.Find<Blob>(ctx.CreateBlob(stream, file.Name, "text/plain"));
                        }
                        newTx.Documents.Add(file);
                    }
                }
            }
        }

        protected abstract List<ImportedTransaction> Read(string fileName);
    }

}
