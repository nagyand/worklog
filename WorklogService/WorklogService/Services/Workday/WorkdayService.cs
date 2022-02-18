using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.DataAccess.WorkdayException;
using WorklogService.Models;

namespace WorklogService.Services.Workday
{
    public class WorkdayService : IWorkdayService
    {
        private readonly IWorkdayExceptionData _workdayExceptionData;
        private IList<WorkdayExceptionModel> _workdayExceptions;
        public WorkdayService(IWorkdayExceptionData workdayExceptionData)
        {
            _workdayExceptionData = workdayExceptionData;
        }

        public bool IsWorkday(DateTime date)
        {
            _workdayExceptions ??= _workdayExceptionData.GetWorkdayExceptions();
            WorkdayExceptionModel workdayException = _workdayExceptions.FirstOrDefault(s => s.Date.Date == date.Date);
            if (workdayException != null)
                return workdayException.IsWork;
            return !IsWeekend(date);
        }

        public DateTime GetPreviousWorkday(DateTime date)
        {
            DateTime previousWorkday = date.AddDays(-1);
            while (!IsWorkday(previousWorkday))
                previousWorkday = previousWorkday.AddDays(-1);
            return previousWorkday;
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
    }
}
