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
        /*		private bool userIsDraggingSlider = false;
                private readonly IViewModel _vm;*/
        public ControlBar()
        {

            InitializeComponent();


            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Start();
        }
    }

}
