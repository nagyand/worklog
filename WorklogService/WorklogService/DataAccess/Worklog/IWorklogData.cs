using WorklogService.Models.Worklogs;

namespace WorklogService.DataAccess
{
    public interface IWorklogData
    {
        IList<WorklogModel> GetWorklogs();
        IEnumerable<WorklogModel> GetWorklog(string userId);
    }
}
