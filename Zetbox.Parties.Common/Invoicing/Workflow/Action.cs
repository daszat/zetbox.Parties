
namespace Zetbox.Parties.Common.Invoicing.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using wf = Zetbox.Basic.Workflow;
    using Zetbox.App.Base;
    using Zetbox.API;

    public class Action
    {
        public bool CreateReceipt(wf.Action action, wf.ParameterizedActionDefinition parameter, wf.State current, Identity identity)
        {
            return true;
        }
    }
}
