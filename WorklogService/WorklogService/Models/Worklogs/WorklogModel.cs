namespace WorklogService.Models.Worklogs
{
    public class WorklogModel
    {
        public string UserId { get; }
        public string Author { get; }
        public DateTime Created { get;}
        public DateTime StartDate { get; }
        public int TimeSpent { get; }

        public WorklogModel(string userId, string author, long created, long startDate, int timespent)
        {
            UserId = userId;
            Author = author;
            TimeSpent = timespent;
            Created = DateTimeOffset.FromUnixTimeMilliseconds(created).LocalDateTime;
            StartDate = DateTimeOffset.FromUnixTimeMilliseconds(startDate).LocalDateTime;
        }

    }
}
