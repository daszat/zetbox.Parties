using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zetbox.Client.ASPNET;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Parties.ASPNET.Models
{
    public class ReceiptSearchViewModel : SearchViewModel<Zetbox.Basic.Invoicing.Receipt>
    {
        public new delegate ReceiptSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ReceiptSearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }
    }
}