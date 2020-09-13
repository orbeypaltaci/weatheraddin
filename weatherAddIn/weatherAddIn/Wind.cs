using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace weatherAddIn
{
    public class Wind
    {
        public double SpeedMetersPerSecond { get; private set; }
        public double SpeedFeetPerSecond { get { return SpeedMetersPerSecond * 3.28084; } }
        public DirectionEnum Direction { get; private set; }
        public double Degree { get; private set; }
        public double Gust { get; private set; }

        public Wind(JToken windData)
        {
            SpeedMetersPerSecond = double.Parse(windData.SelectToken("speed").ToString());

            if(windData.SelectToken("deg") != null)
                Degree = double.Parse(windData.SelectToken("deg").ToString());

            if(windData.SelectToken("gust")!= null)
                Gust = double.Parse(windData.SelectToken("gust").ToString());
        }

        public string directionEnumToString(DirectionEnum dir)
        {
            switch (dir)
            {
                case DirectionEnum.East:
                    return "East";
                case DirectionEnum.East_North_East:
                    return "East North-East";
                case DirectionEnum.East_South_East:
                    return "East South-East";
                case DirectionEnum.North:
                    return "North";
                case DirectionEnum.North_East:
                    return "North East";
                case DirectionEnum.North_North_East:
                    return "North North-East";
                case DirectionEnum.North_North_West:
                    return "North North-West";
                case DirectionEnum.North_West:
                    return "North West";
                case DirectionEnum.South:
                    return "South";
                case DirectionEnum.South_East:
                    return "South East";
                case DirectionEnum.South_South_East:
                    return "South South-East";
                case DirectionEnum.South_South_West:
                    return "South South-West";
                case DirectionEnum.South_West:
                    return "South West";
                case DirectionEnum.West:
                    return "West";
                case DirectionEnum.West_North_West:
                    return "West North-West";
                case DirectionEnum.West_South_West:
                    return "West South-West";
                case DirectionEnum.Unknown:
                    return "Unknown";
                default:
                    return "Unknown";
            }
        }
        private DirectionEnum assignDirection(double degree)
        {
            if (fallsBetween(degree, 348.75, 360))
                return DirectionEnum.North;
            if (fallsBetween(degree, 0, 11.25))
                return DirectionEnum.North;
            if (fallsBetween(degree, 11.25, 33.75))
                return DirectionEnum.North_North_East;
            if (fallsBetween(degree, 33.75, 56.25))
                return DirectionEnum.North_East;
            if (fallsBetween(degree, 56.25, 78.75))
                return DirectionEnum.East_North_East;
            if (fallsBetween(degree, 78.75, 101.25))
                return DirectionEnum.East;
            if (fallsBetween(degree, 101.25, 123.75))
                return DirectionEnum.East_South_East;
            if (fallsBetween(degree, 123.75, 146.25))
                return DirectionEnum.South_East;
            if (fallsBetween(degree, 168.75, 191.25))
                return DirectionEnum.South;
            if (fallsBetween(degree, 191.25, 213.75))
                return DirectionEnum.South_South_West;
            if (fallsBetween(degree, 213.75, 236.25))
                return DirectionEnum.South_West;
            if (fallsBetween(degree, 236.25, 258.75))
                return DirectionEnum.West_South_West;
            if (fallsBetween(degree, 258.75, 281.25))
                return DirectionEnum.West;
            if (fallsBetween(degree, 281.25, 303.75))
                return DirectionEnum.West_North_West;
            if (fallsBetween(degree, 303.75, 326.25))
                return DirectionEnum.North_West;
            if (fallsBetween(degree, 326.25, 348.75))
                return DirectionEnum.North_North_West;
            return DirectionEnum.Unknown;
        }

        private bool fallsBetween (double val, double min, double max)
        {
            if ((min <= val && max >= val))
                return true;
            return false;
        }

    }
}