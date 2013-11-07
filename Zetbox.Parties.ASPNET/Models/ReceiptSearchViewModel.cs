using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zetbox.Client.ASPNET;
using Zetbox.API;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.Models;
using Zetbox.Basic.Invoicing;

namespace Zetbox.Parties.ASPNET.Models
{
    public class ReceiptSearchViewModel : SearchViewModel<Zetbox.Basic.Invoicing.Receipt>
    {
        public new delegate ReceiptSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ReceiptSearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        private StringValueViewModel _InvoiceID;
        public StringValueViewModel InvoiceID
        {
            get
            {
                if (_InvoiceID == null)
                {
                    _InvoiceID = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(Resources.InvoiceID, "", true, false));
                }
                return _InvoiceID;
            }
        }

        private StringValueViewModel _Period;
        public StringValueViewModel Period
        {
            get
            {
                if (_Period == null)
                {
                    _Period = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(Resources.Period, "", true, false));
                }
                return _Period;
            }
        }

        private StringValueViewModel _Description;
        public StringValueViewModel Description
        {
            get
            {
                if (_Description == null)
                {
                    _Description = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(Resources.Description, "", true, false));
                }
                return _Description;
            }
        }

        private NullableDateTimePropertyViewModel _DateFrom;
        public NullableDateTimePropertyViewModel DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {
                    _DateFrom = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this, new DateTimeValueModel(Resources.From, "", true, false));
                }
                return _DateFrom;
            }
        }

        private NullableDateTimePropertyViewModel _DateUntil;
        public NullableDateTimePropertyViewModel DateUntil
        {
            get
            {
                if (_DateUntil == null)
                {
                    _DateUntil = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this, new DateTimeValueModel(Resources.Until, "", true, false));
                }
                return _DateUntil;
            }
        }

        protected override IQueryable<Basic.Invoicing.Receipt> ApplyFilter(IQueryable<Basic.Invoicing.Receipt> qry)
        {
            if (!string.IsNullOrWhiteSpace(InvoiceID.Value))
            {
                var str = InvoiceID.Value.ToLower();
                qry = qry.OfType<Invoice>().Where(i => i.InvoiceID.ToLower().Contains(str));
            }

            if (!string.IsNullOrWhiteSpace(Period.Value))
            {
                var str = Period.Value.ToLower();
                qry = qry.Where(i => i.Period.ToLower().Contains(str));
            }

            if (!string.IsNullOrWhiteSpace(Description.Value))
            {
                var str = Description.Value.ToLower();
                qry = qry.Where(i => i.Description.ToLower().Contains(str));
            }

            if (DateFrom.HasValue)
            {
                var dt = DateFrom.Value.Value.Date;
                qry = qry.Where(i => i.Date >= dt || i.DueDate >= dt);
            }

            if (DateUntil.HasValue)
            {
                var dt = DateUntil.Value.Value.Date.AddDays(1);
                qry = qry.Where(i => i.Date < dt || i.DueDate < dt);
            }

            return qry;
        }
    }
}