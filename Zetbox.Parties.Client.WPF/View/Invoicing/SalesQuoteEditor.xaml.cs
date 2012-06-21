using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zetbox.Client.GUI;
using Zetbox.Parties.Client.ViewModel.Invoicing;

namespace Zetbox.Parties.Client.View.Invoicing
{
    /// <summary>
    /// Interaction logic for InvoiceEditor.xaml
    /// </summary>
    [ViewDescriptor(App.GUI.Toolkit.WPF)]
    public partial class SalesQuoteEditor : UserControl, IHasViewModel<SalesQuoteViewModel>
    {
        public SalesQuoteEditor()
        {
            InitializeComponent();
        }

        public SalesQuoteViewModel ViewModel
        {
            get { return (SalesQuoteViewModel)DataContext; }
        }
    }
}
