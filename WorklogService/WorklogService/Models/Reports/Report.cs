namespace WorklogService.Models.Reports
{
    public class Report
    {
        public IEnumerable<DailyReport> Daily { get; set; }
        public IEnumerable<WeeklyReport> Weekly { get; set; }
    }
}
