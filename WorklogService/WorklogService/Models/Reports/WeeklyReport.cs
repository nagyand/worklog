namespace WorklogService.Models.Reports
{
    public class WeeklyReport
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<UserPercent> UserPercents { get; set; }

        public override bool Equals(object obj)
        {
            return obj is WeeklyReport report &&
                   Start == report.Start &&
                   Stop == report.Stop &&
                   UserPercents.SequenceEqual(report.UserPercents);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, Stop, UserPercents);
        }
    }
}
