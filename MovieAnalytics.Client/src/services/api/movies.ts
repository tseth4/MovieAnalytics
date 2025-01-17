import axios from 'axios'
import { API_URL } from './config'
import { Movie } from '@/types/movie'  // Your movie interface

const api = axios.create({
    baseURL: API_URL
})

export const movieService = {
    getMovies: async () => {
        const response = await api.get<Movie[]>('/movies')
        return response.data
    },
    
    // getMovie: async (id: string) => {
    //     const response = await api.get<Movie>(`/movies/${id}`)
    //     return response.data
    // },

}