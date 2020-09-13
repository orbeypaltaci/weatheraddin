using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;


namespace weatherAddIn
{
    public partial class Ribbon1
    {
        private Dictionary<long, string> cityData = new Dictionary<long, string>();
        private string apiKey;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            fahren_toggle.Checked = Properties.Settings.Default.IsFahrenheit;
            celsius_toggle.Checked = Properties.Settings.Default.IsCelsius;
            kelvin_toggle.Checked = Properties.Settings.Default.IsKelvin;
            apiKey = Properties.Settings.Default.ApiKey;
            //fahren_toggle.Checked = Properties.Settings.Default.IsFahrenheit;

            ImportCityList();
        }

        private void Weather_Button_Click(object sender, RibbonControlEventArgs e)
        {
            var key = getAPIKey();

            if (string.IsNullOrEmpty(key))
                return;

            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();
            Microsoft.Office.Interop.Excel.Range xlRange = currentSheet.UsedRange;
            InitializeMyDomainFields();
            //int maxCityCount = 100;//count of cities method?
            int maxCol = myDomainFields.AllKeys.Length;//can also be a constant.
            int lastRow = xlRange.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
            int lastColumn = xlRange.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Column;
            int fRow = xlRange.Row;
            int fColumn = xlRange.Column;
            var valuesOfCity = myDomainFields.GetValues("City");
            int cityCounter_h = 0;
            int cityCounter_v = 0;
            Boolean isHorizontal = true;

            for (int rowIndex = fRow; rowIndex <= lastRow; rowIndex++)
            {
                for (int columnIndex = fColumn; columnIndex <= lastColumn; columnIndex++)
                {
                    if (Array.IndexOf(valuesOfCity, currentSheet.Cells[rowIndex, columnIndex].Value) >= 0)
                    {
                        int cityRow = rowIndex;
                        int cityColumn = columnIndex;
                        int fCity_row = rowIndex;
                        int fCity_column = columnIndex;
                        if (IsCity(currentSheet.Cells[fCity_row + 1, cityColumn].Value))
                            isHorizontal = true;
                        else if (IsCity(currentSheet.Cells[cityRow, fCity_column + 1].Value))
                            isHorizontal = false;

                        while (currentSheet.Cells[fCity_row, cityColumn].Value != null && isHorizontal == true)//and horizontal true
                        {

                            fCity_row++;
                            if (IsCity(currentSheet.Cells[fCity_row, cityColumn].Value) && !IsDataName(currentSheet.Cells[fCity_row, cityColumn].Value))
                            {
                                cityCounter_h++;
                            }

                        }

                        while (currentSheet.Cells[cityRow, fCity_column].Value != null && isHorizontal == false)//and horizontal false
                        {

                            fCity_column++;
                            if (IsCity(currentSheet.Cells[cityRow, fCity_column].Value) && !IsDataName(currentSheet.Cells[cityRow, fCity_column].Value))
                            {
                                cityCounter_v++;
                            }
                        }
                        DetectFieldsAndGetDataHorizontally(cityRow, cityCounter_h, cityColumn, countOfFieldsHorizontal(cityRow, cityColumn, maxCol));
                        DetectFieldsAndGetDataVertically(cityRow, cityColumn, cityCounter_v, countOfFieldsVertical(cityRow, cityColumn, maxCol));

                    }
                }

            }

        }//btn

        //TODO: make methods async if possible.
        private void DetectFieldsAndGetDataHorizontally(int cityRow, int cityCounter, int cityColumn, int maxFieldCount)
        {

            var valuesOfTemperature = myDomainFields.GetValues("Temperature");
            var valuesOfWindSpeed = myDomainFields.GetValues("Wind Speed");
            var valuesOfHumidity = myDomainFields.GetValues("Humidity");
            var valuesOfSunset = myDomainFields.GetValues("Sunset");
            var valuesOfDescription = myDomainFields.GetValues("Description");
            var valuesOfCity = myDomainFields.GetValues("City");


            OpenWeatherAPI openWeatherAPI = new OpenWeatherAPI(getAPIKey());
            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();

            for (int fieldIndex_R = cityRow; fieldIndex_R <= cityRow + cityCounter; fieldIndex_R++)
            {
                if (IsCity(currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value))
                {
                    //isHorizontal = true;
                    Query query = openWeatherAPI.query(currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value);

                    for (int fieldIndex_C = cityColumn + 1; fieldIndex_C <= cityColumn + maxFieldCount; fieldIndex_C++)
                    {
                        if (Array.IndexOf(valuesOfTemperature, currentSheet.Cells[cityRow, fieldIndex_C].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R + 1, fieldIndex_C].Value = checkedTempUnitIntoQuery(query.Main.Temperature.KelvinCurrent);

                        if (Array.IndexOf(valuesOfHumidity, currentSheet.Cells[cityRow, fieldIndex_C].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R + 1, fieldIndex_C].Value = query.Main.Humidity;

                        if (Array.IndexOf(valuesOfSunset, currentSheet.Cells[cityRow, fieldIndex_C].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R + 1, fieldIndex_C].Value = query.Sys.Sunset;

                        if (Array.IndexOf(valuesOfDescription, currentSheet.Cells[cityRow, fieldIndex_C].Value) >= 0)//TODO:Add icon later.
                            currentSheet.Cells[fieldIndex_R + 1, fieldIndex_C].Value = query.Weathers[0].Description;

                        if (Array.IndexOf(valuesOfWindSpeed, currentSheet.Cells[cityRow, fieldIndex_C].Value) >= 0)//TODO:Add icon.
                            currentSheet.Cells[fieldIndex_R + 1, fieldIndex_C].Value = query.Wind.SpeedMetersPerSecond;

                    }
                }

                else if (Array.IndexOf(valuesOfCity, currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value) >= 0)
                    break;
                else
                    continue;

            }
            currentSheet.Columns.AutoFit();
        }

        private int countOfFieldsHorizontal(int cityRow, int cityColumn, int maxCol)
        {
            int maxFieldCount = 0;
            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();
            maxCol = myDomainFields.AllKeys.Length;
            for (int fieldCounterC = cityColumn; fieldCounterC <= cityColumn + maxCol; fieldCounterC++)
            {
                if (currentSheet.Cells[cityRow, fieldCounterC].Value != null)
                {
                    maxFieldCount++;
                }
                else
                    break;
            }
            return maxFieldCount;
        }
        private int countOfFieldsVertical(int cityRow, int cityColumn, int maxCol)
        {
            int maxFieldCount = 0;
            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();
            maxCol = myDomainFields.AllKeys.Length;
            for (int fieldCounterR = cityRow; fieldCounterR <= cityRow + maxCol; fieldCounterR++)
            {
                if (currentSheet.Cells[fieldCounterR, cityColumn].Value != null)
                {
                    maxFieldCount++;
                }
                else
                    break;
            }
            return maxFieldCount;
        }

        private void DetectFieldsAndGetDataVertically(int cityRow, int cityColumn, int cityCounter_v, int maxFieldCount_v)
        {
            var valuesOfCity = myDomainFields.GetValues("City");
            var valuesOfTemperature = myDomainFields.GetValues("Temperature");
            var valuesOfWindSpeed = myDomainFields.GetValues("Wind Speed");
            var valuesOfHumidity = myDomainFields.GetValues("Humidity");
            var valuesOfSunset = myDomainFields.GetValues("Sunset");
            var valuesOfDescription = myDomainFields.GetValues("Description");

            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();


            OpenWeatherAPI openWeatherAPI = new OpenWeatherAPI(getAPIKey());


            for (int fieldIndex_C = cityColumn; fieldIndex_C <= cityColumn + cityCounter_v; fieldIndex_C++)
            {
                if (IsCity(currentSheet.Cells[cityRow, fieldIndex_C + 1].Value))
                {
                    Query query = openWeatherAPI.query(currentSheet.Cells[cityRow, fieldIndex_C + 1].Value);

                    for (int fieldIndex_R = cityRow + 1; fieldIndex_R <= cityRow + maxFieldCount_v; fieldIndex_R++)
                    {
                        if (Array.IndexOf(valuesOfTemperature, currentSheet.Cells[fieldIndex_R, cityColumn].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R, fieldIndex_C + 1].Value = checkedTempUnitIntoQuery(query.Main.Temperature.KelvinCurrent);

                        if (Array.IndexOf(valuesOfDescription, currentSheet.Cells[fieldIndex_R, cityColumn].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R, fieldIndex_C + 1].Value = query.Weathers[0].Description;

                        if (Array.IndexOf(valuesOfHumidity, currentSheet.Cells[fieldIndex_R, cityColumn].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R, fieldIndex_C + 1].Value = query.Main.Humidity;

                        if (Array.IndexOf(valuesOfSunset, currentSheet.Cells[fieldIndex_R, cityColumn].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R, fieldIndex_C + 1].Value = query.Sys.Sunset;

                        if (Array.IndexOf(valuesOfWindSpeed, currentSheet.Cells[fieldIndex_C, cityColumn].Value) >= 0)
                            currentSheet.Cells[fieldIndex_R, fieldIndex_C + 1].Value = query.Wind.SpeedMetersPerSecond;


                    }
                }
                else if (Array.IndexOf(valuesOfCity, currentSheet.Cells[cityRow, fieldIndex_C + 1].Value) >= 0)
                    break;

                else
                    continue;
            }
            currentSheet.Columns.AutoFit();

        }

        private NameValueCollection myDomainFields;

        private void InitializeMyDomainFields()
        {
            var fields = new NameValueCollection();

            fields.Add("City", "Şehir");
            fields.Add("City", "Sehir");
            fields.Add("City", "City");
            fields.Add("Temperature", "Temperature");
            fields.Add("Temperature", "Temp");
            fields.Add("Temperature", "Sicaklik");
            fields.Add("Temperature", "Sıcaklık");
            fields.Add("Wind Speed", "Rüzgar Hızı");
            fields.Add("Wind Speed", "Esinti Hızı");
            fields.Add("Wind Speed", "Wind Speed");
            fields.Add("Humidity", "Nem");
            fields.Add("Humidity", "Humi");
            fields.Add("Humidity", "Humidity");
            fields.Add("Sunset", "Sunset");
            fields.Add("Sunset", "Gün Batımı");
            fields.Add("Sunset", "Güneş Batış Zamanı");
            fields.Add("Description", "Description");
            fields.Add("Description", "Hava Tanımı");
            fields.Add("Description", "Hava Özeti");

            myDomainFields = fields;
        }

        private string FindDomainField(string name)
        {
            foreach (var field in myDomainFields.AllKeys)
            {
                //TODO: Make this better
                if (myDomainFields[field].Contains(name))
                    return field;
            }

            return null;
        }

        private bool IsDataName(string text)
        {
            return !string.IsNullOrEmpty(FindDomainField(text));
        }

        private bool IsCity(string name)
        {

            return cityData.ContainsValue(name);
        }

        private async void ImportCityList()
        {
            const string JSONFILE = @"C:\Users\orbey\source\repos\deneme\deneme\citylist\city.list.json";

            cityData = await Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();

                Debug.WriteLine("Loading of cities started");
                var serializer = new JsonSerializer();

                var cities = new Dictionary<long, string>();

                using (var file = File.OpenRead(JSONFILE))
                {
                    using (var sr = new StreamReader(file))
                    {
                        using (var reader = new JsonTextReader(sr))
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.StartObject)
                                {
                                    var location = serializer.Deserialize<Repository.WeatherLocation>(reader);
                                    cities.Add(location.Id, location.Name);


                                }
                            }
                        }
                    }
                }
                Debug.WriteLine("Cities loaded in " + sw.Elapsed);

                return cities;
            });

        }

        private string getAPIKey()
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                using (var cfgForm = new API_Form())
                {
                    var result = cfgForm.ShowDialog();

                    if (result != DialogResult.OK)
                        return null;

                    apiKey = Properties.Settings.Default.ApiKey;
                }
            }

            return apiKey;

        }
        private void showForm()
        {
            using (var cfgForm = new API_Form())
            {
                var result = cfgForm.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                apiKey = Properties.Settings.Default.ApiKey;
            }
        }

        private double checkedTempUnitIntoQuery(double dfltTemp)
        {
            if (fahren_toggle.Checked)
            {
                return Math.Round(((9.0 / 5.0) * (dfltTemp - 273.15)) + 32, 3);
            }


            if (celsius_toggle.Checked)
            {
                return Math.Round(dfltTemp - 273.15, 3);
            }


            if (kelvin_toggle.Checked)
            {

                return dfltTemp;
            }
            return dfltTemp;
        }

        private void fahren_toggle_Click(object sender, RibbonControlEventArgs e)
        {
            celsius_toggle.Checked = false;
            fahren_toggle.Checked = false;
            kelvin_toggle.Checked = false;

            ((RibbonToggleButton)sender).Checked = true;

            Properties.Settings.Default.IsFahrenheit = fahren_toggle.Checked;
            Properties.Settings.Default.IsCelsius = celsius_toggle.Checked;
            Properties.Settings.Default.IsKelvin = kelvin_toggle.Checked;
            Properties.Settings.Default.Save();
        }




        private void settingsButton_Click_1(object sender, RibbonControlEventArgs e)
        {
            showForm();
        }

        private void Forecast_Button_Click(object sender, RibbonControlEventArgs e)
        {
            InitializeMyDomainFields();
            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();
            Microsoft.Office.Interop.Excel.Range xlRange = currentSheet.UsedRange;
            OpenWeatherAPI openWeatherAPI = new OpenWeatherAPI(getAPIKey());
            int maxCol = myDomainFields.AllKeys.Length;//can also be a constant.
            int lastRow = xlRange.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
            int lastColumn = xlRange.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Column;
            int fRow = xlRange.Row;
            int fColumn = xlRange.Column;
            var valuesOfCity = myDomainFields.GetValues("City");
            int cityCounter_h = 0;


            for (int rowIndex = fRow; rowIndex <= lastRow; rowIndex++)
            {
                for (int columnIndex = fColumn; columnIndex <= lastColumn; columnIndex++)
                {
                    if (Array.IndexOf(valuesOfCity, currentSheet.Cells[rowIndex, columnIndex].Value) >= 0)
                    {
                        int cityRow = rowIndex;
                        int cityColumn = columnIndex;
                        int fCity_row = rowIndex;
                        int fCity_column = columnIndex;


                        while (currentSheet.Cells[fCity_row, cityColumn].Value != null)//and horizontal true
                        {

                            fCity_row++;
                            if (IsCity(currentSheet.Cells[fCity_row, cityColumn].Value) && !IsDataName(currentSheet.Cells[fCity_row, cityColumn].Value))
                            {
                                cityCounter_h++;
                            }

                        }
                        DetectFieldsAndGetForecastDataHorizontally(cityRow, cityCounter_h, cityColumn, countOfFieldsHorizontal(cityRow, cityColumn, maxCol));

                    }
                }
            }
        }

        private void DetectFieldsAndGetForecastDataHorizontally(int cityRow, int cityCounter, int cityColumn, int maxFieldCount)
        {

            var valuesOfTemperature = myDomainFields.GetValues("Temperature");
            var valuesOfWindSpeed = myDomainFields.GetValues("Wind Speed");
            var valuesOfHumidity = myDomainFields.GetValues("Humidity");
            var valuesOfSunset = myDomainFields.GetValues("Sunset");
            var valuesOfDescription = myDomainFields.GetValues("Description");
            var valuesOfCity = myDomainFields.GetValues("City");


            OpenWeatherAPI openWeatherAPI = new OpenWeatherAPI(getAPIKey());
            Worksheet currentSheet = Globals.ThisAddIn.getActiveWorkSheet();

            for (int fieldIndex_R = cityRow; fieldIndex_R <= cityRow + cityCounter; fieldIndex_R++)
            {
                if (IsCity(currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value))
                {
                    //isHorizontal = true;
                    FiveDayForecast fivedayforecast = openWeatherAPI.fivedayforecast(currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value);
                    int temperatureColumn = cityColumn + 1;
                    int humidityColumn = cityColumn + 2;
                    int descriptionColumn = cityColumn + 3;
                    int windColumn = cityColumn + 4;
                    int dateColumn = cityColumn + 5;
                    int fieldsRow = cityRow + 1;

                    currentSheet.Cells[cityRow, temperatureColumn].Value = "Temperature";
                    currentSheet.Cells[cityRow, humidityColumn].Value = "Humidity";
                    currentSheet.Cells[cityRow, descriptionColumn].Value = "Description";//TODO:Add icon later.
                    currentSheet.Cells[cityRow, windColumn].Value = "Wind Speed";//TODO:Add icon.
                    currentSheet.Cells[cityRow, dateColumn].Value = "Date";

                   foreach(var item in fivedayforecast.forecastList)
                    {

                        currentSheet.Cells[fieldsRow, humidityColumn].Value = item.
                          currentSheet.Cells[fieldsRow, temperatureColumn] = checkedTempUnitIntoQuery(main.Temperature.celsiusCurrent);



                        currentSheet.Cells[fieldsRow, descriptionColumn].Value = weather.Description;



                        currentSheet.Cells[fieldsRow, windColumn].Value = fivedayforecast.Wind.SpeedMetersPerSecond;



                        currentSheet.Cells[fieldsRow, dateColumn].Value = date;
                        fieldsRow++;
                    }
              
                        
                    
                        

                }
                else if (Array.IndexOf(valuesOfCity, currentSheet.Cells[fieldIndex_R + 1, cityColumn].Value) >= 0)
                    break;
                else
                    continue;

            }
            currentSheet.Columns.AutoFit();
        }
    }
}
