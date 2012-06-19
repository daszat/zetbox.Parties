using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.Parties.Client.Tests.Stuff
{
    public static class CommonExtenstions
    {
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
