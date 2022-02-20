using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;

namespace WorklogService.Models.Handler.Reports
{
    public class WeeklyReportHandler : IReportHandler
    {
        private readonly List<WeeklyWorklogs> _weeklyWorklogs;
        public WeeklyReportHandler()
        {
            _weeklyWorklogs = new List<WeeklyWorklogs>();
        }
        public void Add(WorklogModel worklog)
        {
            _ = worklog ?? throw new ArgumentNullException(nameof(worklog));
            DateTime firstDay = GetFirstDayOfWeek(worklog.StartDate);
            WeeklyWorklogs weeklyWorklogs = _weeklyWorklogs.FirstOrDefault(s => s.Start.Date == firstDay.Date);
            if (weeklyWorklogs is null)
            {
                _weeklyWorklogs.Add(new WeeklyWorklogs(firstDay, worklog));
            }
            else
            {
                weeklyWorklogs.Add(worklog);
            }
        }

        public void Report(Report report)
        {
            _ = report ?? throw new ArgumentNullException(nameof(report));
            report.Weekly = _weeklyWorklogs.Select(s => s.GetReport()).OrderBy(s => s.Start);
        }

        private DateTime GetFirstDayOfWeek(DateTime date)
        {
            return date.AddDays(-(int)date.DayOfWeek + 1).Date;
        }
    }
}
