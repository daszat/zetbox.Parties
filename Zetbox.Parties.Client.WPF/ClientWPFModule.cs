
namespace Zetbox.Parties.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.Client;
    using System.ComponentModel;
    using Zetbox.API.Configuration;

    [Feature(NotOnFallback = true)]
    [Description("Parties WPF module")]
    public class ClientWPFModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Register explicit overrides here

        }
    }
}
