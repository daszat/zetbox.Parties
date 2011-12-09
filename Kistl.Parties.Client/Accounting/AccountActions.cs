namespace ZBox.Basic.Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Autofac;
    using Kistl.Parties.Common.Accounting;
    using Kistl.Client.Presentables;

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
                var type = obj.Importer.TypeRef.AsType(true);
                var importer = (IAccountImporter)_container.Resolve(type);
                importer.Import(obj.Context, obj, _modelFactory.GetSourceFileNameFromUser());
                _modelFactory.ShowMessage("Transactions successfully imported", "Success");
            }
        }
    }
}
