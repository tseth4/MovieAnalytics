import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from "path"

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    outDir: '../MovieAnalytics.API/wwwroot', // Path to your .NET wwwroot folder
    emptyOutDir: true, // Clears the folder before building
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
})
