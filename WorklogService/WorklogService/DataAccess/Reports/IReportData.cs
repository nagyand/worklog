using WorklogService.Models.Reports;

namespace WorklogService.DataAccess.Reports
{
    internal interface IReportData
    {
        void Write(Report report);
    }
}