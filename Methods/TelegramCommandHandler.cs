using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace WethaBot.Methods
{

    internal class TelegramCommandHandler
    {
        private static string _lastCommand;

        public static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message?.Text != null)
            {
                Console.WriteLine(message.Text);
                if (message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите /weather , чтобы узнать погоду:");
                }

                if (message.Text.ToLower().Contains("/ping"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "pong");
                }

                if(message.Text.StartsWith("/") && message.Text != "/ping" && message.Text != "/weather" && message.Text != "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вы ввели не существующую команду");
                }

                if (message.Text.ToLower().Contains("/weather"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите название города:");
                    _lastCommand = "/weather";
                }

                if (_lastCommand == "/weather" && message.Text != "/weather")
                {
                    _lastCommand = string.Empty;
                    string city = message.Text;
                    var weatherResult = await WeatherProvider.GetWeatherAsync(city);
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
        public static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
