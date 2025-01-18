import { createContext, useContext, useState, ReactNode } from "react";
import { ChartDataDto } from "@/types/chart";
import { analyticsService } from "@/services/api/AnalyticsService";

interface AnalyticsContextType {
  analyticsData: ChartDataDto | null;
  loading: boolean;
  error: string | null;
  fetchAnalyticsData: () => Promise<void>;
}

const AnalyticsContext = createContext<AnalyticsContextType | undefined>(undefined);

export function AnalyticsProvider({ children }: { children: ReactNode }) {
  const [analyticsData, setAnalyticsData] = useState<ChartDataDto | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchAnalyticsData = async () => {
    if (!analyticsData) { // Fetch only if data hasn't been fetched yet
      setLoading(true);
      setError(null);
      try {
        const response = await analyticsService.getBudgetVsGrossData();
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
