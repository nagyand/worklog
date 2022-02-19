using WorklogService.DataAccess.Holiday;

namespace WorklogService.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayData _holiday;
        public HolidayService(IHolidayData holiday)
        {
            _holiday = holiday;
        }

        public bool IsOnHoliday(string userId, DateTime date) => _holiday.GetHolidays(userId).FirstOrDefault(s => s.Date.Date == date.Date) != null;
    }
}
