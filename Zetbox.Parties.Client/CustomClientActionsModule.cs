using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client;
using Zetbox.Client.Presentables;

namespace Zetbox.Parties.Client
{
    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(CustomClientActionsModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(CustomClientActionsModule).Assembly);

            // Register explicit overrides here
            moduleBuilder.RegisterType<Zetbox.Parties.Common.Accounting.BACA_AccountImporter>()
                .InstancePerDependency();

            moduleBuilder
                .Register<Reporting.ReportingHost>(c => new Reporting.ReportingHost(
                        c.Resolve<IFileOpener>(),
                        c.Resolve<ITempFileService>()
                    )
                )
                .InstancePerDependency();
        }
    }
}
