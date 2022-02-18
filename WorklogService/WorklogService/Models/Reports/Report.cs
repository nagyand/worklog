namespace WorklogService.Models.Reports
{
    public class Report
    {
        public List<DailyReport> Daily { get; set; }
        public List<WeeklyReport> Weekly { get; set; }
    }
}
