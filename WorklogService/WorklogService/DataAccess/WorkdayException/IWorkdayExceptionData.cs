using WorklogService.Models;

namespace WorklogService.DataAccess.WorkdayException
{
    internal interface IWorkdayExceptionData
    {
        IList<WorkdayExceptionModel> GetWorkdayExceptions();
    }
}