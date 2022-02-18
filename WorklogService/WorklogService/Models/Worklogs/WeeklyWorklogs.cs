using WorklogService.Models.Reports;

namespace WorklogService.Models.Worklogs
{
    public class WeeklyWorklogs
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        private List<WorklogModel> _worklogs;

        public WeeklyWorklogs(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            _worklogs = new List<WorklogModel>();
        }

        public void Add(WorklogModel worklog)
        {
            _worklogs.Add(worklog);
        }

        public WeeklyReport GetReport()
        {
            return null;
        }
    }
}
