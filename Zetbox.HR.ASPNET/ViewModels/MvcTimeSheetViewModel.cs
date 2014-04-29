
namespace Zetbox.HR.ASPNET.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using Zetbox.Client.ASPNET;
    using Zetbox.Client.Presentables;

    public class MvcTimeSheetViewModel : GenericDataObjectEditViewModel<CalendarBook, DataObjectViewModel>
    {
        public new delegate MvcTimeSheetViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public MvcTimeSheetViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        protected override CalendarBook CreateNewInstance()
        {
            var result = base.CreateNewInstance();
            return result;
        }
    }
}