namespace WorklogService.Models.Reports
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public List<UserPercent> UserPercents { get; set; }
    }
}
