import { createContext, useContext, useState, ReactNode } from "react";
import { BudgetVsGrossChartDataDto } from "@/types/chart";
import { analyticsService } from "@/services/api/AnalyticsService";

interface AnalyticsContextType {
  analyticsData: BudgetVsGrossChartDataDto | null;
  loading: boolean;
  error: string | null;
  fetchAnalyticsData: (countryName: string) => Promise<void>;
}

const AnalyticsContext = createContext<AnalyticsContextType | undefined>(undefined);

export function AnalyticsProvider({ children }: { children: ReactNode }) {
  const [analyticsData, setAnalyticsData] = useState<BudgetVsGrossChartDataDto | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchAnalyticsData = async (countryName: string) => {
    if (!analyticsData) { // Fetch only if data hasn't been fetched yet
      setLoading(true);
      setError(null);
      try {
        const response = await analyticsService.getBudgetVsGrossData(countryName);
        setAnalyticsData(response);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Failed to fetch analytics data");
      } finally {
        setLoading(false);
      }
    }
  };

  const value = {
    analyticsData,
    loading,
    error,
    fetchAnalyticsData,
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
