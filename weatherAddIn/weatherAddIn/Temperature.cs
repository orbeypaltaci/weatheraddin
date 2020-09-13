using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherAddIn
{
   public class Temperature
    {
        private double current_kel_temp, temp_kel_min, temp_kel_max;

        public double celsiusCurrent { get; private set; }
        public double FahrenheitCurrent { get; private set; }
        public double KelvinCurrent
        {
            get
            {
                return current_kel_temp;
            }
            set
            {
                current_kel_temp = value;
                celsiusCurrent = convertToCelsius(value);
                FahrenheitCurrent = convertToFahrenheit(value);
            }
        }
        public double celsiusMinimum { get; private set; }
        public double celsiusMaximum { get; private set; }
        public double FahrenheitMinimum { get; private set; }
        public double FahrenheitMaximum { get; private set; }

        public double KelvinMinimum
        {
            get
            {
                return temp_kel_min;
            }
            set
            {
                temp_kel_min = value;
                celsiusMinimum = convertToCelsius(value);
                FahrenheitMinimum = convertToFahrenheit(celsiusMinimum);

            }
        }
        
        public double KelvinMaximum
        {
            get
            {
                return temp_kel_max;
            }
            set
            {
                temp_kel_max = value;
                celsiusMaximum = convertToCelsius(value);
                FahrenheitMaximum = convertToFahrenheit(celsiusMaximum);
            }
        }
        public Temperature(double temp, double min, double max)
        {
            KelvinCurrent = temp;
            KelvinMaximum = max;
            KelvinMinimum = min;
        }
        private double convertToFahrenheit(double celsius)
        {
            return Math.Round(((9.0 / 5.0) * celsius) + 32, 3 );
        }
        private double convertToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 3);
        }
    }
}
