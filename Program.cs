using MailKit;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WethaBot;

namespace WeatherBot
{
    class Program
    {
        private static TelegramBotClient botClient;
        public static string response;
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6499335199:AAGDmg7pg2VCwdK2vQZ335k4hAvBmfzMsoM");

            client.StartReceiving(Update, Error);

            Console.WriteLine("Bot started. Press any key to stop...");
            Console.ReadLine();

        }


        async public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                Console.WriteLine(message.Text);
                string city = message.Text;
                if (message.Text.ToLower().Contains("/weather"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название города:");
                    
                }
            }
        }
        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public static string GetWeather(string cityName)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=YOUR_API_KEY";

            var http = new HttpClient();
            var response = http.GetStringAsync(apiUrl).Result;

            WeatherResp weatherResp = JsonConvert.DeserializeObject<WeatherResp>(response);
            return $"Temperature in {weatherResp.Name}: {weatherResp.Main.Temp}°C";
        }

    }
}
