using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.ASPNET;
using Zetbox.Parties.ASPNET.Models;
using Zetbox.Client.Presentables;
namespace Zetbox.Parties.ASPNET.Controllers
{
    public class TransactionController : ZetboxController
    {
        public TransactionController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {
        }

        //
        // GET: /Transaction/
        public ActionResult Index()
        {
            return View(ViewModelFactory.CreateViewModel<ReceiptSearchViewModel.Factory>().Invoke(DataContext, null));
        }

        [HttpPost]
        public ActionResult Index(ReceiptSearchViewModel mdl)
        {
            return View(mdl);
        }

    }
}
