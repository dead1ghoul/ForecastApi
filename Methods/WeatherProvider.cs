using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WethaBot.Models;

namespace WethaBot.Methods
{
    internal class WeatherProvider
    {
        public static async Task<string> GetWeatherAsync(string cityName)
        {
            try
            {
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=c14343f016c933c8bdc7491701478f9c";

                var http = new HttpClient();
                var response =  await http.GetStringAsync(apiUrl);
                СityWeather cityWeather = JsonConvert.DeserializeObject<СityWeather>(response);
                return $"Температура в {cityWeather.Name}: {cityWeather.Main.Temp}°C";
            }

            catch (HttpRequestException)
            {
                return "Неверное название города, попробуйте заново";
            }
            catch (JsonException)
            {
                return "Failed to deserialize weather data";
            }
            catch (Exception ex)
            {
                return $"Произошла ошибка: {ex.Message}";
            }
        }
    }
}
