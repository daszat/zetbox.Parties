using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Basic.Accounting;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Zetbox.Parties.Common.Accounting
{
    public class MT940_AccountImporter : AccountImporter
    {
        protected override List<ImportedTransaction> Read(string fileName)
        {
            var result = new List<ImportedTransaction>();
            var txRefs = new Dictionary<string, ImportedTransaction>();
            var receipts = new Dictionary<string, List<MT940ReceiptRecord>>();

            using (var reader = new MT940Reader(fileName))
            {
                MT940Record record;
                ImportedTransaction lastTx = null;
                string lastReceiptRef = null;
                while ((record = reader.GetNextRecord()) != null)
                {
                    if (record is MT940TransactionRecord)
                    {
                        var txRecord = (MT940TransactionRecord)record;
                        lastTx = new ImportedTransaction() { Date = txRecord.Valuta, Ammount = txRecord.AmountWithSign };
                        result.Add(lastTx);
                        if (!string.IsNullOrEmpty(txRecord.ReceiptRef))
                        {
                            txRefs[txRecord.ReceiptRef] = lastTx;
                        }
                    }
                    if (record is MT940TextRecord && lastTx != null)
                    {
                        lastTx.Text = ((MT940TextRecord)record).Text;
                    }
                    if (record is MT940ReceiptRefRecord)
                    {
                        lastReceiptRef = ((MT940ReceiptRefRecord)record).ReceiptRef;
                    }
                    if (record is MT940ReceiptRecord && !string.IsNullOrEmpty(lastReceiptRef))
                    {
                        List<MT940ReceiptRecord> lst;
                        if (!receipts.TryGetValue(lastReceiptRef, out lst))
                        {
                            lst = receipts[lastReceiptRef] = new List<MT940ReceiptRecord>();
                        }
                        lst.Add((MT940ReceiptRecord)record);
                    }
                }

                foreach (var txRef in txRefs)
                {
                    if (receipts.ContainsKey(txRef.Key))
                    {
                        var lst = receipts[txRef.Key];
                        foreach (var r in lst)
                        {
                            txRef.Value.Receipts.Add(r.Receipt);
                        }
                    }
                }
            }

            return result;
        }
    }

    public class MT940Record
    {
        public MT940Record(string raw)
        {
            this.Raw = raw;
        }

        public string Raw { get; private set; }

        public override string ToString()
        {
            return Raw;
        }

    }

    public class MT940OrderReferenceRecord : MT940Record
    {
        public const string FieldNumber = ":20:";

        public MT940OrderReferenceRecord(string raw)
            : base(raw)
        {
            Reference = raw.Substring(FieldNumber.Length);
        }

        public string Reference { get; private set; }

        public override string ToString()
        {
            return "Order-Ref: " + Reference;
        }
    }

    public class MT940ReferenceRecord : MT940Record
    {
        public const string FieldNumber = ":21:";
        public MT940ReferenceRecord(string raw)
            : base(raw)
        {
            Reference = raw.Substring(FieldNumber.Length);
        }

        public string Reference { get; private set; }

        public override string ToString()
        {
            return "Ref: " + Reference;
        }
    }

    public class MT940AccountRecord : MT940Record
    {
        public const string FieldNumber = ":25:";

        public MT940AccountRecord(string raw)
            : base(raw)
        {
            Account = raw.Substring(FieldNumber.Length);
        }

        public string Account { get; private set; }

        public override string ToString()
        {
            return "Account: " + Account;
        }
    }

    public class MT940StatementNumberRecord : MT940Record
    {
        public const string FieldNumber = ":28C:";

        public MT940StatementNumberRecord(string raw)
            : base(raw)
        {
            StatementNumber = raw.Substring(FieldNumber.Length);
        }

        public string StatementNumber { get; private set; }

        public override string ToString()
        {
            return "StatementNumber: " + StatementNumber;
        }
    }

    public class MT940TextRecord : MT940Record
    {
        public const string FieldNumber = ":86:";

        public MT940TextRecord(string raw)
            : base(raw)
        {
            GVCode = raw.Substring(FieldNumber.Length, 3);
            Text = raw.Substring(FieldNumber.Length + 3);
        }

        public string GVCode { get; private set; }
        public string Text { get; private set; }

        public override string ToString()
        {
            return string.Format("GVCode: {0}, Text: {1}", GVCode, Text);
        }
    }

    public class MT940TransactionRecord : MT940Record
    {
        public const string FieldNumber = ":61:";
        public static Regex parser = new Regex(@"^:61:(?<year>\d{2})(?<month>\d{2})(?<day>\d{2})(\d{2})(\d{2})(?<sign>C|RC|D|RD)(\D?)(?<amount>[\d,]*)(N\w\w\w)(?<ref>[^/]*)(//)?(\w*)?([\n\r]*)(?<receipt>\d{8})?");

        public MT940TransactionRecord(string raw)
            : base(raw)
        {
            var match = parser.Match(raw);
            if (match.Success)
            {
                Valuta = new DateTime(int.Parse(match.Groups["year"].Value) + 2000,
                    int.Parse(match.Groups["month"].Value),
                    int.Parse(match.Groups["day"].Value));
                Sign = match.Groups["sign"].Value;
                var fraction = match.Groups["fraction"].Value;
                if (string.IsNullOrEmpty(fraction)) fraction = "0";
                Amount = decimal.Parse(match.Groups["amount"].Value) + (decimal.Parse(fraction) / (decimal)Math.Pow(10, fraction.Length));
                Reference = match.Groups["ref"].Value;
                ReceiptRef = match.Groups["receipt"].Value;
                if (!string.IsNullOrEmpty(ReceiptRef))
                {
                    ReceiptRef = match.Groups["year"].Value + match.Groups["month"].Value + match.Groups["day"].Value + ReceiptRef;
                }
            }
        }

        public DateTime Valuta { get; private set; }
        public string Sign { get; private set; }
        public decimal Amount { get; private set; }
        public string Reference { get; private set; }
        public string ReceiptRef { get; private set; }

        public override string ToString()
        {
            return string.Format("Valuta: {0}, Sign: {1}, Reference: {2}, ReceiptRef: {3}, Amount: {4}", Valuta, Sign, Reference, ReceiptRef, Amount);
        }

        public decimal AmountWithSign
        {
            get
            {
                switch (Sign)
                {
                    case "C":
                        return Amount;
                    case "D":
                        return -Amount;
                    case "RC":
                        return -Amount;
                    case "RD":
                        return Amount;
                }
                return Amount;
            }
        }
    }

    public class MT940ReceiptRefRecord : MT940Record
    {
        public const string FieldNumber = ":61R:";

        public MT940ReceiptRefRecord(string raw)
            : base(raw)
        {
            ReceiptRef = raw.Substring(FieldNumber.Length, 6 + 8);
        }

        public string ReceiptRef { get; private set; }

        public override string ToString()
        {
            return "ReceiptRef: " + ReceiptRef;
        }
    }

    public class MT940ReceiptRecord : MT940Record
    {
        public const string FieldNumber = ":86E:";

        public MT940ReceiptRecord(string raw)
            : base(raw)
        {
            Receipt = raw.Substring(FieldNumber.Length);
        }

        public string Receipt { get; private set; }

        public override string ToString()
        {
            return "Receipt: \n" + Receipt;
        }
    }

    public class MT940Reader : IDisposable
    {
        StreamReader sr;
        Queue<string> buffer = new Queue<string>();
        static Regex fieldNumberRegex = new Regex("^:(\\d\\d\\w?):");

        public MT940Reader(string filename)
        {
            sr = new StreamReader(filename, Encoding.Default);
        }

        private string ReadLine()
        {
            if (buffer.Count > 0)
            {
                return buffer.Dequeue();
            }
            else
            {
                return sr.ReadLine();
            }
        }

        private string PeekLine()
        {
            if (buffer.Count > 0)
            {
                return buffer.Peek();
            }
            else
            {
                var line = sr.ReadLine();
                buffer.Enqueue(line);
                return line;
            }
        }

        public MT940Record GetNextRecord()
        {
            if (sr.EndOfStream) return null;

            StringBuilder sb = new StringBuilder();
            string line;
            do
            {
                line = ReadLine();
                if (sr.EndOfStream) return null;
            } while (string.IsNullOrEmpty(line));

            var fieldNumber = fieldNumberRegex.Match(line);
            if (!fieldNumber.Success)
            {
                return null;
            }
            sb.Append(line);
            while (true)
            {
                if (sr.EndOfStream) break;
                if (fieldNumberRegex.Match(PeekLine()).Success)
                {
                    break;
                }
                else
                {
                    // record continues
                    sb.AppendLine();
                    line = ReadLine();
                    sb.Append(line);
                }
            }

            switch (fieldNumber.Value)
            {
                case MT940OrderReferenceRecord.FieldNumber:
                    return new MT940OrderReferenceRecord(sb.ToString());

                case MT940ReferenceRecord.FieldNumber:
                    return new MT940ReferenceRecord(sb.ToString());

                case MT940AccountRecord.FieldNumber:
                    return new MT940AccountRecord(sb.ToString());

                case MT940StatementNumberRecord.FieldNumber:
                    return new MT940StatementNumberRecord(sb.ToString());

                case MT940TextRecord.FieldNumber:
                    return new MT940TextRecord(sb.ToString());

                case MT940TransactionRecord.FieldNumber:
                    return new MT940TransactionRecord(sb.ToString());

                case MT940ReceiptRefRecord.FieldNumber:
                    return new MT940ReceiptRefRecord(sb.ToString());

                case MT940ReceiptRecord.FieldNumber:
                    return new MT940ReceiptRecord(sb.ToString());

                default:
                    return new MT940Record(sb.ToString());
            }
        }

        public void Dispose()
        {
            sr.Dispose();
        }
    }
}
