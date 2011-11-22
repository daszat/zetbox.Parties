namespace Kistl.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.API.Client;
    using ZBox.Basic.Invoicing;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;
    using System.ComponentModel;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client;

    /// <summary>
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class InvoiceViewModel : DataObjectViewModel
    {
        public new delegate InvoiceViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Invoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();

            ObjectListViewModel vmdl = base.PropertyModelsByName["Items"] as ObjectListViewModel;
            if (vmdl != null)
            {
                var col = vmdl.DisplayedColumns.Columns.SingleOrDefault(c => c.Property.Name == "VATType");
                if (col != null)
                {
                    var kind = FrozenContext.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_ObjectRefDropdownKind);
                    col.GridPreEditKind = kind;
                    col.ControlKind = kind;
                }
            }
        }

        public Invoice Invoice { get; private set; }

        public abstract ViewModel Party { get; }
    }
}
