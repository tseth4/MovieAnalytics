import React from 'react';

type LoaderSize = 'sm' | 'md' | 'lg';

interface CuteLoaderProps {
  /** Size of the loader component */
  size?: LoaderSize;
  /** Optional className for additional styling */
  className?: string;
  /** Optional loading text override */
  loadingText?: string;
}

// Size classes mapping as a const to ensure type safety
const SIZE_CLASSES: Record<LoaderSize, string> = {
  sm: "w-16 h-16",
  md: "w-24 h-24",
  lg: "w-32 h-32"
} as const;

const CuteLoader: React.FC<CuteLoaderProps> = ({ 
  size = "md",
  className = "",
  loadingText = "Loading..."
}) => {
  // Generate array of indices for stars
  const starIndices = Array.from({ length: 3 }, (_, i) => i);

  return (
    <div className={`flex flex-col items-center justify-center gap-4 ${className}`}>
      {/* Main container */}
      <div className={`relative ${SIZE_CLASSES[size]}`}>
        {/* Shadow that squishes with animation */}
        <div className="absolute bottom-0 left-1/2 -translate-x-1/2 w-1/2 h-2 bg-gray-200 rounded-full animate-pulse"></div>
        
        {/* Cute hamster face */}
        <div className="absolute inset-0 animate-bounce" style={{ animationDuration: '2s' }}>
          {/* Main face circle */}
          <div className="absolute inset-0 bg-amber-200 rounded-full">
            {/* Inner ear (left) */}
            <div className="absolute -top-2 left-3 w-4 h-4 bg-amber-300 rounded-full transform -rotate-45"></div>
            {/* Inner ear (right) */}
            <div className="absolute -top-2 right-3 w-4 h-4 bg-amber-300 rounded-full transform rotate-45"></div>
            
            {/* White circles for cheeks */}
            <div className="absolute bottom-5 left-2 w-5 h-5 bg-pink-100 rounded-full opacity-70"></div>
            <div className="absolute bottom-5 right-2 w-5 h-5 bg-pink-100 rounded-full opacity-70"></div>
            
            {/* Eyes that blink */}
            <div className="absolute top-8 left-4 w-3 h-3 bg-gray-800 rounded-full animate-pulse"></div>
            <div className="absolute top-8 right-4 w-3 h-3 bg-gray-800 rounded-full animate-pulse"></div>
            
            {/* Cute nose */}
            <div className="absolute top-10 left-1/2 -translate-x-1/2 w-2 h-2 bg-pink-400 rounded-full"></div>
            
            {/* Happy mouth */}
            <div className="absolute top-12 left-1/2 -translate-x-1/2 w-4 h-2 border-b-2 border-gray-800 rounded-full"></div>
            
            {/* Whiskers */}
            <div className="absolute top-11 left-2 w-3 h-px bg-gray-400 transform -rotate-12"></div>
            <div className="absolute top-12 left-2 w-3 h-px bg-gray-400"></div>
            <div className="absolute top-13 left-2 w-3 h-px bg-gray-400 transform rotate-12"></div>
            <div className="absolute top-11 right-2 w-3 h-px bg-gray-400 transform rotate-12"></div>
            <div className="absolute top-12 right-2 w-3 h-px bg-gray-400"></div>
            <div className="absolute top-13 right-2 w-3 h-px bg-gray-400 transform -rotate-12"></div>
          </div>
        </div>

        {/* Little stars that twinkle around */}
        {starIndices.map((i) => (
          <div
            key={i}
            className="absolute animate-ping"
            style={{
              top: `${20 + i * 30}%`,
              left: `${80 + i * 10}%`,
              animationDelay: `${i * 0.3}s`,
              fontSize: '0.75rem'
            }}
          >
            ✨
          </div>
        ))}
      </div>

      {/* Cute loading text */}
      <div className="flex items-center gap-1 text-amber-500 font-medium">
        {loadingText.split('').map((letter, i) => (
          <span
            key={i}
            className="animate-bounce"
            style={{ animationDelay: `${i * 0.1}s` }}
          >
            {letter}
          </span>
        ))}
      </div>
    </div>
  );
};

export default CuteLoader;