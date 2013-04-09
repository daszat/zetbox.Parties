using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zetbox.Client.ASPNET;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Parties.ASPNET.Models
{
    public class TransactionSearchViewModel : SearchViewModel<Zetbox.Basic.Accounting.Transaction>
    {
        public new delegate TransactionSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public TransactionSearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }
    }
}