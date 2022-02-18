
namespace WorklogService.Services.Workday
{
    public interface IWorkdayService
    {
        bool IsWorkday(DateTime date);
        DateTime GetPreviousWorkday(DateTime date);
    }
}