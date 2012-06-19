
namespace Zetbox.Parties.Client.Tests.Stuff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Parties.Client.ViewModel;
    using NUnit.Framework;
    
    public abstract class AbstractUITest : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected MockedViewModelFactory mdlFactory;
        protected IFrozenContext frozenContext;
        protected Func<IZetboxContext> ctxFactory;
        protected Func<ClientIsolationLevel, IZetboxContext> ctxClientFactory;
        

        public override void SetUp()
        {
            base.SetUp();
            mdlFactory = scope.Resolve<MockedViewModelFactory>();
            frozenContext = scope.Resolve<IFrozenContext>();
            ctxFactory = scope.Resolve<Func<IZetboxContext>>();
            ctxClientFactory = scope.Resolve<Func<ClientIsolationLevel, IZetboxContext>>();
        }

        protected NavigatorViewModel NavigateTo(IZetboxContext ctx, params Guid[] path)
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

        protected IZetboxContext GetClientContext()
        {
            return ctxClientFactory(ClientIsolationLevel.MergeServerData);
        }
    }
}
