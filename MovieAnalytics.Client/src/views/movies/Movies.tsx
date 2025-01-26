import { Pagination } from "@/components/Pagination"
import { useMovies } from "@/context/MoviesContext"
import { useEffect, useState } from "react"
import { MovieCard } from "./MovieCard"
// import debounce from 'lodash/debounce'
import { Input } from "@/components/ui/input"
import { Movie } from "@/types/movie"

interface MoviesProps {
  firstMovies: Movie[]
}

export default function Movies({ firstMovies }: MoviesProps) {
  const { movies, currentPage, totalPages, loading, error, fetchMovies } = useMovies()
  const [searchTerm, setSearchTerm] = useState('');
  const [isInitialRender, setIsInitialRender] = useState(true);




  useEffect(() => {
    if (isInitialRender) {
      setIsInitialRender(false); // After the first render, mark initial render as complete
    } else {
      fetchMovies(currentPage, { searchTerm });
    }
  }, [currentPage, searchTerm, fetchMovies])

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  const handleSearchClick = () => {
    fetchMovies(1, { searchTerm }); // Reset to page 1 when searching
  };

  const handlePageChange = (page: number) => {
    fetchMovies(page, { searchTerm });
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSearchClick();
    }
  };
  const displayedMovies = isInitialRender ? firstMovies : movies;


  if (loading) return <div>Loading...</div>
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
            onClick={handleSearchClick}
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