using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.ASPNET;
using Zetbox.Client.Presentables;
using Zetbox.App.Base;

namespace Zetbox.Parties.ASPNET.Controllers
{
    public class BlobController : ZetboxController
    {
        public BlobController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {
        }

        //
        // GET: /Blob/

        public ActionResult Get(int id)
        {
            var blob = DataContext.Find<Blob>(id);
            return File(blob.GetStream(), blob.MimeType, blob.OriginalName);
        }
    }
}
