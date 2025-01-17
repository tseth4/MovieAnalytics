// src/components/MovieCard.tsx
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"

interface MovieCardProps {
  movie: {
    title: string;
    year: number;
    rating: number;
    genres: string[];
  }
}

export function MovieCard({ movie }: MovieCardProps) {
  return (
    <Card className="h-full">
      <CardHeader>
        <CardTitle className="flex justify-between items-start">
          <span>{movie.title}</span>
          <Badge variant="secondary">{movie.rating}</Badge>
        </CardTitle>
      </CardHeader>
      <CardContent>
        <div className="flex gap-2 flex-wrap">
          {movie.genres.map((genre) => (
            <Badge key={genre} variant="outline">
              {genre}
            </Badge>
          ))}
        </div>
      </CardContent>
    </Card>
  )
}