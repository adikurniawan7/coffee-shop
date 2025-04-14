using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrewCoffeeController : ControllerBase
    {

        private readonly ILogger<BrewCoffeeController> _logger;
        private readonly ICoffeeShopService _coffeeShopService;
        private int counter = 0;

        public BrewCoffeeController(ICoffeeShopService coffeeShopService, ILogger<BrewCoffeeController> logger)
        {
            _coffeeShopService = coffeeShopService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> BrewCoffee()
        {
            var result = await _coffeeShopService.BrewCoffee(counter);
            return Ok(result);
        }
    }
}
