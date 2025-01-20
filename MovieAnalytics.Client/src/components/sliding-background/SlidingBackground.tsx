import React, { useEffect, useState } from 'react';
import './SlidingBackground.css';

interface SlidingBackgroundProps {
  images: string[]; // Array of image URLs
  interval?: number; // Time interval for each slide (in milliseconds)
}

const SlidingBackground: React.FC<SlidingBackgroundProps> = ({ images, interval = 5000 }) => {
  const [currentIndex, setCurrentIndex] = useState(0);

  useEffect(() => {
    const slideInterval = setInterval(() => {
      setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
    }, interval);

    return () => clearInterval(slideInterval); // Cleanup on component unmount
  }, [images.length, interval]);

  return (
    <div className="background-slider">
      {images.map((image, index) => (
        <div
          key={index}
          className={`slide ${index === currentIndex ? 'active' : ''}`}
          style={{ backgroundImage: `url(${image})` }}
        ></div>
      ))}
    </div>
  );
};

export default SlidingBackground;
