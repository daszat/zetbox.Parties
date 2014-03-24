namespace Zetbox.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Basic.Parties;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Parties.Client.ViewModel.Parties;

    [ViewModelDescriptor]
    public class OrganizationViewModel : PartyViewModel
    {
        public new delegate OrganizationViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public OrganizationViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Organization obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

        public Organization Person { get { return (Organization)base.Object; } }

        protected override PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, DataObjectViewModel.PropertyGroupCollection lst)
        {
            if (tag == "Main")
            {
                return UICreator.CustomPropertyGroup(tag, translatedTag, new[] 
                {
                    UICreator.StackPanel(new ViewModel[]
                    {
                        UICreator.GroupBox(PartiesResources.Person_MainGroupLabel, new []
                        {
                            UICreator.Grid(new [] {
                                new GridPanelViewModel.Cell(0, 0, UICreator.StackPanel(new []
                                {
                                    PropertyModelsByName["Name"],
                                    PropertyModelsByName["TaxIDNumber"],
                                    PropertyModelsByName["CompanyRegistrationNumber"],
                                })),
                                new GridPanelViewModel.Cell(0, 1, PropertyModelsByName["Comment"]),
                            }),
                        }),
                        UICreator.GroupBox(PartiesResources.ContactGroupLabel, new []
                        {
                            PropertyModelsByName["EMail"],
                            PropertyModelsByName["Phone"],
                            PropertyModelsByName["Mobile"],
                            PropertyModelsByName["Fax"],
                        }),
                        UICreator.GroupBox(PartiesResources.AddressesGroupLabel, new []
                        {
                            PropertyModelsByName["Address"],
                            PropertyModelsByName["InvoiceAddress"],
                            PropertyModelsByName["DeliveryAddresses"],
                        }),
                        UICreator.GroupBox(PartiesResources.OtherGroupLabel, new []
                        {
                            PropertyModelsByName["BankAccount"],
                        }),
                    }),
                });
            }
            else
            {
                return base.CreatePropertyGroup(tag, translatedTag, lst);
            }
        }
    }
}
