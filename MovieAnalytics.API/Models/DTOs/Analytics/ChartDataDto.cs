﻿namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class ChartDataDto
    {
        public required List<string> Labels { get; set; }
        public required List<decimal> Values { get; set; }
    }
}