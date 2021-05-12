﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class DotNetMetricsByTimePeriodResponse
    {
        public List<DotNetMetricDto> Response { get; set; }
    }
    public class DotNetMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
