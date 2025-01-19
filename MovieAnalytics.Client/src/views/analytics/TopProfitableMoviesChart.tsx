import { ChartConfig, ChartContainer, ChartLegend, ChartLegendContent, ChartTooltip, ChartTooltipContent } from "@/components/ui/chart";
import { useAnalytics } from "@/context/AnalyticsContext";
import { useEffect } from "react";
import { Bar, BarChart, CartesianGrid, XAxis } from "recharts";


export default function TopProfitableMovie() {
  const { topProfitableData, loading, error, fetchTopProfitableMovieData } = useAnalytics();

  // Fetch data on component mount
  useEffect(() => {
    fetchTopProfitableMovieData('United States');
  }, [fetchTopProfitableMovieData]);

  // Transform the analytics data into a format usable by Recharts
  const chartData = topProfitableData
    ? topProfitableData.labels.map((label, index) => ({
      year: label,
      roi: topProfitableData.values[index],
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
      <h2 className="text-primary"><strong>Top 10 Profitable Movies in the United States (ROI)</strong></h2>
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
              tickFormatter={(value) => value.slice(0, 1)}
            />
            <ChartTooltip content={<ChartTooltipContent />} />
            <ChartLegend content={<ChartLegendContent />} />

            <Bar animationDuration={100} dataKey="roi" fill="#2563eb" radius={4} />
          </BarChart>
        </ChartContainer>
      )}
    </div>
  );
}