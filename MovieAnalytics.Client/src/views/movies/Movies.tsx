import { Pagination } from "@/components/Pagination"
import { useMovies } from "@/context/MoviesContext"
import { useEffect, useState } from "react"
import { MovieCard } from "./MovieCard"
// import debounce from 'lodash/debounce'
import Loader from "@/components/Loader"
import { Input } from "@/components/ui/input"

export default function Movies() {
  const { movies, currentPage, totalPages, loading, error, fetchMovies } = useMovies()
  const [searchTerm, setSearchTerm] = useState('');
  const [searchQuery, setSearchQuery] = useState('');

  // On mount, fetch movies if not already loaded
  useEffect(() => {
    if (movies.length === 0) {
      fetchMovies(currentPage, { searchTerm: searchQuery });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    fetchMovies(currentPage, { searchTerm: searchQuery });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentPage, searchQuery]);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  const handleSearchSubmit = () => {
    setSearchQuery(searchTerm); // Only update query when Enter is pressed or button is clicked
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSearchSubmit();
    }
  };

  const handlePageChange = (page: number) => {
    fetchMovies(page, { searchTerm: searchQuery });
  };

  // Always use movies from context
  const displayedMovies = movies;

  if (loading) return <div><Loader/></div>
  if (error) return <div>Error: {error}</div>
  return (
    <>
      <div className="container mx-auto py-8 px-4">
        {/* <h1 className="text-3xl font-bold mb-8">Movies</h1> */}
        <div className="flex gap-4  mb-8  h-12">
          <Input
            type="text"
            placeholder="Search movies by titles, director, genre"
            value={searchTerm}
            onChange={handleSearchChange}
            onKeyDown={handleKeyDown}
            className="border rounded h-full"
          />
          <button
            onClick={handleSearchSubmit}
            className="bg-blue-500 text-white rounded"
          >Search</button>
        </div>
        {!loading && !error && displayedMovies.length === 0 && (
          <div className="text-center text-gray-500">
            No movies found for "{searchTerm}"
          </div>
        )}

        <div className="flex flex-col gap-4">
          {displayedMovies.map(movie => (
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