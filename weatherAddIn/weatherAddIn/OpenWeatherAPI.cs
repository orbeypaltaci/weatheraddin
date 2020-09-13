using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherAddIn
{
    class OpenWeatherAPI
    {
        private static IAppCache _cache;
        string openWeatherAPIKey;

        public OpenWeatherAPI(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }

        public void updateAPIKey(string apiKey)
        {
            openWeatherAPIKey = apiKey;
        }

        //Returns null if invalid request
        public Query query(string queryStr)
        {
            _cache = new CachingService();
            var key = queryStr;

            return _cache.GetOrAdd(key, () =>
            {
                Query newQuery = new Query(openWeatherAPIKey, queryStr);
                if (newQuery.ValidRequest)
                    return newQuery;
                return null;
            }, new TimeSpan(3,0,0));

           

        }
        public FiveDayForecast fivedayforecast(string queryStr)
        {
            _cache = new CachingService();
            var key = queryStr;

            return _cache.GetOrAdd(key, () =>
            {
                FiveDayForecast newQuery = new FiveDayForecast(openWeatherAPIKey, queryStr);
                if (newQuery.ValidRequest)
                    return newQuery;
                return null;
            }, new TimeSpan(3, 0, 0));

        }





    }
}
