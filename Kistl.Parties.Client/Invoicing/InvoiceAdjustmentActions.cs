using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceAdjustmentActions
    {
        [Invocation]
        public static void postSet_Type(InvoiceAdjustment obj, PropertyPostSetterEventArgs<AdjustmentType> e)
        {
            if (e.NewValue == null) return;

            if (!obj.Percentage.HasValue && e.NewValue.Percentage.HasValue)
            {
                obj.Percentage = e.NewValue.Percentage.Value;
                if(obj.Quantity == 0) obj.Quantity = 1;
                if(obj.Invoice != null && obj.Amount == 0)
                    obj.Amount = Math.Round(obj.Invoice.TotalNet * obj.Percentage.Value / (decimal)100.0, 2);
            }
            else if (obj.Amount == 0 && e.NewValue.Absolute.HasValue)
            {
                if (obj.Quantity == 0) obj.Quantity = 1;
                obj.Amount = e.NewValue.Absolute.Value;
            }

            if (string.IsNullOrEmpty(obj.Description) && !string.IsNullOrEmpty(e.NewValue.Description))
            {
                obj.Description = e.NewValue.Description;
            }
        }
    }
}
