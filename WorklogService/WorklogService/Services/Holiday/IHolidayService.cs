
namespace WorklogService.Services
{
    public interface IHolidayService
    {
        bool IsOnHoliday(string userId, DateTime date);
    }
}