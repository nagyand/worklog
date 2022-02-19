using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;

namespace WorklogService.Models.Handler.Reports
{
    public interface IReportHandler
    {
        void Add(WorklogModel worklog);
        void Report(Report report);
    }
}
