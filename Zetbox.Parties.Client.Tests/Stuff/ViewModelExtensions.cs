using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Parties.Client.Tests.Stuff
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
