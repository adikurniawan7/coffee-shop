using System.Globalization;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        public Task<BrewCoffeeResponse> BrewCoffee(int counter)
        {
            BrewCoffeeResponse response = new BrewCoffeeResponse(
                Message: "Your piping hot coffee is ready",
                Prepared: DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)
            );
  
            return Task.FromResult(response);
        }
    }
}
