using WorklogService.Models.Reports;

namespace WorklogService.Reporter
{
    public interface IWorklogReport
    {
        Report CreateReport();
    }
}