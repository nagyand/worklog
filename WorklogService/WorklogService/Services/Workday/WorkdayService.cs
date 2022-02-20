using WorklogService.DataAccess.WorkdayException;
using WorklogService.Models;

namespace WorklogService.Services.Workday
{
    public class WorkdayService : IWorkdayService
    {
        private readonly IWorkdayExceptionData _workdayExceptionData;
        private readonly IHolidayService _holidayService;
        private IList<WorkdayExceptionModel> _workdayExceptions;
        public WorkdayService(IWorkdayExceptionData workdayExceptionData, IHolidayService holidayService)
        {
            _ = workdayExceptionData ?? throw new ArgumentNullException(nameof(workdayExceptionData));
            _ = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
            _workdayExceptionData = workdayExceptionData;
            _holidayService = holidayService;
        }

        public bool IsWorkday(DateTime date)
        {
            _workdayExceptions ??= _workdayExceptionData.GetWorkdayExceptions();
            WorkdayExceptionModel workdayException = _workdayExceptions.FirstOrDefault(s => s.Date.Date == date.Date);
            if (workdayException != null)
                return workdayException.IsWork;
            return !IsWeekend(date);
        }

        public DateTime GetUserPreviousWorkday(DateTime date,string userId)
        {
            DateTime previousWorkday = date.AddDays(-1);
            while (!IsWorkday(previousWorkday) || _holidayService.IsOnHoliday(userId, previousWorkday))
                previousWorkday = previousWorkday.AddDays(-1);
            return previousWorkday;
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
    }
}
