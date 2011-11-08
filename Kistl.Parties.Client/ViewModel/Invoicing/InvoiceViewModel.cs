namespace Kistl.Parties.Client.ViewModel.Invoicing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.API.Client;
    using ZBox.Basic.Invoicing;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;
    using System.ComponentModel;
    using Kistl.App.GUI;
    using Kistl.App.Extensions;
    using Kistl.Client;

    /// <summary>
    /// Abstract class, no descriptor
    /// </summary>
    public abstract class InvoiceViewModel : DataObjectViewModel
    {
        public new delegate InvoiceViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public InvoiceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Invoice obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.Invoice = obj;
        }

        public Invoice Invoice { get; private set; }

        public abstract ViewModel Party { get; }

        #region new item properties
        private NullableStructValueViewModel<decimal> _amount = null;
        private NullableStructValueModel<decimal> _amountMdl = null;
        public ViewModel Amount
        {
            get
            {
                if (_amount == null)
                {
                    _amountMdl = new NullableStructValueModel<decimal>("Amount", "", false, false);
                    _amount = ViewModelFactory.CreateViewModel<NullableStructValueViewModel<decimal>.Factory>().Invoke(DataContext, this, _amountMdl);
                }
                return _amount;
            }
        }

        private NullableStructValueViewModel<decimal> _unitPrice = null;
        private NullableStructValueModel<decimal> _UnitPriceMdl = null;
        public ViewModel UnitPrice
        {
            get
            {
                if (_unitPrice == null)
                {
                    _UnitPriceMdl = new NullableStructValueModel<decimal>("Unit price", "", true, false);
                    _unitPrice = ViewModelFactory.CreateViewModel<NullableStructValueViewModel<decimal>.Factory>().Invoke(DataContext, this, _UnitPriceMdl);
                    _UnitPriceMdl.PropertyChanged += new PropertyChangedEventHandler(_amountPerUnitMdl_PropertyChanged);
                }
                return _unitPrice;
            }
        }

        void _amountPerUnitMdl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value" 
                && _amountMdl != null 
                && _quantityMdl != null 
                && _UnitPriceMdl.Value.HasValue 
                && _quantityMdl.Value.HasValue)
            {
                _amountMdl.Value = _quantityMdl.Value.Value * _UnitPriceMdl.Value.Value;
            }
        }

        private NullableStructValueViewModel<decimal> _quantity = null;
        private NullableStructValueModel<decimal> _quantityMdl = null;
        public ViewModel Quantity
        {
            get
            {
                if (_quantity == null)
                {
                    _quantityMdl = new NullableStructValueModel<decimal>("Quantity", "", false, false);
                    _quantityMdl.Value = 1;
                    _quantity = ViewModelFactory.CreateViewModel<NullableStructValueViewModel<decimal>.Factory>().Invoke(DataContext, this, _quantityMdl);
                }
                return _quantity;
            }
        }

        private ClassValueViewModel<string> _description = null;
        private ClassValueModel<string> _descriptionMdl = null;
        public ViewModel Description
        {
            get
            {
                if (_description == null)
                {
                    _descriptionMdl = new ClassValueModel<string>("Description", "", false, false);
                    _description = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, _descriptionMdl);
                }
                return _description;
            }
        }

        private NullableBoolPropertyViewModel _taxable = null;
        private BoolValueModel _taxableMdl = null;
        public ViewModel Taxable
        {
            get
            {
                if (_taxable == null)
                {
                    _taxableMdl = new BoolValueModel("Taxable", "", false, false);
                    _taxableMdl.Value = true;
                    _taxable = ViewModelFactory.CreateViewModel<NullableBoolPropertyViewModel.Factory>().Invoke(DataContext, this, _taxableMdl);
                }
                return _taxable;
            }
        }

        private ObjectReferenceViewModel _adjustmentType = null;
        private ObjectReferenceValueModel _adjustmentTypeMdl = null;
        public ViewModel AdjustmentType
        {
            get
            {
                if (_adjustmentType == null)
                {
                    _adjustmentTypeMdl = new ObjectReferenceValueModel("AdjustmentType", "", false, false, FrozenContext.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_ObjectRefDropdownKind), typeof(AdjustmentType).GetObjectClass(FrozenContext));
                    _adjustmentTypeMdl.PropertyChanged += (s, e) => UpdateAdjustmentAmount();
                    _adjustmentType = ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(DataContext, this, _adjustmentTypeMdl);
                }
                return _adjustmentType;
            }
        }

        private NullableStructValueViewModel<decimal> _adjustmentAmount = null;
        private NullableStructValueModel<decimal> _adjustmentAmountMdl = null;
        public ViewModel AdjustmentAmount
        {
            get
            {
                if (_adjustmentAmount == null)
                {
                    _adjustmentAmountMdl = new NullableStructValueModel<decimal>("Adjustment amount", "", false, false);
                    _adjustmentAmount = ViewModelFactory.CreateViewModel<NullableStructValueViewModel<decimal>.Factory>().Invoke(DataContext, this, _adjustmentAmountMdl);
                }
                return _adjustmentAmount;
            }
        }

        #endregion

        #region Helper
        protected void UpdateAdjustmentAmount()
        {
            if (_adjustmentTypeMdl != null && _adjustmentTypeMdl.Value != null && _adjustmentAmountMdl != null)
            {
                var type = (AdjustmentType)_adjustmentTypeMdl.Value;
                if (type.Percentage.HasValue)
                {
                    _adjustmentAmountMdl.Value = Math.Round(Invoice.TotalNet * type.Percentage.Value / (decimal)100.0, 2);
                }
                else if (type.Absolute.HasValue)
                {
                    _adjustmentAmountMdl.Value = type.Absolute.Value;
                }
            }
        }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);
            if (propName == "TotalNet")
            {
                UpdateAdjustmentAmount();
            }
        }
        #endregion

        #region Commands
        private ICommandViewModel _NewItemCommand = null;
        public ICommandViewModel NewItemCommand
        {
            get
            {
                if (_NewItemCommand == null)
                {
                    _NewItemCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "New", "Creates a new Item", NewItem, CanNewItem, null);
                }
                return _NewItemCommand;
            }
        }

        public void NewItem()
        {
            if (CanNewItem())
            {
                var item = Invoice.CreateItem(_quantityMdl.Value.Value, _amountMdl.Value.Value, _descriptionMdl.Value, _taxableMdl.Value.Value);
                if (_UnitPriceMdl != null && _UnitPriceMdl.Value.HasValue)
                {
                    item.UnitPrice = _UnitPriceMdl.Value;
                }
            }
        }

        public bool CanNewItem()
        {
            return _amountMdl != null && _amountMdl.Value != null
                && _quantityMdl != null && _quantityMdl != null
                && _descriptionMdl != null && !string.IsNullOrEmpty(_descriptionMdl.Value);
        }

        private ICommandViewModel _NewAdjustmentCommand = null;
        public ICommandViewModel NewAdjustmentCommand
        {
            get
            {
                if (_NewAdjustmentCommand == null)
                {
                    _NewAdjustmentCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "New", "Creates a new adjustment item", NewAdjustment, CanNewAdjustment, null);
                }
                return _NewAdjustmentCommand;
            }
        }

        public void NewAdjustment()
        {
            if (CanNewAdjustment())
            {
                var item = DataContext.Create<InvoiceAdjustment>();
                Invoice.Items.Add(item);
                item.Type = (AdjustmentType)_adjustmentTypeMdl.Value;
            }
        }

        public bool CanNewAdjustment()
        {
            return _adjustmentTypeMdl != null && _adjustmentTypeMdl.Value != null;
        }
        #endregion
    }
}
