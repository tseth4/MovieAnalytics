import { createContext, useContext, useState, ReactNode } from "react";
import { BudgetVsGrossChartDataDto, TopProfitableMovieData } from "@/types/chart";
import { analyticsService } from "@/services/api/AnalyticsService";

interface AnalyticsContextType {
  budgetVsGrossData: BudgetVsGrossChartDataDto | null;
  topProfitableData: TopProfitableMovieData | null;
  loading: boolean;
  error: string | null;
  fetchBudgetVsGrossData: (countryName: string) => Promise<void>;
  fetchTopProfitableMovieData: (countryName: string) => Promise<void>;
}

const AnalyticsContext = createContext<AnalyticsContextType | undefined>(undefined);

export function AnalyticsProvider({ children }: { children: ReactNode }) {
  const [budgetVsGrossData, setBudgetVsGrossData] = useState<BudgetVsGrossChartDataDto | null>(null);
  const [topProfitableData, setTopProfitableData] = useState<TopProfitableMovieData | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchBudgetVsGrossData = async (countryName: string) => {
    if (!budgetVsGrossData) { // Fetch only if data hasn't been fetched yet
      setLoading(true);
      setError(null);
      try {
        const response = await analyticsService.getBudgetVsGrossData(countryName);
        setBudgetVsGrossData(response);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Failed to fetch budget vs gross data");
      } finally {
        setLoading(false);
      }
    }
  };

  const fetchTopProfitableMovieData = async (countryName: string) => {
    if (!topProfitableData) { // Fetch only if data hasn't been fetched yet
      setLoading(true);
      setError(null);
      try {
        const response = await analyticsService.getTopProfitableMovieData(countryName);
        setTopProfitableData(response);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Failed to fetch top profitable data");
      } finally {
        setLoading(false);
      }
    }
  }

  const value = {
    budgetVsGrossData,
    topProfitableData,
    loading,
    error,
    fetchBudgetVsGrossData,
    fetchTopProfitableMovieData
  };

  return (
    <AnalyticsContext.Provider value={value}>
      {children}
    </AnalyticsContext.Provider>
  );
}

export function useAnalytics() {
  const context = useContext(AnalyticsContext);
  if (context === undefined) {
    throw new Error("useAnalytics must be used within an AnalyticsProvider");
  }
  return context;
}
