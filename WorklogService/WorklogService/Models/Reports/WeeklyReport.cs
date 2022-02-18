namespace WorklogService.Models.Reports
{
    public class WeeklyReport
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<UserPercent> UserPercents { get; set; }
    }
}
