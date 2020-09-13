using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace weatherAddIn
{
    public class FiveDayForecast 
    {
        private const string BaseAdress = "http://api.openweathermap.org/data/2.5/";
        private const string ValidCod = "200";
        private const string COD = "cod";
        private const string CoordSelector = "coord";
        private const string WeatherSelector = "weather";
        private const string BaseSelector = "base";
        private const string MainSelector = "main";
        private const string VisibilitySelector = "visibility";
        private const string WindSelector = "wind";
        private const string RainSelector = "rain";
        private const string SnowSelector = "snow";
        private const string CloudsSelector = "clouds";
        private const string IdSelector = "id";
        private const string NameSelector = "name";
        private const string CODSelector = "cod";
        private const string ForecastSelector = "list";
        private const string CitySelector = "city";
        private const string ForecastDatesSelector = "dt";

        public DateTime ForecastDate { get; set; }
        public bool ValidRequest { get; set; }
        public string Base { get; set; }
        public Coord Coord { get; set; }
        public List<Weather> Weathers { get; set; }
        public Weather Description { get; set; }
        public Main Main { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public Clouds Clouds { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
        public City City { get; set; }
        public List<Main> mainList { get; private set; }
        public List<Rain> rainList { get; private set; }
        public List<Snow> snowList { get; private set; }
        public List<Wind> windList { get; private set; }
        public List<Clouds> cloudList { get; private set; }
        public List<DateTime> dateList { get; private set; }


        // var dailyData = forecast[new DateTime(2018, 06, 05)]
        // temp = dailyData.Main.Temp
        // wspd = dailyData.Wind.Speed



        public FiveDayForecast(string apiKey, string queryStr)
        {

            //var weatherForecasts = new List<JToken>();
            Weathers = new List<Weather>();
            mainList = new List<Main>();
            snowList = new List<Snow>();
            rainList = new List<Rain>();
            cloudList = new List<Clouds>();
            dateList = new List<DateTime>();
            windList = new List<Wind>();
            var WeatherForecasts = new List<>();



            JObject response = JObject.Parse(new System.Net.WebClient().DownloadStringTaskAsync(String.Format(FiveDayForecast.BaseAdress + "forecast?appid={0}&q={1}", apiKey, queryStr)).Result);
            var forecastResponse = JArray.Parse(response[ForecastSelector].ToString());

            if (response.SelectToken(COD).ToString() == FiveDayForecast.ValidCod)
            {
                City = new City(response.SelectToken(CitySelector));
                Cod = int.Parse(response.SelectToken(CODSelector).ToString());

                foreach (var item in forecastResponse)
                {
                    ValidRequest = true;

                    foreach (var weather in item[WeatherSelector])
                        Weathers.Add(new Weather(weather));

                    Main = new Main(item[MainSelector]);

                    mainList.Add(Main);

                    Wind = new Wind(item[WindSelector]);
                    windList.Add(Wind);

                    if (item[RainSelector] != null)
                    {
                        Rain = new Rain(item[RainSelector]);
                        rainList.Add(Rain);
                    }

                    if (item[SnowSelector] != null)
                    {
                        Snow = new Snow(item[SnowSelector]);
                        snowList.Add(Snow);
                    }

                    Clouds = new Clouds(item[CloudsSelector]);
                    cloudList.Add(Clouds);

                    ForecastDate = convertUnixToDateTime(double.Parse(item[ForecastDatesSelector].ToString()));
                    dateList.Add(ForecastDate);

                    
                }

            }
            else
            {
                ValidRequest = false;
            }


            }


        private DateTime convertUnixToDateTime(double unixTime)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(unixTime).ToLocalTime();
        }

    }

}