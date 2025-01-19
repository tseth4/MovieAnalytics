// pages/MovieDetail.tsx
import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { movieService } from '@/services/api/MoviesService'
import { tmdbService } from '@/services/api/tmdb'
import { Movie } from '@/types/movie'
import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'



export default function MovieDetail() {
  const { id } = useParams()
  const [movie, setMovie] = useState<Movie | null>(null)
  // const [poster, setPoster] = useState<string | null>(null);
  const [overview, setOverview] = useState<string | null>(null);
  const [backdrop, setBackdrop] = useState<string | null>(null);
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const data = await movieService.getMovie(id!)
        setMovie(data)
      } catch (error) {
        console.error('Failed to fetch movie:', error)
      } finally {
        setLoading(false)
      }
    }
    const fetchPosterOverviewBackdrop = async () => {
      try {
        const movieData = await tmdbService.getMovieByImdbId(id!);

        if (movieData?.overview) {
          setOverview(movieData.overview)
        }
        if (movieData?.backdrop_path) {
          let backdropPath = tmdbService.getBackdropUrl(movieData.backdrop_path)
          setBackdrop(backdropPath)
        }
      } catch (error) {
        console.error('Failed to fetch poster:', error);
      }
    };
    fetchPosterOverviewBackdrop()
    fetchMovie()
  }, [id])

  if (loading) return <div>Loading...</div>
  if (!movie) return <div>Movie not found</div>

  return (
    <Card className="container mx-auto py-8">
      <div className="rounded-lg shadow-lg p-6">
        <CardHeader className="flex flex-row justify-between items-start mb-6">
          <CardTitle>
            <h1 className="text-3xl font-bold mb-2">{movie.title} <span className="text-gray-600 font-semibold">({movie.year})</span></h1>
            {/* <p className="text-gray-600">{movie.year}</p> */}
          </CardTitle>
          <Badge variant="secondary" className="text-lg">
            {movie.rating}
          </Badge>
        </CardHeader>

        <CardContent className="bg-muted flex-col justify-center py-5">
          <div className="text-2xl">
            {backdrop ? (
              <img className="w-full object-contain" src={backdrop} alt="Poster" />
            ) : (
              <span role="img" aria-label="No Poster">
                🎬
              </span>
            )}

          </div>
          <div >
            <div className="space-y-6">
              <div className="items-start flex flex-col mt-5">
                <p className="font-semibold">Directors:</p>
                <p>{movie.directorNames.join(', ')}</p>
              </div>
              <div className="items-start flex flex-col">
                <p className="font-semibold">Stars:</p>
                <p>{movie.starNames.join(', ')}</p>
              </div>

              <div className="flex flex-col items-start">
                <div className="items-start flex text-left text-sm py-1">
                  {overview ? (
                    <p>{overview}</p>
                  ) : (
                    <p>
                      Missing info
                    </p>
                  )}
                </div>
                <div className="flex flex-wrap gap-2 py-4">
                  {movie.genreNames.map(genre => (
                    <Badge key={genre} variant="destructive">
                      {genre}
                    </Badge>
                  ))}
                </div>
              </div>
            </div>

            {/* Add more sections as needed */}
            <div className="items-start flex flex-col py-6">
              <Link to={movie.movieLink}>
                <Button className="w-full">
                  IMDB
                </Button>
              </Link>
            </div>

          </div>
        </CardContent >
        <CardFooter>

        </CardFooter>


      </div>
    </Card>
  )
}