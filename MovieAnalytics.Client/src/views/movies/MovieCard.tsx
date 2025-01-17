// components/MovieCard.tsx
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Movie } from "@/types/movie"
import { useEffect, useState } from "react";
import { tmdbService } from "@/services/api/tmdb";
import { useNavigate } from "react-router-dom";

interface MovieCardProps {
  movie: Movie
}

export function MovieCard({ movie }: MovieCardProps) {

  const navigate = useNavigate()


  const [poster, setPoster] = useState<string | null>(null);

  useEffect(() => {
    console.log("movie: ", movie)
    const fetchPoster = async () => {
      try {
        const movieData = await tmdbService.getMovieByImdbId(movie.id);
        // console.log("movieData: ", movieData)
        if (movieData?.poster_path) {
          let imagePath = tmdbService.getPosterUrl(movieData.poster_path)
          setPoster(imagePath);
        }
      } catch (error) {
        console.error('Failed to fetch poster:', error);
      }
    };

    fetchPoster();
    // console.log("poster from movie card: ", poster)
  }, [movie.id]);
  return (
    <Card className="flex flex-row overflow-hidden hover:shadow-lg cursor-pointer" onClick={() => navigate(`/movies/${movie.id}`)}>
      {/* Left side - could be for an image later */}
      <div className="w-48 bg-muted flex items-center justify-center">
        <div className="text-4xl">
          {poster ? (
            <img  src={poster} alt="Poster" />
          ) : (
            <span role="img" aria-label="No Poster">
              🎬
            </span>
          )}

        </div>
      </div>

      {/* Right side - movie info */}
      <div className="flex-1 p-6">
        <div className="flex justify-between items-start mb-4">
          <div className="flex gap-2 ">
            <p className="font-bold">{movie.title}</p>
            <p className="text-muted-foreground">{movie.year}</p>
          </div>
          <Badge variant="secondary">{movie.rating}</Badge>
        </div>
        <div className="space-y-2 flex-row items-start justify-start">
          <div className="items-start flex gap-2">
            <p className="text-sm font-semibold">Rating: </p>
            <p className="text-sm ">{movie.mpaRating}</p>
          </div>
        </div>
        <div className="space-y-2 flex-row items-start justify-start">
          <div className="items-start flex">
            <p className="text-sm font-semibold">Directors:      <span className="text-sm text-muted-foreground">{movie.directorNames.join(', ')}</span>
            </p>
          </div>

          <div className="items-start justify-start flex gap-2">
            <p className="text-sm font-semibold items-start flex">Genres:   </p>
            <div className="flex flex-wrap gap-2 mt-1">
              {movie.genreNames.map(genre => (
                <Badge key={genre} variant="outline">{genre}</Badge>
              ))}
            </div>
          </div>
        </div>
      </div>
    </Card>
  )
}