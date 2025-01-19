import { useNavigate } from 'react-router-dom'
import "./homestyles.css"

export default function Home() {
  const navigate = useNavigate()  // Get the navigate function

  
  return (
    <div className="hero">
      {/* Floating Background Elements */}
      <div className="floating-elements">
        {/* Create some movie-related symbols that float around */}
        <span className="float-item">🎬</span>
        <span className="float-item">🎥</span>
        <span className="float-item">🍿</span>
        <span className="float-item">🎭</span>
        <span className="float-item">📽️</span>
      </div>

      <div className="hero-content">
        <h1>Movie Analytics</h1>
        <p>Discover insights from over 33,000 movies</p>
        <button onClick={() => navigate('/movies')} className="explore-btn">
          Explore Movies
        </button>
      </div>
    </div>
  )
}