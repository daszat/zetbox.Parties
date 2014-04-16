using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class OrganizationActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.Organization obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name ?? String.Empty;
        }
    }
}
