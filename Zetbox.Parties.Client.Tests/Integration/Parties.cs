
namespace Zetbox.Parties.Client.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Parties.Client.Tests.Stuff;
    using Zetbox.Parties.Client.ViewModel;
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
