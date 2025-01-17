// import { Button } from "@/components/ui/button"
// import Layout from "@/components/layout/Layout"
import { Routes, Route } from 'react-router-dom'


import './App.css'
// import { MovieCard } from "./components/MovieCard"
import Home from "./views/Home"
import Movies from "./views/movies/Movies"
import Analytics from "./views/analytics/Analytics"
import Layout from './components/layout/Layout'

function App() {

  // let movie = {
  //   title: "Example Title",
  //   year: 1980,
  //   rating: 10,
  //   genres: ["Horror", "Thriller"]
  // }

  return (
    <>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/movies" element={<Movies />} />
          <Route path="/analytics" element={<Analytics />} />
        </Routes>
      </Layout>
    </>
  )
}

export default App
