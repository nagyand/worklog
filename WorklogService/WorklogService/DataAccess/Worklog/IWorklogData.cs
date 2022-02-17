using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;

namespace WorklogService.DataAccess
{
    internal interface IWorklogData
    {
        IList<WorklogModel> GetWorklogs();
    }
}
