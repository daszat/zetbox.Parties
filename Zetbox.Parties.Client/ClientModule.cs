
namespace Zetbox.Parties.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Common.Reporting;
    using Zetbox.API.Configuration;
    using Zetbox.Client;

    [Feature(NotOnFallback = true)]
    [Description("Parties client module")]
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Zetbox.Parties.Common.CommonModule>();

            moduleBuilder.RegisterZetboxImplementors(typeof(ClientModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);

            // Register explicit overrides here
            moduleBuilder.RegisterType<Zetbox.Parties.Common.Accounting.BACA_AccountImporter>()
                .InstancePerDependency();

            moduleBuilder.RegisterType<Zetbox.Parties.Common.Accounting.MT940_AccountImporter>()
                .InstancePerDependency();

            moduleBuilder
                .Register<Reporting.ReportingHost>(c => new Reporting.ReportingHost(
                        c.Resolve<IFileOpener>(),
                        c.Resolve<ITempFileService>(),
                        c.Resolve<IReportingErrorReporter>()
                    )
                )
                .InstancePerDependency();
        }
    }
}
