import React, { useEffect } from "react";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";
import { useAnalytics } from "@/context/AnalyticsContext";

const BudgetChart: React.FC = () => {
  const { analyticsData, loading, error, fetchAnalyticsData } = useAnalytics();

  // Fetch data on component mount
  useEffect(() => {
    fetchAnalyticsData('United States');
  }, [fetchAnalyticsData]);

  // Transform the analytics data into a format usable by Recharts
  const chartData = analyticsData
    ? analyticsData.labels.map((label, index) => ({
      year: label,
      avgBudget: analyticsData.avgBudgetValues[index],
      avgGross: analyticsData.avgGrossValues[index],
    }))
    : [];

  return (
    <div className="w-full">
      <h2><strong>Average Budget and Gross Profit by Year in the United States</strong></h2>
      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : (
        <ResponsiveContainer className="mt-6 w-full" width="100.5%" height={400}>
          <BarChart
            data={chartData}
            margin={{ top: 20, right: 30, left: 20, bottom: 5 }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="year" />
            <YAxis
              label={{
                // value: "Amount (in $M)",
                angle: -90,
                position: "insideLeft",
              }}
              tickFormatter={(value) => `$${(value / 1_000_000).toFixed(1)}M`}
            />
            <Tooltip formatter={(value: number) => `$${(value / 1_000_000).toFixed(1)}M`} />
            <Legend />
            <Bar dataKey="avgBudget" fill="blue" name="Avg Budget" />
            <Bar dataKey="avgGross" fill="red" name="Avg Gross" />
          </BarChart>
        </ResponsiveContainer>
      )}
    </div>
  );
};

export default BudgetChart;
