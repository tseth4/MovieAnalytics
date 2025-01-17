import axios from 'axios'
import { API_URL } from './config'
// import { Movie } from '@/types/movie'  // Your movie interface

const api = axios.create({
  baseURL: API_URL
});


export const movieService = {
  getMovies: async (page = 1, pageSize = 10) => {
    const response = await api.get(`/movies?pageNumber=${page}&pageSize=${pageSize}`)

    const items = response.data;
    const paginationHeader = response.headers['pagination'];
    const pagination = paginationHeader ? JSON.parse(paginationHeader) : null;

    return { items, pagination };

  }
}