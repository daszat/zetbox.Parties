using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API;
using Kistl.API.Client;
using Kistl.Client;
using Kistl.Client.Presentables;

namespace Kistl.Parties.Client
{
    public class CustomClientActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZBoxImplementors(typeof(CustomClientActionsModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(CustomClientActionsModule).Assembly);

            // Register explicit overrides here
            moduleBuilder.RegisterType<Kistl.Parties.Common.Accounting.BACA_AccountImporter>()
                .InstancePerDependency();

            moduleBuilder
                .Register<Reporting.ReportingHost>(c => new Reporting.ReportingHost(
                        c.Resolve<IFileOpener>()
                    )
                )
                .InstancePerDependency();
        }
    }
}
