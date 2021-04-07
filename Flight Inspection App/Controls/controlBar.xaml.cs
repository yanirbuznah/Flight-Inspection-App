using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Flight_Inspection_App.Controls
{
    /// <summary>
    /// Interaction logic for controlBar.xaml
    /// </summary>
    public partial class ControlBar : UserControl
    {
        private readonly bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
 
        public ControlBar()
        {

            InitializeComponent();
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Start();
        }
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        /*		private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
                {
                    //userIsDraggingSlider = true;
                }

                private void increase_speed(object sender, RoutedEventArgs e)
                {

                }

                private void decrease_speed(object sender, RoutedEventArgs e)
                {

                }

                private void Button_Click(object sender, RoutedEventArgs e)
                {

                }

                private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
                {

                }
                private void silProgress_DragStarted(object sender, DragStartedEventArgs e)
                {
                    //userIsDraggingSlider = true;
                }
                private void silProgress_DragCompleted(object sender, DragStartedEventArgs e)
                {
                //	userIsDraggingSlider = false;
                }
        */
    }

}
