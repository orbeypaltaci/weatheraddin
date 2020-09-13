using Newtonsoft.Json.Linq;

namespace weatherAddIn
{
    public class City
    {
        private const string IdSelector = "id";
        private const string CountrySelector = "country";
        private const string NameSelector = "name";
        private const string CoordSelector = "coord";

        public int ID { get; private set; }
        public string Name { get; private set; }
        public Coord Coord { get; private set; }
        public string Country { get; private set; }

        public City(JToken cityData)
        {
            ID = int.Parse(cityData.SelectToken(IdSelector).ToString());
            Name = cityData.SelectToken(NameSelector).ToString();
            Coord = new Coord(cityData.SelectToken(CoordSelector));
            Country = cityData.SelectToken(CountrySelector).ToString();

        }



    }
}