namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class BudgetVsGrossChartDataDto
    {
        public required List<string> Labels { get; set; }
        public required List<decimal> AvgBudgetValues { get; set; }
        public required List<decimal> AvgGrossValues { get; set; }

    }
}
