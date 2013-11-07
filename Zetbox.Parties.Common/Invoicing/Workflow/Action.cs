
namespace Zetbox.Parties.Common.Invoicing.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using wf = Zetbox.Basic.Workflow;
    using Zetbox.App.Base;
    using Zetbox.API;
    using Zetbox.Basic.Invoicing;
    using Zetbox.API.Utils;

    public class Action
    {
        public bool CreateReceipt(wf.Action action, wf.ParameterizedActionDefinition parameter, wf.State current, Identity identity)
        {
            var ctx = current.Context;
            var payload = current.Instance.Payload.GetObject(ctx);
            if (payload is ReceiptTemplate)
            {
                var template = (ReceiptTemplate)current.Instance.Payload.GetObject(ctx);
                template.CreateReceipt();
                return true;
            }
            else
            {
                var msg = string.Format("Payload {0} is not a ReceiptTemplate", payload);
                Logging.Log.Warn(msg);
                current.Instance.AddLogEntry(msg);
                return false;
            }
        }
    }
}
