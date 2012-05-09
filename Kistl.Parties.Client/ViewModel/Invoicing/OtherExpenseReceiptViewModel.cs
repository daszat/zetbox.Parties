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
    /// </summary>
    [ViewModelDescriptor]
    public class OtherExpenseReceiptViewModel : ReceiptViewModel
    {
        public new delegate OtherExpenseReceiptViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public OtherExpenseReceiptViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, OtherExpenseReceipt obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.OtherExpenseReceipt = obj;
        }

        public OtherExpenseReceipt OtherExpenseReceipt { get; private set; }

        public override ViewModel InternalOrganization
        {
            get
            {
                return PropertyModelsByName["IntOrg"];
            }
        }

        public override ViewModel Party
        {
            get
            {
                return PropertyModelsByName["Party"];
            }
        }
    }
}
