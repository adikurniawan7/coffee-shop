using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeShopController : ControllerBase
{

    private readonly ILogger<CoffeeShopController> _logger;
    private readonly ICoffeeShopService _coffeeShopService;

    public CoffeeShopController(ICoffeeShopService coffeeShopService, ILogger<CoffeeShopController> logger)
    {
        _coffeeShopService = coffeeShopService;
        _logger = logger;
    }

    [HttpGet]
    [Route("brew-coffee")]
    public async Task<IActionResult> BrewCoffee()
    {
        var result = await _coffeeShopService.BrewCoffee();

        if (result.Message.Equals(Config.UnavailableMessage, StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, Config.UnavailableMessage);
        }
        else if (result.Message.Equals(Config.TeapotMessage, StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(StatusCodes.Status418ImATeapot, Config.TeapotMessage);
        }

        return Ok(result);
    }
}
