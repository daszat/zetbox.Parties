
namespace Kistl.Parties.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using NUnit.Framework;
    using ZBox.Basic.Parties;

    [Explicit]
    public class StressTests
        : AbstractTestFixture
    {
        protected Func<IKistlContext> ctxFactory;
        protected IKistlContext ctx;
        public override void SetUp()
        {
            base.SetUp();

            ctxFactory = scope.Resolve<Func<IKistlContext>>();
            ctx = ctxFactory();
        }

        [Test]
        public void StressQueries()
        {
            Console.Out.WriteLine("Starting StressQueries");

            for (int i = 0; i < 1000; i++)
            {
                using (var subScope = scope.BeginLifetimeScope())
                {
                    var ctx = subScope.Resolve<IKistlContext>();
                    var results1 = ctx.GetQuery<Person>().Where(k => k.LastName.Contains("e")).ToList();
                    var results2 = ctx.GetQuery<Person>().Where(k => k.LastName.Contains("r")).ToList();

                    Console.Out.WriteLine("{0:0000}: Selected {1} e-persons and {2} r-persons", i, results1.Count, results2.Count);
                }
            }
            Console.Out.WriteLine("Finished StressQueries");
        }
    }
}
