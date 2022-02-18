﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorklogService.Models
{
    public class HolidayModel
    {
        [JsonProperty("name")]
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
