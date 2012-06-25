
namespace Zetbox.Parties.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.Client.PerfCounter;

    [SetUpFixture]
    public class SetUpFixture : Zetbox.API.AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnActivated(args => args.Instance.Initialize(args.Context.Resolve<IFrozenContext>()))
                .OnRelease(obj => obj.Dump())
                .SingleInstance();

            builder
                .RegisterType<NopFileOpener>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.Parties.Client.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
