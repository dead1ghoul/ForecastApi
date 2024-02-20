
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WethaBot.Models;

namespace WeatherBot
{
    class Program
    {
        private static TelegramBotClient botClient;
        static string _lastCommand;
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6499335199:AAGDmg7pg2VCwdK2vQZ335k4hAvBmfzMsoM");

            client.StartReceiving(Update, Error);

            Console.WriteLine("Bot started. Press any key to stop...");
            Console.ReadLine();

        }


         public static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
         {
            var message = update.Message;
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите /weather , чтобы узнать погоду:");
            }
            if (message != null && message.Text != null)
            {
                Console.WriteLine(message.Text);
                
                if (message.Text.ToLower().Contains("/weather"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название города:");
                    _lastCommand = "/weather";
                    
                }

                if (_lastCommand == "/weather" && message.Text!= "/weather")
                {
                    _lastCommand = string.Empty;
                    string city = message.Text;
                    var weatherResult = GetWeather(city);
                    if (weatherResult != null)
                    {
                        Console.WriteLine(weatherResult);
                        await botClient.SendTextMessageAsync(message.Chat.Id, weatherResult); 
                    }
                    else
                        await botClient.SendTextMessageAsync(message.Chat.Id, "retype");
                }
            }
        }
        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public static string GetWeather(string cityName)
        {
            try
            {
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=c14343f016c933c8bdc7491701478f9c";

                var http = new HttpClient();
                var response = http.GetStringAsync(apiUrl).Result;

                if (response.Contains("404 Not Found"))
                {
                    return "Город не найден";
                }

                СityWeather cityWeather = JsonConvert.DeserializeObject<СityWeather>(response);
                return $"Температура в {cityWeather.Name}: {cityWeather.Main.Temp}°C";
            }
            catch (HttpRequestException)
            {
                return "Failed to connect to weather service";
            }
            catch (JsonException)
            {
                return "Failed to deserialize weather data";
            }
            catch (Exception ex)
            {
                return $"Такого города не существует, введите реальный: {ex.Message}";
            }
        }
    }

    
}
