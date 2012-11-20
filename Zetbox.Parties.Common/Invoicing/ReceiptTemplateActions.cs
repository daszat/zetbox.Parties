using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using wf = Zetbox.Basic.Workflow;

namespace Zetbox.Basic.Invoicing
{
    [Implementor]
    public static class ReceiptTemplateActions
    {
        [Invocation]
        public static void ToString(ReceiptTemplate obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}, {1}, Total {2}/{3}", obj.Description, obj.Period, obj.TotalNet, obj.Total);
        }

        [Invocation]
        public static void postSet_Total(ReceiptTemplate obj, PropertyPostSetterEventArgs<decimal> e)
        {
            if (obj is OtherIncomeReceiptTemplate || obj is OtherExpenseReceiptTemplate)
            {
                // There is no difference between total & total net
                // sync the props in common to ensure, they are always the same
                // TODO: Enable overriding property setter
                obj.TotalNet = obj.Total;
            }
        }

        [Invocation]
        public static void CreateReceipt(ReceiptTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Invoicing.Receipt> e)
        {
        }

        [Invocation]
        public static void StartWorkflow(ReceiptTemplate obj, MethodReturnEventArgs<Zetbox.Basic.Workflow.WFInstance> e, Zetbox.Basic.Workflow.WFDefinition workflow)
        {
            var ctx = obj.Context;
            var instance = ctx.Create<wf.WFInstance>();
            instance.Payload.SetObject(obj);
            instance.Summary = obj.Description;
            instance.Start(workflow);
            e.Result = instance;
        }

        [Invocation]
        public static void UpdateTotal(ReceiptTemplate obj)
        {
            // Do nothing, work is done in derived classes
        }
    }
}
