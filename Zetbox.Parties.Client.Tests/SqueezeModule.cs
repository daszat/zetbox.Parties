
namespace Zetbox.Parties.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.Parties.Client.Tests.Stuff;
    using Zetbox.API.Client;
    using Zetbox.API.Common;
    
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
