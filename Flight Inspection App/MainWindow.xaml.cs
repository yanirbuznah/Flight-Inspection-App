﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        HomePage home;
        IViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new FGVM(new FGM(new Client()));
            DataContext = vm;
            home = new HomePage(vm);
            Navigate(home);
        }

        public HomePage GetHomePage()
        {
            return home;
        }
    }
}
