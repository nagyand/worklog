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
            _ = workdayService ?? throw new ArgumentNullException(nameof(workdayService));
            _workdayService = workdayService;
            _dailyWorklogs = new List<DailyWorklogs>();
        }
        public void Add(WorklogModel worklog)
        {
            _ = worklog ?? throw new ArgumentNullException(nameof(worklog));
            bool isCorrectWorklog = IsCorrectWorklog(worklog);
            DailyWorklogs dailyWorklogs = _dailyWorklogs.FirstOrDefault(s => s.Date.Date == worklog.StartDate.Date);
            if (dailyWorklogs is null)
            {
                _dailyWorklogs.Add(new DailyWorklogs(worklog.StartDate.Date, worklog, isCorrectWorklog));
            }
            else
            {
                dailyWorklogs.Add((worklog, isCorrectWorklog));
            }
        }

        private bool IsCorrectWorklog(WorklogModel worklog)
        {
            if (worklog.StartDate.Date != worklog.Created.Date)
            {
                if (IsCorrectlyBookingOnPreviousDay(worklog))
                    return true;
                return false;
            }
            return true;
        }

        private bool IsCorrectlyBookingOnPreviousDay(WorklogModel worklog) =>
            _workdayService.GetUserPreviousWorkday(worklog.Created, worklog.UserId).Date == worklog.StartDate.Date && worklog.Created.Hour < 10;

        public void Report(Report report)
        {
            _ = report ?? throw new ArgumentNullException(); 
            report.Daily = _dailyWorklogs.Select(s => s.GetReport());
        }
    }
}
