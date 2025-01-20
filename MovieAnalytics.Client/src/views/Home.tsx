import SlidingBackground from '@/components/sliding-background/SlidingBackground';
import { useNavigate } from 'react-router-dom';
import "./homestyles.css";

export default function Home() {
  const navigate = useNavigate()

  const movieImages = [
    '/avatar.jpg',
    '/endgame.jpg',
    '/infinitywar.jpg',
    '/jurassicworld.jpg',
    '/spiderman.jpg',
    '/titanic.jpg',
  ];


  return (
    <>
      <SlidingBackground images={movieImages} interval={5000} />

      <div className="hero">
        {/* <div className="floating-elements">
        <span className="float-item">🎬</span>
        <span className="float-item">🎥</span>
        <span className="float-item">🍿</span>
        <span className="float-item">🎭</span>
        <span className="float-item">📽️</span>
      </div> */}

        <div className="hero-content">
          <h1>Movie Analytics</h1>
          <p>Discover insights from over 33,000 movies</p>
          <button onClick={() => navigate('/movies')} className="explore-btn">
            Explore Movies
          </button>
        </div>
        <div>
          {/* <div style={{ position: 'relative', zIndex: 1, color: 'white', textAlign: 'center' }}>
          <h1>Welcome to Movie Analytics</h1>
          <p>Your personalized dashboard for movie trends and insights.</p>
        </div> */}
        </div>
      </div>
    </>

  )
}