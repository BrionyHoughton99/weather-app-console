using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace weatherApp
{
    class Program
    {

        static void Main(string[] args)
        {

            string stillSearching = "y";

            while (stillSearching.ToLower() == "y")
            {

                Console.WriteLine("Where would you like the weather for?");
                string whereFor = Console.ReadLine();

                RestConsumer getLocation = new RestConsumer();
                getLocation.endPoint = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{whereFor}.json?access_token=pk.eyJ1IjoiYnJpb255aG91Z2h0b24iLCJhIjoiY2p0N2RnMmZmMGdkajN5bGhtNHpiMWtvNiJ9.783AmJqTmMBsXweD4UZsPQ";

                string strResponse = string.Empty;

                strResponse = getLocation.makeRequest();

                dynamic location = JsonConvert.DeserializeObject(strResponse);



                RestConsumer getWeather = new RestConsumer();
                getWeather.endPoint = ($"https://api.darksky.net/forecast/0d6d7e59f5148c9be44c69ebc3f23a77/{location.features[0].center[1]},{location.features[0].center[0]}");


                string weatherData = getWeather.makeRequest();

                dynamic weather = JsonConvert.DeserializeObject(weatherData);



                Console.WriteLine($"The current temperature in {whereFor} is {weather.currently.temperature} °F and the outlook is {weather.currently.summary} with a {weather.currently.precipProbability * 100 }% chance of rain");

                Console.WriteLine("Are you still searching the weather? y/n");
                stillSearching = Console.ReadLine();
            }
        }


    }
}
