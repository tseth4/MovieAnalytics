import { Routes, Route } from 'react-router-dom'
import './App.css'
import Home from "./views/Home"
import Movies from "./views/movies/Movies"
import Analytics from "./views/analytics/Analytics"
import Layout from './components/layout/Layout'
import { MoviesProvider } from './context/MoviesContext'
import { ThemeProvider } from "@/components/ThemeProvider"


function App() {

  return (
    <>
      <ThemeProvider>
        <MoviesProvider>
          <Layout>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/movies" element={<Movies />} />
              <Route path="/analytics" element={<Analytics />} />
            </Routes>
          </Layout>
        </MoviesProvider>
      </ThemeProvider>
    </>
  )
}

export default App
