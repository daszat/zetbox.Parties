
namespace Kistl.Parties.Client.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Parties.Client.Tests.Stuff;
    using Kistl.Parties.Client.ViewModel;
    using NUnit.Framework;

    public class Parties : AbstractUITest
    {
        [Test]
        public void Query()
        {
            var ctx = GetClientContext();
            var result = ctx.GetQuery<ObjectClass>().FirstOrDefault();
            Assert.That(result, Is.Not.Null);
        }
    }
}
