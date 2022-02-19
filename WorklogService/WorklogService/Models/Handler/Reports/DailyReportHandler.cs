using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using WorklogService.Services.Workday;

namespace WorklogService.Models.Handler.Reports
{
    public class DailyReportHandler : IReportHandler
    {
        private readonly IWorkdayService _workdayService;
        private readonly List<DailyWorklogs> _dailyWorklogs;
        public DailyReportHandler(IWorkdayService workdayService)
        {
            _workdayService = workdayService;
        }
        public void Add(WorklogModel worklog)
        {
            _ = worklog ?? throw new ArgumentNullException(nameof(worklog));
            bool isCorrectWorklog = IsCorrectWorklog(worklog);
            _dailyWorklogs.Add(new DailyWorklogs(worklog.StartDate.Date, worklog, isCorrectWorklog));
        }

        private bool IsCorrectWorklog(WorklogModel worklog)
        {
            if (worklog.StartDate.Date != worklog.Created.Date)
            {
                if (IsBookingOnPreviousDay(worklog))
                    return true;
                return false;
            }
            return true;
        }

        private bool IsBookingOnPreviousDay(WorklogModel worklog) =>
            _workdayService.GetUserPreviousWorkday(worklog.Created, worklog.UserId).Date == worklog.StartDate.Date && worklog.Created.Date.Hour < 10;

        public void Report(Report report)
        {
            _ = report ?? throw new ArgumentNullException(); 
            report.Daily = _dailyWorklogs.Select(s => s.GetReport());
        }
    }
}
