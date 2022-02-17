using WorklogService.Models;

namespace WorklogService.DataAccess.WorkdayException
{
    public interface IWorkdayExceptionData
    {
        IList<WorkdayExceptionModel> GetWorkdayExceptions();
    }
}