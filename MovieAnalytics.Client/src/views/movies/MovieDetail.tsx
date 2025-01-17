// pages/MovieDetail.tsx
import { useParams } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { Movie } from '@/types/movie'
import { movieService } from '@/services/api/MoviesService'
import { Badge } from '@/components/ui/badge'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { tmdbService } from '@/services/api/tmdb'
import { Button } from '@/components/ui/button'
import { Check } from 'lucide-react'


export default function MovieDetail() {
  const { id } = useParams()
  const [movie, setMovie] = useState<Movie | null>(null)
  const [poster, setPoster] = useState<string | null>(null);
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const data = await movieService.getMovie(id!)
        console.log("data: ", data)
        setMovie(data)
      } catch (error) {
        console.error('Failed to fetch movie:', error)
      } finally {
        setLoading(false)
      }
    }
    const fetchPoster = async () => {
      try {
        const movieData = await tmdbService.getMovieByImdbId(id!);
        // console.log("movieData: ", movieData)
        if (movieData?.poster_path) {
          let imagePath = tmdbService.getPosterUrl(movieData.poster_path)
          setPoster(imagePath);
        }
      } catch (error) {
        console.error('Failed to fetch poster:', error);
      }
    };
    fetchPoster()
    fetchMovie()
  }, [id])

  if (loading) return <div>Loading...</div>
  if (!movie) return <div>Movie not found</div>

  return (
    <Card className="border border-red-500 container mx-auto py-8">
      <div className="border border-red-500  rounded-lg shadow-lg p-6">
        <CardHeader className="border border-red-500 flex flex-row justify-between items-start mb-6">
          <CardTitle>
            <h1 className="text-3xl font-bold mb-2">{movie.title} <span className="text-gray-600 font-semibold">({movie.year})</span></h1>
            {/* <p className="text-gray-600">{movie.year}</p> */}
          </CardTitle>
          <Badge variant="secondary" className="text-lg">
            {movie.rating}
          </Badge>
        </CardHeader>

        <CardContent  className="border border-red-500 bg-muted flex justify-center">
          <div className="text-2xl">
            {poster ? (
              <img className="size-72 object-contain" src={poster} alt="Poster" />
            ) : (
              <span role="img" aria-label="No Poster">
                🎬
              </span>
            )}

          </div>
          <div className="border border-red-500">
            <div className="space-y-6">
              <div className="flex flex-row gap-2">
                <p className="font-semibold mb-2">Directors:</p>
                <p>{movie.directorNames.join(', ')}</p>
              </div>

              <div className="flex flex-row">
                <h2 className="text-xl font-semibold mb-2">Writers</h2>
                <p>{movie.writerNames.join(', ')}</p>
              </div>

              <div className="flex flex-row">
                <h2 className="text-xl font-semibold mb-2">Stars</h2>
                <p>{movie.starNames.join(', ')}</p>
              </div>

              <div className="flex flex-col items-start">
                <p className="text-m font-semibold mb-2">Genres</p>
                <div className="flex flex-wrap gap-2">
                  {movie.genreNames.map(genre => (
                    <Badge key={genre} variant="outline">
                      {genre}
                    </Badge>
                  ))}
                </div>
              </div>
            </div>

            {/* Add more sections as needed */}
          </div>
        </CardContent >
        <CardFooter>
        <Button className="w-full">
          <Check /> Mark all as read
        </Button>
      </CardFooter>


      </div>
    </Card>
  )
}