using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherTest.Models
{
    public class City
    {
        public int City_id { get; set; }
        public string name { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
    }
}