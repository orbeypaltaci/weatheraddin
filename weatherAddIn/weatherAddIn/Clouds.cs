using Newtonsoft.Json.Linq;

namespace weatherAddIn
{
    public class Clouds
    {
        private const string AllSelector = "all";

        public double All{get; private set;}
        
        public Clouds(JToken cloudsData)
        {
            All = double.Parse(cloudsData.SelectToken(AllSelector).ToString());
        }
    }
}