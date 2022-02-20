using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using WorklogService.Models;
using WorklogService.Models.Configuration;

namespace WorklogService.DataAccess.Holiday
{
    public class HolidayData : IHolidayData
    {
        private readonly HolidayConfiguration _configuration;
        private IList<HolidayModel> _holidays;
        public HolidayData(IOptions<HolidayConfiguration> holidayConfiguration)
        {
            _configuration = holidayConfiguration.Value;
        }
        public IList<HolidayModel> GetHolidays()
        {
            string holidaysContent = File.ReadAllText(_configuration.FilePath);
            JObject json = JObject.Parse(holidaysContent);
            return json["holiday"].ToObject<List<HolidayModel>>();
        }

        public IList<HolidayModel> GetHolidays(string userId)
        {
            _holidays ??= GetHolidays();
            return _holidays.Where(s => s.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                            .ToList();
        }
    }
}
