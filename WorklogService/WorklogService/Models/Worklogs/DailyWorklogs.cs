using WorklogService.Models.Reports;

namespace WorklogService.Models.Worklogs
{
    public enum WorklogType
    {
        Correct,
        Incorrect
    }
    public class DailyWorklogs
    {
        public DateTime Date { get; set; }
        public List<(WorklogModel worklog, WorklogType type)> Worklogs { get; }
        public DailyWorklogs(DateTime date)
        {
            Date = date;
            Worklogs = new List<(WorklogModel worklog,WorklogType type)>();
        }

        public void Add((WorklogModel worklog, WorklogType type) worklog)
        {
            _ = worklog.worklog ?? throw new ArgumentNullException(nameof(worklog.worklog));
            Worklogs.Add(worklog);
        }

        public DailyReport GetReport()
        {
            DailyReport report = new DailyReport
            {
                Date = Date,
                UserPercents = new List<UserPercent>()
            };
            report.UserPercents.AddRange(from groupedWorklog in Worklogs.GroupBy(s => s.worklog.UserId)
                                         let correct = groupedWorklog.Where(s => s.type == WorklogType.Correct).Sum(s => s.worklog.TimeSpent)
                                         let all = groupedWorklog.Sum(s => s.worklog.TimeSpent)
                                         select new UserPercent
                                         {
                                             UserId = groupedWorklog.Key,
                                             Percent = correct / (double)all * 100
                                         });
            return report;
        }
    }
}
