using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace app
{
    class CurrentWeather
    {
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Root
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Tuple<float, bool> GetWeather(string city)
        {
            if (!CheckForInternetConnection())
            {
                throw new WebException("No internet connection");
            }

            float temp = 0f;
            bool weatherOK = false;
            string weath = "";

            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=aa768c8b184a85a2e18c5b8fa9736598", city);
                string json = web.DownloadString(url);
                var w = JsonSerializer.Deserialize<Root>(json);
                temp = Convert.ToSingle(w.main.feels_like);
                weath = w.weather[0].main;
            }
            if (weath == "Clear" || weath == "Clouds")
            {
                weatherOK = true;
            }

            return new Tuple<float, bool>(temp, weatherOK);
        }
    }
}