namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Basic.Parties;

    [Implementor]
    public static class AccountActions
    {
        [Invocation]
        public static void ToString(Account obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name ?? String.Empty;
        }

        [Invocation]
        public static void AssignParties(Account obj)
        {
            var ctx = obj.Context;
            // TODO: Improve this!
            var allParties = ctx.GetQuery<Party>()
                .ToList()
                .Select(p => new { Party = p, Names = GetPartyNames(p) })
                .Where(p => p.Names.Length > 0)
                .ToList();
            foreach (var transaction in ctx.GetQuery<Transaction>().Where(t => t.Party == null))
            {
                var comment = transaction.Comment.ToLower();
                var party = allParties.Where(p => p.Names.Any(n => comment.Contains(n))).ToList();
                if (party.Count == 1)
                {
                    transaction.Party = party.Single().Party;
                }
            }
        }

        private static string[] GetPartyNames(Party p)
        {
            var result = new List<string>();
            if (p is Organization)
            {
                var orgName = (((Organization)p).Name ?? string.Empty).ToLower();
                if (!string.IsNullOrEmpty(orgName))
                {

                    result.Add(orgName);
                    var parts = orgName.Split(' ');
                    if (parts.Length > 1)
                    {
                        result.Add(string.Join(" ", parts.Take(parts.Length - 1)).Trim());
                    }
                }
            }
            else if (p is Person)
            {
                var person = (Person)p;
                var firstName = (person.FirstName ?? string.Empty).ToLower();
                var lastName = (person.LastName ?? string.Empty).ToLower();
                var middleName = (person.MiddleName ?? string.Empty).ToLower();

                result.Add(string.Join(" ", firstName, lastName).Trim());
                result.Add(string.Join(" ", firstName, middleName, lastName).Trim());

                result.Add(string.Join(" ", lastName, firstName).Trim());
                result.Add(string.Join(" ", lastName, firstName, middleName).Trim());
            }
            return result.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }
    }
}
