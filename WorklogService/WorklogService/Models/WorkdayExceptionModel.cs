﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorklogService.Models
{
    internal class WorkdayExceptionModel
    {
        public DateTime Date { get; set; }
        public bool IsWork { get; set; }
    }
}