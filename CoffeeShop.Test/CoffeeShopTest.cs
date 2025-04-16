using System.Globalization;
using CoffeeShop.Models;
using CoffeeShop.Services;
using Moq;

namespace CoffeeShop.Test;

public class CoffeeShopTest
{
    private ICoffeeShopService _coffeeShopService;

    private string DateTimeNow => DateTime.UtcNow.ToString(Config.DateFormat, CultureInfo.InvariantCulture);

    [SetUp]
    public void Setup()
    {
        var mockDateService = new Mock<IDateService>();
        var mockWeatherService = new Mock<IWeatherService>();

        _coffeeShopService = new CoffeeShopService(mockDateService.Object, mockWeatherService.Object);
    }

    [Test]
    public async Task BrewCoffee_ReturnOk()
    {
        var response = new BrewCoffeeResponse
        (
            Message: Config.RegularSuccessMessage,
            Prepared: DateTimeNow
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
            Prepared: DateTimeNow
        );
        Config.CoffeeMachineCallCount = 5;
        var result = await _coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message));
    }

    [Test]
    public async Task BrewCoffee_ReturnImATeapot()
    {
        var mockDateService = new Mock<IDateService>();
        var mockWeatherService = new Mock<IWeatherService>();

        mockDateService.Setup(x => x.TodayDate).Returns(new DateTime(2025, 4, 1));
        var coffeeShopService = new CoffeeShopService(mockDateService.Object, mockWeatherService.Object);

        var response = new BrewCoffeeResponse
        (
            Message: Config.TeapotMessage,
            Prepared: DateTimeNow
        );

        var result = await coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message) );
    }

    [Test]
    public async Task BrewCoffee_ReturnIcedCoffeeMessage()
    {
        var mockDateService = new Mock<IDateService>();
        var mockWeatherService = new Mock<IWeatherService>();

        mockWeatherService.Setup(x => x.GetCurrentTemperature()).ReturnsAsync(35);
        var coffeeShopService = new CoffeeShopService(mockDateService.Object, mockWeatherService.Object);

        var response = new BrewCoffeeResponse
        (
            Message: Config.HotWeatherSuccessMessage,
            Prepared: DateTimeNow
        );

        var result = await coffeeShopService.BrewCoffee();
        Assert.That(response.Message, Is.EqualTo(result.Message));
    }
}

