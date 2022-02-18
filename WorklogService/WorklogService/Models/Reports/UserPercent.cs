namespace WorklogService.Models.Reports
{
    public class UserPercent
    {
        public string UserId { get; set;}
        public double Percent { get; set;}

        public override bool Equals(object obj)
        {
            return obj is UserPercent percent &&
                   UserId == percent.UserId &&
                   Percent == percent.Percent;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, Percent);
        }
    }
}
