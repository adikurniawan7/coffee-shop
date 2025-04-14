using System.Globalization;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        public Task<BrewCoffeeResponse> BrewCoffee(int counter)
        {
            string preparedDate = DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture);

            if (DateTime.Today.Day == 1 && DateTime.Today.Month == 4)
            {
                return Task.FromResult(new BrewCoffeeResponse(Config.TeapotMessage, preparedDate));
            }
            else if (++Config.CoffeeMachineCallCount % 5 == 0)
            {
                return Task.FromResult(new BrewCoffeeResponse(Config.UnavailableMessage, preparedDate));
            }

            return Task.FromResult(new BrewCoffeeResponse(
                Message: Config.SuccessMessage,
                Prepared: preparedDate
            ));
        }
    }
}
