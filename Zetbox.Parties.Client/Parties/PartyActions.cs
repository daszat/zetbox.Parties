using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Zetbox.API;
using Zetbox.App.Extensions;
using Zetbox.App.GUI;
using Zetbox.API.Utils;
using Zetbox.Client.Presentables;
using Zetbox.App.Base;
using Zetbox.API.Common;
using Zetbox.Client.Presentables.Parties;
using Zetbox.Basic.HR;

namespace Zetbox.Basic.Parties
{
    [Implementor]
    public class PartyActions
    {
        private static IViewModelFactory _vmf;
        private static IFrozenContext _frozenCtx;
        private static IAssetsManager _assets;

        public PartyActions(IViewModelFactory vmf, IFrozenContext frozenCtx, IAssetsManager assets)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (assets == null) throw new ArgumentNullException("assets");

            _vmf = vmf;
            _frozenCtx = frozenCtx;
            _assets = assets;
        }

        public class RoleSelectionViewModel : ViewModel
        {
            public new delegate RoleSelectionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass targetPropClass);

            public RoleSelectionViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, ObjectClass targetPropClass)
                : base(dependencies, dataCtx, parent)
            {
                TargetPropClass = targetPropClass;
                _name = _assets.GetString(targetPropClass.Module, ZetboxAssetKeys.DataTypes, ZetboxAssetKeys.ConstructNameKey(targetPropClass), targetPropClass.Name);
            }

            public override ControlKind RequestedKind
            {
                get
                {
                    return NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(FrozenContext);
                }
            }

            private string _name;
            public override string Name
            {
                get { return _name; }
            }

            public ObjectClass TargetPropClass { get; private set; }
        }

        [Invocation]
        public static void AddPartyRole(Zetbox.Basic.Parties.Party obj, MethodReturnEventArgs<PartyRole> e)
        {
            var ctx = obj.Context;
            var candidates = new List<RoleSelectionViewModel>();

            // Common first
            if(!obj.PartyRole.Any(r => r is Customer))
                candidates.Add(_vmf.CreateViewModel<RoleSelectionViewModel.Factory>().Invoke(ctx, null, typeof(Customer).GetObjectClass(_frozenCtx)));

            ObjectClass clsPartyRole;
            if (obj is Person)
            {
                clsPartyRole = (ObjectClass)typeof(PersonRole).GetObjectClass(_frozenCtx);
                
                if (!obj.PartyRole.Any(r => r is Employee))
                    candidates.Add(_vmf.CreateViewModel<RoleSelectionViewModel.Factory>().Invoke(ctx, null, typeof(Employee).GetObjectClass(_frozenCtx)));
            }
            else if (obj is Organization)
            {
                clsPartyRole = (ObjectClass)typeof(OrganizationRole).GetObjectClass(_frozenCtx);

                if (!obj.PartyRole.Any(r => r is Supplier))
                    candidates.Add(_vmf.CreateViewModel<RoleSelectionViewModel.Factory>().Invoke(ctx, null, typeof(Supplier).GetObjectClass(_frozenCtx)));
                if (!obj.PartyRole.Any(r => r is InternalOrganization))
                    candidates.Add(_vmf.CreateViewModel<RoleSelectionViewModel.Factory>().Invoke(ctx, null, typeof(InternalOrganization).GetObjectClass(_frozenCtx)));
            }
            else
            {
                throw new InvalidOperationException("Party is of an unknown type");
            }

            List<ObjectClass> subClasses = new List<ObjectClass>();
            clsPartyRole.CollectChildClasses(subClasses, includeAbstract: false);
            // all other
            foreach (var roleCls in subClasses.Except(candidates.Select(c => c.TargetPropClass))
                                              .Except(obj.PartyRole.Select(c => c.GetObjectClass(_frozenCtx)))
                                              .OrderBy(r => r.Name)
                                              .ToList())
            {
                candidates.Add(_vmf.CreateViewModel<RoleSelectionViewModel.Factory>().Invoke(ctx, null, roleCls));
            }

            var selectClass = _vmf
                .CreateViewModel<SimpleSelectionTaskViewModel.Factory>()
                .Invoke(
                    ctx,
                    null,
                    candidates,
                    (chosenClass) =>
                    {
                        if (chosenClass != null && chosenClass.Count() == 1)
                        {
                            var propCls = ((RoleSelectionViewModel)chosenClass.Single()).TargetPropClass;
                            var ifType = propCls.GetDescribedInterfaceType();
                            var newRole = (PartyRole)ctx.Create(ifType);
                            obj.PartyRole.Add(newRole);
                            var partyVmdl = (PartyViewModel)DataObjectViewModel.Fetch(_vmf, ctx, null, obj);
                            partyVmdl.UpdateRoleTabs(newRole);
                            e.Result = newRole; // show result, UpdateTabs cannot show the new tab yet
                        }
                    },
                    null);
            selectClass.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DataObjectSelectionTaskSimpleKind.Find(_frozenCtx);
            _vmf.ShowDialog(selectClass);
        }
    }
}
