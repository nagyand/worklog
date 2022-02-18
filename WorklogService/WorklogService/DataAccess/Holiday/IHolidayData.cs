using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;

namespace WorklogService.DataAccess.Holiday
{
    public interface IHolidayData
    {
        IList<HolidayModel> GetHolidays();
        IList<HolidayModel> GetHolidays(string userId);
    }
}
