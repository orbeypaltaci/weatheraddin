using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace weatherAddIn
{
    public class Rain
    {
        private string ThreeHour = "3h";
        public double H3;
        
        public Rain(JToken rainData)
        {
            if (rainData.SelectToken(ThreeHour) != null)
                H3 = double.Parse(rainData.SelectToken(ThreeHour).ToString());      
        }
    }
}