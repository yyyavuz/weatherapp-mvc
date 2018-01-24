using System.Threading.Tasks;
using Kontrolmatik.Models;

namespace Kontrolmatik.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherInfo(string city,string country);
    }
}