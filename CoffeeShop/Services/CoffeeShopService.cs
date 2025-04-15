using System.Globalization;
using CoffeeShop.Models;

namespace CoffeeShop.Services;

public class CoffeeShopService : ICoffeeShopService
{
    private IDateService _dateTimeService;

    public CoffeeShopService(IDateService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public Task<BrewCoffeeResponse> BrewCoffee()
    {
        string preparedDate = DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture);

        if (_dateTimeService.TodayDate.Day == 1 && _dateTimeService.TodayDate.Month == 4)
        {
            return Task.FromResult(new BrewCoffeeResponse(Config.TeapotMessage, preparedDate));
        }
        else if (Config.CoffeeMachineCallCount++ % 5 == 0)
        {
            return Task.FromResult(new BrewCoffeeResponse(Config.UnavailableMessage, preparedDate));
        }

        return Task.FromResult(new BrewCoffeeResponse(
            Message: Config.SuccessMessage,
            Prepared: preparedDate
        ));
    }
}
