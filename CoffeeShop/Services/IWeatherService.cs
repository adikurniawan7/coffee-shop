namespace CoffeeShop.Services;

public interface IWeatherService
{
    public Task<float> GetCurrentTemperature();
}
