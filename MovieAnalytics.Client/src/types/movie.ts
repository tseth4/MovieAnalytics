// export interface Movie {
//   id: string
//   title: string
//   year: number
//   rating: number
//   directorNames: string[]
//   writerNames: string[]
//   starNames: string[]
//   genreNames: string[]
// }
export interface Movie {
  id: string
  title: string
  movieLink: string
  year: number
  duration: string
  mpaRating: string
  rating: number
  votes: string
  budget: number
  grossWorldWide: number
  grossUsCanada: number
  openingWeekendGross: number
  wins: number
  nominations: number
  oscars: number
  directorNames: string[]
  writerNames: string[]
  starNames: string[]
  genreNames: string[]
  countryNames: string[]
  productionCompanies: string[]
  languages: string[]
}
