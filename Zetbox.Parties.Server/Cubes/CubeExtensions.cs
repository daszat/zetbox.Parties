using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dasz.LinqCube;
using Zetbox.API;
using Zetbox.Basic.Parties;

namespace Zetbox.Parties.Server.Cubes
{
    public static class CubeExtensions
    {
        public static List<DimensionEntry<string>> BuildCustomerEnum(this DimensionEntry<string> parent, IZetboxContext ctx)
        {
            return parent.BuildEnum(ctx.GetQuery<Customer>().Select(c => c.Party).ToList().Select(p => p.ToString()).OrderBy(n => n).ToArray());
        }
    }
}
