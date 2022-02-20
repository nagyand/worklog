using Serilog;
using WorklogService.DataAccess;
using WorklogService.Models.Handler.Reports;
using WorklogService.Models.Reports;

namespace WorklogService.Reporter
{
    public class WorklogReport
    {
        private readonly List<IReportHandler> _reportHandlers;
        private readonly IWorklogData _worklogData;
        private readonly ILogger _logger;

        public WorklogReport(IWorklogData worklogData, IEnumerable<IReportHandler> reportHandlers, ILogger logger)
        {
            _worklogData = worklogData;
            _reportHandlers = new List<IReportHandler>(reportHandlers);
            _logger = logger;
        }

        public Report CreateReport()
        {
            Report report = new Report();
            foreach (var worklog in _worklogData.GetWorklogs())
            {
                _logger.Information($"Process worklog: {worklog.StartDate} {worklog.Author}");
                foreach (var reportHandler in _reportHandlers)
                {
                    reportHandler.Add(worklog);
                }
            }
            _logger.Information("Generate reports");
            _reportHandlers.ForEach(s => s.Report(report));
            return report;
        }
    }
}
