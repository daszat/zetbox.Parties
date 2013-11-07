

namespace Zetbox.Parties.Client.ViewModel.Invoicing.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using System.Linq.Expressions;
    using Zetbox.Basic.Parties;
    using System.Reflection;
    using Zetbox.App.Base;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;
    
    public static class PartyRoleReferenceViewModelFactory
    {
        public static BaseValueViewModel Create<TObject, TRole>(IViewModelFactory mdlFactory, IZetboxContext ctx, IFrozenContext frozenCtx, ViewModel parent, string label, IDataObject obj, Expression<Func<TObject, PartyRole>> property)
            where TRole : class, PartyRole
            where TObject : class, IDataObject
        {
            var me = (MemberExpression)property.Body;
            var propInfo = (PropertyInfo)me.Member;

            var initialPartyRole = (PartyRole)propInfo.GetValue(obj, null);

            var mdl = new ObjectReferenceValueModel(label, "", false, false, (ObjectClass)NamedObjects.Base.Classes.Zetbox.Basic.Parties.Party.Find(frozenCtx));
            mdl.Value = initialPartyRole != null ? initialPartyRole.Party : null;
            mdl.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Value")
                {
                    if (mdl.Value != null)
                    {
                        var party = (Party)mdl.Value;
                        var partyRole = party.PartyRole.OfType<TRole>().SingleOrDefault();
                        if (partyRole == null)
                        {
                            partyRole = ctx.Create<TRole>();
                            partyRole.Party = party;
                        }
                        propInfo.SetValue(obj, partyRole, null);
                    }
                    else
                    {
                        propInfo.SetValue(obj, null, null);
                    }
                }
            };
            return mdlFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(ctx, parent, mdl);
        }
    }
}
