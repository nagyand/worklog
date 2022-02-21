using Newtonsoft.Json;

namespace WorklogService.Models
{
    public class HolidayModel
    {
        [JsonProperty("name")]
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
