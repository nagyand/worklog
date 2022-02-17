using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;

namespace WorklogService.DataAccess.Holiday
{
    internal interface IHolidayData
    {
        IList<HolidayModel> GetHolidays();
    }
}
