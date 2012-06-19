
namespace Kistl.Parties.Client.Tests.Stuff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client.PerfCounter;
    using Kistl.API.Configuration;
    using Kistl.Client.Presentables;

    public class MockedViewModelFactory : ViewModelFactory
    {
        public MockedViewModelFactory(ILifetimeScope container, IFrozenContext frozenCtx, KistlConfig cfg, IPerfCounter perfCounter)
            : base(container, frozenCtx, cfg, perfCounter)
        {
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotImplementedException();
        }

        private bool _decisionFromUser = false;
        public void SetDecisionFromUser(bool d)
        {
            _decisionFromUser = d;
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return _decisionFromUser;
        }

        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            throw new NotImplementedException();
        }

        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            throw new NotImplementedException();
        }

        public ViewModel LastShownModel { get; private set; }
        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
            LastShownModel = mdl;
        }

        public string LastShownCaption { get; private set; }
        public string LastShownMessage { get; private set; }
        public override void ShowMessage(string message, string caption)
        {
            LastShownCaption = caption;
            LastShownMessage = message;
        }

        public void ResetMock()
        {
            LastShownMessage = LastShownCaption = string.Empty;
            LastShownModel = null;
        }

        public override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.TEST; }
        }
    }

    public static class MockedViewModelFactoryExtensions
    {
        public static T LastShownModel<T>(this MockedViewModelFactory mdlFact)
            where T : ViewModel
        {
            return (T)mdlFact.LastShownModel;
        }
    }
}
