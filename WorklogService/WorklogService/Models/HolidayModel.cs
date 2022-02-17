using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorklogService.Models
{
    internal class HolidayModel
    {
        [JsonProperty("name")]
        public string UserId { get; set; }
        public string Date { get; set; }
    }
}
