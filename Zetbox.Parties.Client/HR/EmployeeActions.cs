
namespace Zetbox.Basic.HR
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
    public class EmployeeActions
    {
        [Invocation]
        public static void NotifyCreated(Employee obj)
        {
            obj.TimeSheet = obj.Context.Create<CalendarBook>();
            obj.Employments.Add(obj.Context.Create<Employment>());
        }

        [Invocation]
        public static void postSet_Party(Employee obj, PropertyPostSetterEventArgs<Party> e)
        {
            obj.TimeSheet.Name = string.Format("TimeSheet for {0}", obj.Party);
        }
    }
}
