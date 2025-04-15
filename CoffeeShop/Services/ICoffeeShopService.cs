using CoffeeShop.Models;

namespace CoffeeShop.Services;

public interface ICoffeeShopService
{
    Task<BrewCoffeeResponse> BrewCoffee();
}
