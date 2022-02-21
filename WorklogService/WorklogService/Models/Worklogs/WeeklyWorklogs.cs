using WorklogService.Models.Reports;

namespace WorklogService.Models.Worklogs
{
    public class WeeklyWorklogs
    {
        public DateTime Start { get; }
        private List<WorklogModel> _worklogs;

        public WeeklyWorklogs(DateTime start, WorklogModel worklog)
        {
            _ = worklog ?? throw new ArgumentNullException(nameof(worklog));
            Start = start;
            _worklogs = new List<WorklogModel> { worklog };
        }

        public void Add(WorklogModel worklog)
        {
            _ = worklog ?? throw new ArgumentNullException(nameof(worklog));
            _worklogs.Add(worklog);
        }

        public WeeklyReport GetReport()
        {
            WeeklyReport report = new WeeklyReport
            {
                Start = Start,
                UserPercents = new List<UserPercent>()
            };
            report.UserPercents.AddRange(from groupedWorklog in _worklogs.GroupBy(s => s.UserId)
                                         let precent = groupedWorklog.Sum(s => s.TimeSpent) / ((double)(32 * 3600)) * 100
                                         select new UserPercent
                                         {
                                             UserId = groupedWorklog.Key,
                                             Percent = Math.Round(precent,2)
                                         });
            return report;
        }
    }
}
