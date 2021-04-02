using System.Windows;
using System.Windows.Controls;


namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Page
    {
        IViewModel _vm;
        public Simulator(IViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public IViewModel getVm()
        {
            return _vm;
        }

        private void HeliWindow(object sender, RoutedEventArgs e)
        {
            HeliWindow hw = new(vm: _vm);
            hw.Show();
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

        }

        private void ControlBar_Loaded_4(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Slider_ValueChanged()
        {

        }
    }
}
