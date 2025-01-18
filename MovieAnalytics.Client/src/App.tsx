import { Routes, Route } from 'react-router-dom'
import './App.css'
import Home from "./views/Home"
import Movies from "./views/movies/Movies"
import Analytics from "./views/analytics/Analytics"
import Layout from './components/layout/Layout'
import { MoviesProvider } from './context/MoviesContext'
import { ThemeProvider } from "@/components/ThemeProvider"
import MovieDetail from './views/movies/MovieDetail'
import { AnalyticsProvider } from './context/AnalyticsContext'


function App() {

  return (
    <div>
      <ThemeProvider>
        <AnalyticsProvider>
          <MoviesProvider>
            <Layout>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/movies" element={<Movies />} />
                <Route path="/movies/:id" element={<MovieDetail />} />
                <Route path="/analytics" element={<Analytics />} />
              </Routes>
            </Layout>
          </MoviesProvider>
        </AnalyticsProvider>
      </ThemeProvider>
    </div>
  )
}

export default App
