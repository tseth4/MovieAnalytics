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
    fetchAnalyticsData();
  }, [fetchAnalyticsData]);

  return (
    <div>
      <h2>Average Budget by Year</h2>
      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : analyticsData ? (
        <ResponsiveContainer width="100%" height={400}>
          <BarChart
            data={analyticsData.labels.map((label, index) => ({
              year: label,
              avgBudget: analyticsData.values[index],
            }))}
            margin={{ top: 20, right: 30, left: 20, bottom: 5 }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="year" />
            <YAxis label={{ value: "Budget (in M)", angle: -90, position: "insideLeft" }} />
            <Tooltip />
            <Legend />
            <Bar dataKey="avgBudget" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>
      ) : (
        <p>No data available</p>
      )}
    </div>
  );
};

export default BudgetChart;
