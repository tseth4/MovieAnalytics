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
        <div className="hero-content">
          <h1>Movie Analytics</h1>
          <p>Discover insights from over 33,000 movies</p>
          <button onClick={() => navigate('/movies')} className="explore-btn">
            Explore Movies
          </button>
        </div>
        <div>
        </div>
      </div>
    </>

  )
}