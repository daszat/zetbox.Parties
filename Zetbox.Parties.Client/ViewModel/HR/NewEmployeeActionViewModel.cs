namespace Zetbox.Parties.Client.ViewModel.HR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Calendar;
    using Zetbox.App.GUI;
    using Zetbox.Basic.HR;
    using Zetbox.Basic.Parties;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ObjectEditor;

    [ViewModelDescriptor]
    public class NewEmployeeActionViewModel : NavigationActionViewModel
    {
        public new delegate NewEmployeeActionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationAction screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public NewEmployeeActionViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, NavigationAction screen, Func<IZetboxContext> ctxFactory)
            : base(dependencies, dataCtx, parent, screen)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            _ctxFactory = ctxFactory;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override string GetReason()
        {
            return string.Empty;
        }

        private const string EmpFirstNameKey = "emp_first_name";
        private const string EmpLastNameKey = "emp_last_name";
        private const string EmpUsernameKey = "emp_username";
        private const string EmpOrgUnitKey = "emp_orgunit";

        public override void Execute()
        {
            // TODO: this context is in the wrong autofac Unit of Work
            // and will therefore fail in the long run.
            var ctx = _ctxFactory();
            var newEmpDlg = ViewModelFactory.CreateDialog(ctx, HRResources.NewEmployeeDialogTitle)
                .AddString(EmpFirstNameKey, HRResources.NewEmployeeFirstNameLabel, allowNullInput: false)
                .AddString(EmpLastNameKey, HRResources.NewEmployeeLastNameLabel, allowNullInput: false)
                .AddString(EmpUsernameKey, HRResources.NewEmployeeUsernameLabel, allowNullInput: false)
                .AddObjectReference(EmpOrgUnitKey, HRResources.NewEmployeeOrgUnitLabel, (ObjectClass)NamedObjects.Base.Classes.Zetbox.Basic.Parties.InternalOrganization.Find(ctx));

            newEmpDlg.AcceptLabel = HRResources.NewEmployeeCreateLabel;

            newEmpDlg.Show((values) =>
            {
                var firstName = (string)values[EmpFirstNameKey];
                var lastName = (string)values[EmpLastNameKey];
                var username = (string)values[EmpUsernameKey];
                var orgUnit = (InternalOrganization)values[EmpOrgUnitKey];
                CreateEmployee(ctx, firstName, lastName, username, orgUnit);

            }, this);

        }

        private void CreateEmployee(IZetboxContext ctx, string firstName, string lastName, string username, InternalOrganization orgUnit)
        {
            var identity = ctx.GetQuery<Identity>().Where(id => id.UserName == username).SingleOrDefault();
            if (identity == null)
            {
                identity = ctx.Create<Identity>();
                identity.UserName = username;
            }
            else
            {
                var existingEmployee = ctx.GetQuery<Employee>().Where(emp => emp.Identity == identity).SingleOrDefault();
                if (existingEmployee == null)
                {
                    // error: username is already assigned to someone else.
                    ViewModelFactory.ShowMessage(string.Format(HRResources.NewEmployeeUsernameAlreadyAssignedMessageFormat, username, existingEmployee.Party.ToString()), HRResources.NewEmployeeUsernameAlreadyAssignedTitle);
                    return;
                }
            }
            var employeePerson = ctx.Create<Person>();
            employeePerson.FirstName = firstName;
            employeePerson.LastName = lastName;
            var employee = ctx.Create<Employee>();
            employee.Party = employeePerson;
            employee.Identity = identity;
            employee.TimeSheet.Name = string.Format("TimeSheet for {0}", employeePerson);
            var employment = employee.Employments.FirstOrDefault() ?? ctx.Create<Employment>();
            employment.InternalOrganization = orgUnit;
            // full time schedule
            employment.Schedule = ctx.FindPersistenceObject<WorkSchedule>(new Guid("a2aaa5e8-283d-4fa1-9448-284b4daf2bb3"));
            // set employee only after initializing the employment
            employment.Employee = employee;

            var ws = ViewModelFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctx, null);
            ws.ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(ws.ViewModelFactory, ctx, ws, employeePerson), activate: true);
            ws.Show = true;
            ViewModelFactory.ShowModel(ws, true);
        }
    }
}
