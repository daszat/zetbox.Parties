#define NO_DATABASE_RESET

namespace Kistl.Parties.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Client.PerfCounter;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.Client;
    using Kistl.Client.Presentables;
    using Kistl.Parties.Client.Tests.Stuff;
    using Kistl.Parties.Client.ViewModel;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Parties.Client.Tests.SetUpFixture");
        private IKistlAppDomain manager;

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder
                .Register(c => new Kistl.Server.SchemaManagement.LoggingSchemaProviderAdapter(new Kistl.Server.SchemaManagement.NpgsqlProvider.Postgresql()))
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

            var config = container.Resolve<KistlConfig>();

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
            return "Kistl.Parties.Client.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}