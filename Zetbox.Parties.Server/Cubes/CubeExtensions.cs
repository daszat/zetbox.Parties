using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dasz.LinqCube;
using Kistl.API;
using ZBox.Basic.Parties;

namespace Kistl.Parties.Server.Cubes
{
    public static class CubeExtensions
    {
        public static List<DimensionEntry<string>> BuildCustomerEnum(this DimensionEntry<string> parent, IKistlContext ctx)
        {
            return parent.BuildEnum(ctx.GetQuery<Customer>().Select(c => c.Party).ToList().Select(p => p.ToString()).OrderBy(n => n).ToArray());
        }
    }
}
