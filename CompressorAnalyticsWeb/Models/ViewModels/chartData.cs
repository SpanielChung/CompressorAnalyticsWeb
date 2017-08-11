using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompressorAnalyticsWeb
{
    public class chartData
    {
        public List<string> labels { get; set; }
        public List<dataset> datasets { get; set; }

        public chartData()
        {
            labels = new List<string>();
            datasets = new List<dataset>();
        }

    }

    public class dataset
    {
        public string label { get; set; }
        public List<decimal> data { get; set; }

        public dataset()
        {
            data = new List<decimal>();
        }

    }
}