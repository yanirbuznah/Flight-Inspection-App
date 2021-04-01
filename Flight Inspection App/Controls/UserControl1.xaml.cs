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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;

namespace Flight_Inspection_App.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            Create3DViewPort();
        }
        private void Create3DViewPort()
        {
            ObjReader CurrentHelixObjReader = new ObjReader();
           
            Model3DGroup MyView = CurrentHelixObjReader.Read(@"C:\Users\yanir\source\repos\Flight Inspection App\Flight Inspection App\Images\Heli_bell.obj");
            model.Content = MyView;
           

        }
    }
}
