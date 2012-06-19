
namespace Kistl.Parties.Client.Tests.Stuff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Parties.Client.ViewModel;
    using NUnit.Framework;
    
    public abstract class AbstractUITest : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected MockedViewModelFactory mdlFactory;
        protected IFrozenContext frozenContext;
        protected Func<IKistlContext> ctxFactory;
        protected Func<ClientIsolationLevel, IKistlContext> ctxClientFactory;
        

        public override void SetUp()
        {
            base.SetUp();
            mdlFactory = scope.Resolve<MockedViewModelFactory>();
            frozenContext = scope.Resolve<IFrozenContext>();
            ctxFactory = scope.Resolve<Func<IKistlContext>>();
            ctxClientFactory = scope.Resolve<Func<ClientIsolationLevel, IKistlContext>>();
        }

        protected NavigatorViewModel NavigateTo(IKistlContext ctx, params Guid[] path)
        {
            var app = frozenContext.FindPersistenceObject<Application>(new Guid("6be0ba52-4589-48f1-832f-6cd463ba319a"));
            var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctx, null, app);
            var navigator = mdlFactory.CreateViewModel<NavigatorViewModel.Factory>().Invoke(ctx, null, appMdl.RootScreen);

            foreach (var screenGuid in path)
            {
                var screen = navigator.CurrentScreen.Children.Single(s => s.ExportGuid == screenGuid);
                navigator.NavigateTo(screen);
                Assert.That(navigator.CurrentScreen, Is.SameAs(screen));
            }
            return navigator;
        }

        protected IKistlContext GetClientContext()
        {
            return ctxClientFactory(ClientIsolationLevel.MergeServerData);
        }
    }
}
