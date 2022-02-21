using WorklogService.Models;

namespace WorklogService.DataAccess.Holiday
{
    public interface IHolidayData
    {
        IList<HolidayModel> GetHolidays();
        IList<HolidayModel> GetHolidays(string userId);
    }
}
