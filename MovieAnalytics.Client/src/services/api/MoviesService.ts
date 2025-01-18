import axios from 'axios'
import { API_URL } from './config'
import { FetchParams } from '@/types/fetchParams';
// import { Movie } from '@/types/movie'  // Your movie interface

const api = axios.create({
  baseURL: API_URL
});


export const movieService = {
  getMovies: async (page = 1, pageSize = 10, params?: FetchParams) => {
    let url = `/movies?pageNumber=${page}&pageSize=${pageSize}`;
    // Only add searchTerm if it exists and isn't empty
    if (params?.searchTerm) {
      url += `&searchTerm=${params.searchTerm}`
    }

    console.log("getting this url: ", url)

    const response = await api.get(url);

    const items = response.data;
    const paginationHeader = response.headers['pagination'];
    const pagination = paginationHeader ? JSON.parse(paginationHeader) : null;

    return { items, pagination };

  },
  getMovie: async (id: string) => {
    const response = await api.get(`/movies/${id}`)
    return response.data;

  }
}