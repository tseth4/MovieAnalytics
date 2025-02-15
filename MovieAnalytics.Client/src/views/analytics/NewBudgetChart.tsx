import { ChartConfig, ChartContainer, ChartLegend, ChartLegendContent, ChartTooltip, ChartTooltipContent } from "@/components/ui/chart";
import { useAnalytics } from "@/context/AnalyticsContext";
import { useEffect } from "react";
import { Bar, BarChart, CartesianGrid, XAxis } from "recharts";


export default function NewBudgeChart() {
  const { budgetVsGrossData, loading, error, fetchBudgetVsGrossData } = useAnalytics();

  // Fetch data on component mount
  useEffect(() => {
    fetchBudgetVsGrossData('United States');
  }, [fetchBudgetVsGrossData]);

  // Transform the analytics data into a format usable by Recharts
  const chartData = budgetVsGrossData
    ? budgetVsGrossData.labels.map((label, index) => ({
      year: label,
      avgBudget: budgetVsGrossData.avgBudgetValues[index],
      avgGross: budgetVsGrossData.avgGrossValues[index],
    }))
    : [];

  const chartConfig = {
    avgBudget: {
      label: "Average Budget",
      color: "#2563eb",
    },
    avgGross: {
      label: "Average Gross",
      color: "#60a5fa",
    },
  } satisfies ChartConfig


  return (
    <div >
      <h2 className="text-primary"><strong>Average Budget and Gross Profit by Year in the United States</strong></h2>
      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : (
        <ChartContainer config={chartConfig} className="min-h-[100%]">
          <BarChart accessibilityLayer data={chartData}>
            <CartesianGrid vertical={false} />
            <XAxis
              dataKey="year"
              tickLine={false}
              tickMargin={10}
              axisLine={true}
              tickFormatter={(value) => value.slice(2, 4)}
            />
            <ChartTooltip content={<ChartTooltipContent />} />
            <ChartLegend content={<ChartLegendContent />} />

            <Bar animationDuration={100} dataKey="avgBudget" fill="#2563eb" radius={4} />
            <Bar animationDuration={100} dataKey="avgGross" fill="#60a5fa" radius={4} />
          </BarChart>
        </ChartContainer>
      )}
    </div>
  );
}