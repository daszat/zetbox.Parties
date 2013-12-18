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
    public class PersonViewModel : PartyViewModel
    {
        public new delegate PersonViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            IDataObject obj);

        public PersonViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Party obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

        public Person Person { get { return (Person)base.Object; } }

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
                                    PropertyModelsByName["Gender"],
                                    PropertyModelsByName["PersonalTitle"],
                                    PropertyModelsByName["FirstName"],
                                    PropertyModelsByName["MiddleName"],
                                    PropertyModelsByName["LastName"],
                                    PropertyModelsByName["Suffix"],
                                    PropertyModelsByName["BirthDate"],
                                })),
                                new GridPanelViewModel.Cell(0, 1, PropertyModelsByName["Comment"]),
                            }),
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
