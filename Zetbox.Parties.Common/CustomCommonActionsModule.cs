using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Zetbox.API;

namespace Zetbox.Parties.Common
{
    public class CustomCommonActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(CustomCommonActionsModule).Assembly);

            // Register explicit overrides here
        }
    }
}
