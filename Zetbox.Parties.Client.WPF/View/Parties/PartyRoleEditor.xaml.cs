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
using Zetbox.Client.Presentables.Parties;

namespace Zetbox.Parties.Client.WPF.View.Parties
{
    /// <summary>
    /// Interaction logic for PartyRoleEditor.xaml
    /// </summary>
    [ViewDescriptor(App.GUI.Toolkit.WPF)]
    public partial class PartyRoleEditor : UserControl, IHasViewModel<PartyRoleViewModel>
    {
        public PartyRoleEditor()
        {
            InitializeComponent();
        }

        public PartyRoleViewModel ViewModel
        {
            get { return (PartyRoleViewModel)DataContext; }
        }
    }
}
