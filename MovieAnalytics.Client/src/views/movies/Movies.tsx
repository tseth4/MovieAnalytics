import { useEffect, useState } from "react"
import { MovieCard } from "./MovieCard"
import { useMovies } from "@/context/MoviesContext"
import { Pagination } from "@/components/Pagination"
import debounce from 'lodash/debounce'


export default function Movies() {
  const { movies, currentPage, totalPages, loading, error, fetchMovies } = useMovies()
  const [searchTerm, setSearchTerm] = useState('')


  // Create debounced search function
  const debouncedSearch = debounce((term: string) => {
    console.log("dfetch: ", term)
    fetchMovies(1, { searchTerm: term }) // Reset to page 1 when searching
  }, 500)

  useEffect(() => {
    fetchMovies(currentPage, { searchTerm })
  }, [currentPage])

  // useEffect(() => {
  //   fetchMovies(currentPage)
  // }, [currentPage])

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value
    setSearchTerm(value)
    debouncedSearch(value)
  }

  const handlePageChange = (page: number) => {
    fetchMovies(page)
  }


  if (loading) return <div>Loading...</div>
  if (error) return <div>Error: {error}</div>
  return (
    <>
      <div className="container mx-auto py-8 px-4">
        <h1 className="text-3xl font-bold mb-8">Movies</h1>
        <input
          type="text"
          placeholder="Search movies..."
          value={searchTerm}
          onChange={handleSearch}
          className="p-2 border rounded w-64"
        />
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