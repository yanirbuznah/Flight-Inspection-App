﻿using OxyPlot;
using System;
using System.Collections.Generic;

namespace Flight_Inspection_App
{

    public class Feature
    {
        public string Name { get; set; }
        private readonly List<double> _Values = new();
        private readonly IList<DataPoint> _Points = new List<DataPoint>();

        public Feature MostCorrelativeFeature
        {
            get;set;
        }
        public Line LineReg
        {
            get;set;
        }

        public List<double> Values {
            get { return _Values; }
        }
        public void AddValue(string value)
        {
            _Values.Add(Double.Parse(value));
        }
        public void AddPoint(int line, string value)
        {
            double x = line / 10;
            double y = Double.Parse(value);
            Points.Add(new DataPoint(x,y));
            _Values.Add(Double.Parse(value));
        }
        public IList<DataPoint> Points
        {
            get { return _Points; }

        }

    }

}
