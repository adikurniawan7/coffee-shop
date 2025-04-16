using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoffeeShop.Services;

public class WeatherService : IWeatherService
{
    public async Task<float> GetCurrentTemperature()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://api.open-meteo.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("v1/forecast?latitude=-37.814&longitude=144.9633&current=temperature_2m");
            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().Result;
            var location = JsonSerializer.Deserialize
                <WeatherData>(responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            if (location == null) return 0;
            return location.Current.Temperature;
        }
    }
}

class Current
{
    [JsonPropertyName("temperature_2m")]
    public float Temperature { get; set; } = 0;
}

class WeatherData
{
    public Current Current { get; set; } = new Current();
}