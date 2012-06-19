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
    public class OtherIncomeReceiptViewModel : ReceiptViewModel
    {
        public new delegate OtherIncomeReceiptViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public OtherIncomeReceiptViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, OtherIncomeReceipt obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.OtherIncomeReceipt = obj;
        }

        public OtherIncomeReceipt OtherIncomeReceipt { get; private set; }

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
