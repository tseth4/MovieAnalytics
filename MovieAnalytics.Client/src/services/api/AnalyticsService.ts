import axios from 'axios'
import { API_URL } from './config'

const api = axios.create({
  baseURL: API_URL
});


export const analyticsService = {
  getBudgetVsGrossData: async (countryName: string) => {
    console.log("getBudgetVsGrossData")
    const response = await api.get(`/analytics/budget-vs-gross/${countryName}`)
    return response.data;
  },
  getTopProfitableMovieData: async (countryName: string) => {
    console.log("getTopProfitableMovieData")
    const response = await api.get(`/analytics/top-profitable/${countryName}`)
    return response.data;
  }
}