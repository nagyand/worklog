
namespace WorklogService.Services.Workday
{
    public interface IWorkdayService
    {
        bool IsWorkday(DateTime date);
        DateTime GetUserPreviousWorkday(DateTime date, string userId);
    }
}