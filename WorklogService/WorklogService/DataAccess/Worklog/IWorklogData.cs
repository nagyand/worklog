using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;

namespace WorklogService.DataAccess
{
    public interface IWorklogData
    {
        IList<WorklogModel> GetWorklogs();
        IEnumerable<WorklogModel> GetWorklog(string userId);
    }
}
