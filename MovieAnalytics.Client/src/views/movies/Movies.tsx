import { useEffect } from "react"
import { MovieCard } from "./MovieCard"
import { useMovies } from "@/context/MoviesContext"
import { Pagination } from "@/components/Pagination"


export default function Movies() {
  const { movies, currentPage, totalPages, loading, error, fetchMovies } = useMovies()


  useEffect(() => {
    fetchMovies(currentPage)
  }, [currentPage])

  const handlePageChange = (page: number) => {
    fetchMovies(page)
  }


  if (loading) return <div>Loading...</div>
  if (error) return <div>Error: {error}</div>
  return (
    <>
      <div className="container mx-auto py-8 px-4">
        <h1 className="text-3xl font-bold mb-8">Movies</h1>
        <div className="flex flex-col gap-4">
          {movies.map(movie => (
            <MovieCard key={movie.id} movie={movie} />
          ))}
        </div>
        <Pagination
          currentPage={currentPage}
          totalPages={totalPages}
          onPageChange={handlePageChange}
        />
      </div>
    </>
  )
}