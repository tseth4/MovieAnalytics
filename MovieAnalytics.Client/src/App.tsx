import { ThemeProvider } from "@/components/ThemeProvider"
import { Route, Routes } from 'react-router-dom'
import './App.css'
import Layout from './components/layout/Layout'
import { AnalyticsProvider } from './context/AnalyticsContext'
import { MoviesProvider } from './context/MoviesContext'
import Analytics from "./views/analytics/Analytics"
import Home from "./views/Home"
import MovieDetail from './views/movies/MovieDetail'
import Movies from "./views/movies/Movies"


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
