using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.Models;
using WorklogService.Models.Configuration;

namespace WorklogService.DataAccess.Holiday
{
    internal class HolidayData : IHolidayData
    {
        private readonly HolidayConfiguration _configuration;
        public HolidayData(HolidayConfiguration holidayConfiguration)
        {
            _configuration = holidayConfiguration;
        }
        public IList<HolidayModel> GetHolidays()
        {
            string holidaysContent = File.ReadAllText(_configuration.FilePath);
            JObject json = JObject.Parse(holidaysContent);
            return json["holiday"].ToObject<List<HolidayModel>>();
        }
    }
}
