namespace Zetbox.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Basic.Invoicing;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using System.ComponentModel;
    using Zetbox.App.GUI;
    using Zetbox.App.Extensions;
    using Zetbox.Client;

    /// <summary>
    /// </summary>
    [ViewModelDescriptor]
    public class OtherIncomeReceiptViewModel : ReceiptViewModel
    {
        public new delegate OtherIncomeReceiptViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public OtherIncomeReceiptViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, OtherIncomeReceipt obj)
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
