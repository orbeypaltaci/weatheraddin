using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace weatherAddIn
{
    public class Coord
    {
        private const string LongitudeSelector = "lon";
        private const string LatitudeSelector = "lat";

        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Coord(JToken coordData)
        {
            Longitude = double.Parse(coordData.SelectToken(LongitudeSelector).ToString());
            Latitude = double.Parse(coordData.SelectToken(LatitudeSelector).ToString());
        }
    }
}