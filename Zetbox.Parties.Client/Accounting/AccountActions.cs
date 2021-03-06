namespace Zetbox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Autofac;
    using Zetbox.Parties.Common.Accounting;
    using Zetbox.Client.Presentables;

    [Implementor]
    public class AccountActions
    {
        private static ILifetimeScope _container;
        private static IViewModelFactory _modelFactory;
        public AccountActions(ILifetimeScope container, IViewModelFactory modelFactory)
        {
            _container = container;
            _modelFactory = modelFactory;
        }

        [Invocation]
        public static void Import(Account obj)
        {
            if (obj.Importer != null)
            {
                var type = Type.GetType(obj.Importer.ImporterName, true);
                var importer = (IAccountImporter)_container.Resolve(type);
                importer.Import(obj.Context, obj, _modelFactory.GetSourceFileNameFromUser());
                _modelFactory.ShowMessage("Transactions successfully imported", "Success");
            }
        }
    }
}
