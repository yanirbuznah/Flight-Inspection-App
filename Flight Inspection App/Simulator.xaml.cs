using OxyPlot.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Page
    {
        FGVM _vm;
        ControlBarVM _cbvm;
        FileComponentVM _fcvm;
        FeaturesPanelVM _fpvm;
        FeaturesGraphsVM _fegvm;
        FlightInstrumentsVM _fivm;
        JoystickVM _jvm;
        Annotation _fixedAnnotation;
        public Simulator(FGVM vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = vm;
            _cbvm = new ControlBarVM((_vm as FGVM).getModel());
            _fcvm = new FileComponentVM((_vm as FGVM).getModel());
            _fpvm = new FeaturesPanelVM((_vm as FGVM).getModel());
            _fegvm = new FeaturesGraphsVM((_vm as FGVM).getModel());
            _fivm = new FlightInstrumentsVM((_vm as FGVM).getModel());
            _jvm = new JoystickVM((_vm as FGVM).getModel());
            controlbar.DataContext = _cbvm;
            fileselector.DataContext = _fcvm;
            features.DataContext = _fpvm;
            _fpvm.IntrestingPropertyChanged += ChangeAnnoatation;
            featuregraphs.DataContext = _fegvm;
            flightinstruments.DataContext = _fivm;
            joystick.DataContext = _jvm;
            _fixedAnnotation = featuregraphs.MyPlot.Annotations[0];
            


        }
        
        public void ChangeAnnoatation(object sender, PropertyChangedEventArgs e)
        {

            if(_fegvm.VM_Annotation!= null)
            {
                featuregraphs.MyPlot.Annotations.Clear();
                featuregraphs.MyPlot.Annotations.Add(_fixedAnnotation);
                featuregraphs.MyPlot.Annotations.Add(_fegvm.VM_Annotation);
            }
            
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
