
// TODO : rename namespace when moving employee
namespace Zetbox.Basic.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Calendar;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Basic.Parties;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.Parties;

    [Implementor]
    public class EmploymentActions
    {
        [Invocation]
        public static void NotifyCreated(Employment obj)
        {
            obj.From = DateTime.Now;
        }
    }
}
