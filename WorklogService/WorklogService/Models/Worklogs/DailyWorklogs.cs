using WorklogService.Models.Reports;

namespace WorklogService.Models.Worklogs
{
    public class DailyWorklogs
    {
        public DateTime Date { get; set; }
        public List<(WorklogModel worklog, bool isCorrenct)> Worklogs { get; }
        public DailyWorklogs(DateTime date,WorklogModel worklog, bool isCorrect)
        {
            Date = date;
            Worklogs = new List<(WorklogModel worklog, bool isCorrect)>
            {
                (worklog,isCorrect)
            };
        }

        public void Add((WorklogModel worklog, bool type) worklog)
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
                                         let correct = groupedWorklog.Where(s => s.isCorrenct).Sum(s => s.worklog.TimeSpent)
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
