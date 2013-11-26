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
using Zetbox.Basic.Parties;
using Zetbox.Client.GUI;
using Zetbox.Client.WPF.CustomControls;

namespace Zetbox.Parties.Client.WPF.View.Parties
{
    /// <summary>
    /// Interaction logic for BankAccountEditor.xaml
    /// </summary>
    [ViewDescriptor(App.GUI.Toolkit.WPF)]
    public partial class BankAccountEditor : PropertyEditor, IHasViewModel<BankAccount>
    {
        public BankAccountEditor()
        {
            InitializeComponent();
        }

        public BankAccount ViewModel
        {
            get { return (BankAccount)DataContext; }
        }

        protected override FrameworkElement MainControl
        {
            get { return txtName; }
        }
    }
}
