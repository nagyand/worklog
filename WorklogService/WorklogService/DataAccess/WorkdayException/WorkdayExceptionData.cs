using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;
using WorklogService.Models.Configuration;

namespace WorklogService.DataAccess.WorkdayException
{
    public class WorkdayExceptionData : IWorkdayExceptionData
    {
        private readonly WorkdayExceptionConfiguration _workdayExceptionConfiguration; 
        public WorkdayExceptionData(WorkdayExceptionConfiguration workdayExceptionConfiguration)
        {
            _workdayExceptionConfiguration = workdayExceptionConfiguration;
        }
        public IList<WorkdayExceptionModel> GetWorkdayExceptions()
        {
            string worklogContent = File.ReadAllText(_workdayExceptionConfiguration.FilePath);
            JObject json = JObject.Parse(worklogContent);
            return json["workdayexceptions"].ToObject<List<WorkdayExceptionModel>>();
        }
    }
}
