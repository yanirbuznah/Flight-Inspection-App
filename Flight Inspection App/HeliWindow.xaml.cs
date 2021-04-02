using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for HeliWindow.xaml
    /// </summary>
    public partial class HeliWindow : Window
    {
        IViewModel _vm;
        public HeliWindow(IViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}
