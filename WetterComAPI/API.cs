using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WeatherAPI
{
    class Weather
    {
        public string wURL { get; set; }
        public string projectName { get; set; }
        public string apiKey { get; set; }
        public string CityName { get; set; }
        public string WeatherType { get; set; }
        public string Temperature { get; set; }
        public string Rain { get; set; }
        public string Wind { get; set; }
        public string CityCode { get; set; }
        public string Checksume { get; set; }
        public string Error { get; set; }
        public void GetWeather(string URL, string Zeit) // I need another method because special characters like: ä ö ü and ß won't get displayed properly.
        {
            try
            {
                string xml;
                using (var webClient = new WebClient())
                {
                    xml = webClient.DownloadString(URL);
                }
                XDocument doc = XDocument.Parse(xml);
                this.CityName = doc.XPathSelectElement("/city/name").Value;
                this.WeatherType = doc.XPathSelectElement("/city/forecast/date/time[@value='" + Zeit + "']/w_txt").Value;
                this.Temperature = doc.XPathSelectElement("/city/forecast/date/time[@value='" + Zeit + "']/tn").Value + "-" + doc.XPathSelectElement("/city/forecast/date/time/tx").Value + "°";
                this.Rain = doc.XPathSelectElement("/city/forecast/date/time[@value='" + Zeit + "']/pc").Value + "% Rain";
                this.Wind = doc.XPathSelectElement("/city/forecast/date/time[@value='" + Zeit + "']/ws").Value + " km/h";
            }
            catch
            {
                Error = "Error getting the weather";
            }
        }
        public void Citycode(string URL)
        {
            string xml;
            using (var webClient = new WebClient())
            {
                xml = webClient.DownloadString(URL);
            }
            XDocument doc = XDocument.Parse(xml);
            try
            {
                string CityCodee = doc.XPathSelectElement("/search/result/item/city_code").Value;
                this.CityCode = CityCodee;
            }
            catch
            {
                Error = "Coulnd´t find the city";
                this.CityCode = null;
            }
        }
        public string GetMD5Hash(string TextToHash)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);
            return Checksume = System.BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
        }
    }
}
