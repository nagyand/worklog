using System.Linq;

namespace WorklogService.Models.Reports
{
    public class DailyReport
    {
        public DateTime Date { get; set; }
        public List<UserPercent> UserPercents { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DailyReport report &&
                   Date.Date == report.Date.Date &&
                   UserPercents.SequenceEqual(report.UserPercents);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date, UserPercents);
        }
    }
}
