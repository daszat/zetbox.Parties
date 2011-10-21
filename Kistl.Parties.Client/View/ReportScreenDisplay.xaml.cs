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
using Kistl.Client.GUI;
using Kistl.Parties.Client.ViewModel;

namespace Kistl.Parties.Client.View
{
    /// <summary>
    /// Interaction logic for ReportScreenDisplay.xaml
    /// </summary>
    [ViewDescriptor(App.GUI.Toolkit.WPF)]
    public partial class ReportScreenDisplay : UserControl, IHasViewModel<ReportScreenViewModel>
    {
        public ReportScreenDisplay()
        {
            InitializeComponent();
        }

        public ReportScreenViewModel ViewModel
        {
            get { return (ReportScreenViewModel)DataContext; }
        }
    }
}
