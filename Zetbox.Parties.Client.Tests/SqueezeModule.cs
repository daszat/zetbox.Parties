
namespace Kistl.Parties.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.Parties.Client.Tests.Stuff;
    using Kistl.API.Client;
    using Kistl.API.Common;
    
    public class SqueezeModule : Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<MockCredentialsResolver>()
                .As<ICredentialsResolver>()
                .SingleInstance();

            builder
                .RegisterType<MockIdentityResolver>()
                .As<IIdentityResolver>()
                .SingleInstance();
        }
    }
}
