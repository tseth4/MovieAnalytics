import axios from 'axios'
import { API_URL } from './config'

const api = axios.create({
  baseURL: API_URL
});


export const analyticsService = {
  getBudgetVsGrossData: async () => {
    const response = await api.get(`/analytics/budget-vs-gross`)
    return response.data;
  }
}