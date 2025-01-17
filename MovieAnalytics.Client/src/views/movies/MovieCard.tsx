// components/MovieCard.tsx
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Movie } from "@/types/movie"

interface MovieCardProps {
  movie: Movie
}

export function MovieCard({ movie }: MovieCardProps) {
  return (
    <Card className="flex flex-row overflow-hidden">
      {/* Left side - could be for an image later */}
      <div className="w-48 bg-muted flex items-center justify-center">
        <div className="text-4xl">🎬</div>
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