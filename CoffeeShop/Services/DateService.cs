
namespace CoffeeShop.Services;

public class DateService : IDateService
{
    public DateTime TodayDate
    {
        get { return DateTime.Today; }
    }
}
