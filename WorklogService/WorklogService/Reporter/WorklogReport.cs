using WorklogService.DataAccess;
using WorklogService.Models.Handler.Reports;
using WorklogService.Models.Reports;

namespace WorklogService.Reporter
{
    public class WorklogReport
    {
        private readonly List<IReportHandler> _reportHandlers;
        private readonly IWorklogData _worklogData;

        public WorklogReport(IWorklogData worklogData, IList<IReportHandler> reportHandlers)
        {
            _worklogData = worklogData;
            _reportHandlers = new List<IReportHandler>(reportHandlers);
        }

        public Report CreateReport()
        {
            Report report = new Report();
            foreach(var worklog in _worklogData.GetWorklogs())
            {
                foreach (var reportHandler in _reportHandlers)
                {
                    reportHandler.Add(worklog);
                }
            }
            _reportHandlers.ForEach(s => s.Report(report));
            return report;
        }
    }
}
