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
using System.Windows.Shapes;
using Zetbox.Client.GUI;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Parties.Client.ViewModel.Accounting;

namespace Zetbox.Parties.Client.View.Accounting
{
    /// <summary>
    /// Interaction logic for LinkReceiptTransactionDialog.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class LinkReceiptTransactionDialog : WindowView, IHasViewModel<LinkReceiptTransactionViewModel>
    {
        public LinkReceiptTransactionDialog()
        {
            InitializeComponent();
        }

        public LinkReceiptTransactionViewModel ViewModel
        {
            get { return (LinkReceiptTransactionViewModel)DataContext; }
        }
    }
}
