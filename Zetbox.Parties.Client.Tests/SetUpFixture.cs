#define NO_DATABASE_RESET

namespace Zetbox.Parties.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.Client;
    using Zetbox.Client.Presentables;
    using Zetbox.Parties.Client.Tests.Stuff;
    using Zetbox.Parties.Client.ViewModel;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : Zetbox.API.AbstractConsumerTests.AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Parties.Client.Tests.SetUpFixture");
        private IZetboxAppDomain manager;

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder
                .Register(c => new Zetbox.Server.SchemaManagement.LoggingSchemaProviderAdapter(new Zetbox.Server.SchemaManagement.NpgsqlProvider.Postgresql()))
                .As<ISchemaProvider>()
                .Named<ISchemaProvider>("POSTGRESQL")
                .InstancePerDependency();

            builder
                .RegisterType<MockedViewModelFactory>()
                .As<MockedViewModelFactory>()
                .As<IViewModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<PerfCounterDispatcher>()
                .As<IPerfCounter>()
                .OnActivated(args => args.Instance.Initialize(args.Context.Resolve<IFrozenContext>()))
                .OnRelease(obj => obj.Dump())
                .SingleInstance();
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);

            var config = container.Resolve<ZetboxConfig>();

            if (config.Server != null && config.Server.StartServer)
            {
#if !NO_DATABASE_RESET
                ResetDatabase(container);
#endif
                using (Log.InfoTraceMethodCall("Starting server domain"))
                {
                    manager = new ServerDomainManager();
                    manager.Start(config);
                }
            }
        }

        public override void TearDown()
        {
            lock (typeof(SetUpFixture))
            {
                if (manager != null)
                {
                    using (Log.InfoTraceMethodCall("Shutting down"))
                    {
                        manager.Stop();
                        manager = null;
                    }
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            TearDown();
        }

        #endregion

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
