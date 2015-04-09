using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI;

namespace WeatherAPIExample
{
    class Program
    {
        public static void Main(string[] args)
        {
            Weather API = new Weather(); //Create a new instance
            API.apiKey = ""; // This is the API Key
            API.projectName = ""; //This is the Project Key
            Console.Write("Input your Citycode/PLZ: ");
            string input = Console.ReadLine(); //This is the CityCode

            string surl = null;
            string Checksume = API.GetMD5Hash(API.projectName + API.apiKey + input); //Generate a MD5 Checksume
            if (IsNumeric(input))
            {
                surl = "http://api.wetter.com/location/plz/search/" + input + "/project/" + API.projectName + "/cs/" + Checksume; //Creating a Search URL for a CityCode
            }
            else
            {
                surl = "http://api.wetter.com/location/name/search/" + input + "/project/" + API.projectName + "/cs/" + Checksume; //Creating a Search URL for a CityName
            }

            API.Citycode(surl); //Get the Citycode
            Checksume = API.GetMD5Hash(API.projectName + API.apiKey + API.CityCode); //Generate a new MD5 Checksume
            API.wURL = "http://api.wetter.com/forecast/weather/city/" + API.CityCode + "/project/" + API.projectName + "/cs/" + Checksume; //Get the Weather XML
            API.GetWeather(API.wURL, "06:00"); //Read the Weather for the time 6AM, you can choose between 06:00 11:00 17:00 23:00.


            Console.WriteLine(API.CityName); //Display the CityName
            Console.WriteLine(API.Temperature); //Display the Temperature
            Console.WriteLine(API.WeatherType); //Display what weather it is
            Console.WriteLine(API.Rain); //Display Rain chanche
            Console.WriteLine(API.Wind); //Display the Wind speed
            Console.ReadLine();
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
    }
}
