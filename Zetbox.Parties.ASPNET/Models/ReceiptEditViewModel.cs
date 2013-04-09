using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zetbox.Client.Presentables;
using Zetbox.API;

namespace Zetbox.Parties.ASPNET.Models
{
    public class ReceiptEditViewModel : Zetbox.Client.ASPNET.DataObjectEditViewModel<Zetbox.Basic.Invoicing.Receipt>
    {
        public new delegate ReceiptEditViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ReceiptEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get
            {
                return ReceiptViewModel.Name;
            }
        }

        public Zetbox.Parties.Client.ViewModel.Invoicing.ReceiptViewModel ReceiptViewModel
        {
            get
            {
                return (Zetbox.Parties.Client.ViewModel.Invoicing.ReceiptViewModel)ViewModel;
            }
        }
    }
}