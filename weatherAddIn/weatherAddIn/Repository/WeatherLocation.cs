using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace weatherAddIn.Repository
{

    internal class WeatherLocation
    {
        internal class Coordinate
        {
            public float Lat { get; set; }
            public float Lon { get; set; }
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Coordinate Coord { get; set; }
        
        


    }
}
