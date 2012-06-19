using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.API.Common;
using Zetbox.API.Utils;

namespace Zetbox.Parties.Client.Tests.Stuff
{
    public class MockCredentialsResolver : ICredentialsResolver
    {
        public void EnsureCredentials()
        {
        }

        public void InitCredentials(System.ServiceModel.Description.ClientCredentials c)
        {
        }

        public void InitWebRequest(System.Net.WebRequest req)
        {
            req.Credentials = new System.Net.NetworkCredential("ccnet", "plok");
        }

        public void InvalidCredentials()
        {
            Logging.Client.Fatal("Invalid credentials was reported");
        }
    }

    public class MockIdentityResolver : BaseIdentityResolver
    {
        public MockIdentityResolver(Func<IReadOnlyZetboxContext> resolverCtxFactory)
            : base(resolverCtxFactory)
        {
        }

        public override Zetbox.App.Base.Identity GetCurrent()
        {
            return Resolve("ccnet");
        }
    }
}
