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
        public List<(WorklogModel worklog, WorklogType type)> Worklogs { get; set; }
        public DailyReport GetReport()
        {
            return null;
        }
    }
}
