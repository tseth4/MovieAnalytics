import axios from "axios";

const TMDB_API_KEY = import.meta.env.VITE_TMDB_API_KEY;
const TMDB_BASE_URL = 'https://api.themoviedb.org/3';

export const tmdbService = {
  getMovieByImdbId: async (imdbId: string) => {
    const response = await axios.get(
      `${TMDB_BASE_URL}/find/${imdbId}?api_key=${TMDB_API_KEY}&external_source=imdb_id`
    );
    return response.data.movie_results[0];
  },

  getPosterUrl: (posterPath: string) => {
    // console.log(`poster URL: https://image.tmdb.org/t/p/original${posterPath}`)
    return `https://image.tmdb.org/t/p/original${posterPath}`;
  },
  getBackdropUrl: (backdropPath: string) => {
    // console.log(`poster URL: https://image.tmdb.org/t/p/original${posterPath}`)
    return `https://image.tmdb.org/t/p/original${backdropPath}`;
  }
};

// https://image.tmdb.org/t/p/original/tJLV3BAlHOgscVOrA99Wnb2gAef.jpg