using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;
using WorklogService.Models.Configuration;

namespace WorklogService.DataAccess
{
    internal class WorklogData : IWorklogData
    {
        private readonly WorklogConfiguration _configuration;
        private IList<WorklogModel> _worklogs;
        public WorklogData(WorklogConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IList<WorklogModel> GetWorklogs()
        {
            string worklogContent = File.ReadAllText(_configuration.FilePath);
            JObject json = JObject.Parse(worklogContent);
            List<WorklogModel> worklogs = new List<WorklogModel>();
            foreach(var worklog in json["worklog"])
            {
                foreach(var entry in worklog["entries"])
                {
                    WorklogModel worklogModel = new WorklogModel((string)entry["author"], (string)entry["authorFullName"], (long)entry["created"], (long)entry["startDate"], (int)entry["timeSpent"]);
                    worklogs.Add(worklogModel);
                }
            }
            return worklogs;
        }

        public IEnumerable<WorklogModel> GetWorklog(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                _worklogs ??= GetWorklogs();
                return _worklogs.Where(s => s.UserId == userId);
            }
            throw new ArgumentNullException(nameof(userId));
        }
    }
}
