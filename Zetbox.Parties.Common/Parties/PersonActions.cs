using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Zetbox.API;
using Zetbox.App.Extensions;
using Zetbox.App.GUI;
using Zetbox.API.Utils;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public static class PersonActions
    {
        [Invocation]
        public static void ToString(Zetbox.Basic.Parties.Person obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} {1}", obj.FirstName, obj.LastName);
        }

        [Invocation]
        public static void get_LastName(Zetbox.Basic.Parties.Person obj, PropertyGetterEventArgs<System.String> e)
        {
        }

        [Invocation]
        public static void NotifyPreSave(Zetbox.Basic.Parties.Person obj)
        {
        }

        [Invocation]
        public static void postSet_FirstName(Zetbox.Basic.Parties.Person obj, PropertyPostSetterEventArgs<System.String> e)
        {
        }

        [Invocation]
        public static void preSet_FirstName(Zetbox.Basic.Parties.Person obj, PropertyPreSetterEventArgs<System.String> e)
        {
        }
    }
}
