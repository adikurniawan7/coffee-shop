using System.Globalization;
using CoffeeShop.Models;

namespace CoffeeShop.Services;

public class CoffeeShopService : ICoffeeShopService
{
    private IDateService _dateTimeService;
    private IWeatherService _weatherService;

    public CoffeeShopService(IDateService dateTimeService, IWeatherService weatherService)
    {
        _dateTimeService = dateTimeService;
        _weatherService = weatherService;
    }

    public Task<BrewCoffeeResponse> BrewCoffee()
    {
        string preparedDate = DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture);
        var currentTemperature = _weatherService.GetCurrentTemperature().Result;
        
        if (_dateTimeService.TodayDate.Day == 1 && _dateTimeService.TodayDate.Month == 4)
        {
            return Task.FromResult(new BrewCoffeeResponse(Config.TeapotMessage, preparedDate));
        }
        else if (Config.CoffeeMachineCallCount++ % 5 == 0)
        {
            return Task.FromResult(new BrewCoffeeResponse(Config.UnavailableMessage, preparedDate));
        }
        else if (currentTemperature > 30)
        {
            return Task.FromResult(new BrewCoffeeResponse(Config.HotWeatherSuccessMessage, preparedDate));
        }

        return Task.FromResult(new BrewCoffeeResponse(
            Message: Config.RegularSuccessMessage,
            Prepared: preparedDate
        ));
    }
}
