using System.Globalization;
using CoffeeShop.Models;
using CoffeeShop.Services;
using Moq;

namespace CoffeeShop.Test;

public class CoffeeShopTest
{
    private ICoffeeShopService _coffeeShopService;

    [SetUp]
    public void Setup()
    {
        var mockDateService = new Mock<IDateService>();

        _coffeeShopService = new CoffeeShopService(mockDateService.Object);
    }

    [Test]
    public async Task BrewCoffee_ReturnOk()
    {
        var response = new BrewCoffeeResponse
        (
            Message: Config.SuccessMessage,
            Prepared: DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture)
        );
        Config.CoffeeMachineCallCount = 1;
        var result = await _coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message));
    }

    [Test]
    public async Task BrewCoffee_ReturnUnavailable()
    {
        var response = new BrewCoffeeResponse
        (
            Message: Config.UnavailableMessage,
            Prepared: DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture)
        );
        Config.CoffeeMachineCallCount = 5;
        var result = await _coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message));
    }

    [Test]
    public async Task BrewCoffee_ReturnImATeapot()
    {
        var mockDateService = new Mock<IDateService>();

        mockDateService.Setup(x => x.TodayDate).Returns(new DateTime(2025, 4, 1));
        var coffeeShopService = new CoffeeShopService(mockDateService.Object);

        var response = new BrewCoffeeResponse
        (
            Message: Config.TeapotMessage,
            Prepared: DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture)
        );

        var result = await coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message) );
    }
}

