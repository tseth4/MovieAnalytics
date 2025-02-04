import axios from 'axios';
import { API_URL } from './config';

import { LoginCredentials } from '@/types/LoginCredentials';
import { LoginResponse } from '@/types/LoginResponse';

const api = axios.create({
  baseURL: API_URL
});

interface StoredUser {
  id: string;
  username: string;
  knownAs: string;
  token: string;
}


// // Add a response interceptor to handle token
// api.interceptors.response.use(
//   (response) => response,
//   (error) => {
//     if (error.response?.status === 401) {
//       // Handle unauthorized access
//       localStorage.removeItem('token');
//       // Optionally redirect to login
//       window.location.href = '/login';
//     }
//     return Promise.reject(error);
//   }
// );

export const authService = {

  initializeAuth: () => {
    const storedUser = authService.getStoredUser();
    if (storedUser) {
      api.defaults.headers.common['Authorization'] = `Bearer ${storedUser.token}`;
      return storedUser;
    }
    return null;
  },
  // Add these helper methods
  setStoredUser: (user: StoredUser) => {
    localStorage.setItem('user', JSON.stringify(user));
    localStorage.setItem('token', user.token);
  },

  getStoredUser: (): StoredUser | null => {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  },

  clearStoredUser: () => {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
  },
  login: async (credentials: LoginCredentials): Promise<LoginResponse> => {
    try {
      const response = await api.post<LoginResponse>('/Account/login', credentials);

      // Store user data on successful login
      if (response.data) {
        authService.setStoredUser(response.data);
      }

      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        throw new Error(error.response?.data?.message || 'Login failed');
      }
      throw new Error('An unexpected error occurred');
    }
  },

  logout: () => {
    authService.clearStoredUser();
    delete api.defaults.headers.common['Authorization'];
  }

  // // Helper to check if user is authenticated
  // isAuthenticated: (): boolean => {
  //   const token = localStorage.getItem('token');
  //   return !!token; // Returns true if token exists
  // },

  // // Get stored token
  // getToken: (): string | null => {
  //   return localStorage.getItem('token');
  // },

};
