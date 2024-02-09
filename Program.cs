using System.Net;
using System.IO;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Telegram.Bot;

namespace WethaBot
{

    class Program
    {
        private static TelegramBotClient botClient;

        static void Main(string[] args)
        {

            Console.WriteLine("Enter the city: ");
            string cityName = Console.ReadLine();

            // Адрес API для получения погоды через координаты
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=03a6166867875433b671f0b4a50744fc";


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            { 
                response = streamReader.ReadToEnd();
            }
       
            WeatherResp weatherResp = JsonConvert.DeserializeObject<WeatherResp>(response);
            Console.WriteLine($"Temperature in {weatherResp.Name} : {weatherResp.Main.Temp}°C");
        }

        

    }

}
