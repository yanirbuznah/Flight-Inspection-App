using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        Simulator s;
        IViewModel _vm;

        public HomePage(IViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            s = new Simulator();
            this.NavigationService.Navigate(s);
            _vm.Start();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            _vm.Connect();
           
        }
    }
}
