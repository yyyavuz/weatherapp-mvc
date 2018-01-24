using System;
using System.ComponentModel.DataAnnotations;

namespace Kontrolmatik.Data.Tables
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string WeatherStatus { get; set; }
        public string WeatherStatusDescription { get; set; }
        public decimal Temperature { get; set; }
        public decimal TemperatureMin { get; set; }
        public decimal TemperatureMax { get; set; }
    }
}