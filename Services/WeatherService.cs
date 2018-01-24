using System.Net.Http;
using System.Threading.Tasks;
using Kontrolmatik.Models;
using Newtonsoft.Json;

namespace Kontrolmatik.Services
{
    public class WeatherService : IWeatherService
    {
        public string AppId { get; set; } = "aa731867ea74c6b8c39073979986cfd6";
        public async Task<WeatherModel> GetWeatherInfo(string city, string country)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = await client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={AppId}&units=metric");
                if (request.IsSuccessStatusCode)
                {
                    var json = await request.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherModel>(json);
                }
                return null;
            }
        }
    }
}