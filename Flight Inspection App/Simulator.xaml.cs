using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using Flight_Inspection_App.Controls;

namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Page
    {
        IViewModel _vm;
        ControlBar cb;
        public Simulator(IViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            cb = new ControlBar(vm);
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start Internet Explorer. Defaults to the home page.
            //  Process.Start("FlightGear - Compositor");

            // Display the contents of the favorites folder in the browser.
            Process.Start(@"C:\Program Files\FlightGear 2020.3.6\bin\fgfs - compositor.exe");
        }

        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public IViewModel getVm()
        {
            return _vm;
        }
        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ControlBar_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void ControlBar_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void ControlBar_Loaded_3(object sender, RoutedEventArgs e)
        {

        }

        private void FilesComponent_Loaded(object sender, RoutedEventArgs e)
        {
            //Load the file
        }

        private void ControlBar_Loaded_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
