using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models.Configuration;
using WorklogService.Models.Reports;

namespace WorklogService.DataAccess.Reports
{
    internal class ReportData : IReportData
    {
        private readonly ReportConfiguration _reportsConfiguration;
        public ReportData(IOptions<ReportConfiguration> reportConfiguraion)
        {
            _reportsConfiguration = reportConfiguraion.Value;
        }

        public void Write(Report report)
        {
            File.WriteAllText(_reportsConfiguration.ReportPath, JsonConvert.SerializeObject(report));
        }
    }
}
