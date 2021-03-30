using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Flight_Inspection_App.Controls
{
	/// <summary>
	/// Interaction logic for controlBar.xaml
	/// </summary>
	public partial class ControlBar : UserControl
	{
		private bool mediaPlayerIsPlaying = false;
		private bool userIsDraggingSlider = false;
		private IViewModel _vm;
		public ControlBar()
		{

			InitializeComponent();

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Start();
		}
		public ControlBar(IViewModel vm) : this()
		{
			_vm = vm;
		}
		public static void Play()
		{

		}
		public static void Pause()
		{

		}
		public static void Stop()
		{

		}
		public static void SpeedUp()
		{

		}
		public static void SpeedDown()
		{

		}

		private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
		private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{

		}

		private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{

		}

		private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = mediaPlayerIsPlaying;
		}

		private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
		{

			mediaPlayerIsPlaying = false;
		}
		private void SpeedDown_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{

		}

		private void SpeedDown_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
		private void SpeedUp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{

		}

		private void SpeedUp_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
		private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
		{
			userIsDraggingSlider = true;
		}

		private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			userIsDraggingSlider = false;

		}

		private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
		}

		private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
		{

		}

		private void pbVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

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
    }

}
