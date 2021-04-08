using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Flight_Inspection_App.Controls
{
    /// <summary>
    /// Interaction logic for FeaturesGraphs.xaml
    /// </summary>
    public partial class FeaturesGraphs : UserControl
    {

        public UserControl Graph
        {
            set;get;
        }
        public FeaturesGraphs()
        {

       
            InitializeComponent();
           
          
        }



        public void GenerateGraph(string file)
        {
            Assembly a = Assembly.LoadFile("fdsfds");
            // Get the type to use.
            Type myType = a.GetType("WpfLibrary.Graph");
            UserControl Graph = Activator.CreateInstance(myType) as UserControl;
            // Get the method to call.
/*
            MethodInfo detect = myType.GetMethod("Detect");
            MethodInfo setIntres = myType.GetMethod("setIntrestingFeatureIndex");
            MethodInfo currentTime = myType.GetMethod("CurrentTime");*/


            // Create an instance.

            // Execute the method.
/*            detect.Invoke(Graph, new object[] { @"C:\Users\yanir\Desktop\flightgearProject\anomaly_flight.csv" });
            setIntres.Invoke(Graph, new object[] { 5 });
            currentTime.Invoke(Graph, new object[] { 300 });*/
           
/*            setIntres.Invoke(Graph, new object[] { 0 });*/

        }

        private void Detect(object sender, RoutedEventArgs e)
        {
            //Detector.Children.Add(Graph);

        }
    }
}
