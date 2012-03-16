using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Parties.Client.Tests.Stuff
{
    public static class ViewModelExtensions
    {
        public static T PropertyModelsByName<T>(this DataObjectViewModel mdl, string prop)
            where T : BaseValueViewModel
        {
            return (T)mdl.PropertyModelsByName[prop];
        }
    }
}
