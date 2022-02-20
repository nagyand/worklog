using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;

namespace WorklogService.Handlers.Reports
{
    public interface IReportHandler
    {
        void Add(WorklogModel worklog);
        void Report(Report report);
    }
}
