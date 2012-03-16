using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Common;
using Kistl.API.Utils;

namespace Kistl.Parties.Client.Tests.Stuff
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
        public MockIdentityResolver(Func<IReadOnlyKistlContext> resolverCtxFactory)
            : base(resolverCtxFactory)
        {
        }

        public override Kistl.App.Base.Identity GetCurrent()
        {
            return Resolve("ccnet");
        }
    }
}
