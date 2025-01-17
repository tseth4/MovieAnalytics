import { Button } from "@/components/ui/button"
import Layout from "@/components/layout/Layout"

import './App.css'
import { MovieCard } from "./components/MovieCard"

function App() {

  let movie = {
    title: "Example Title",
    year: 1980,
    rating: 10,
    genres: ["Horror", "Thriller"]
  }

  return (
    <>
      <Layout>
        <Button onClick={() => console.log("Hello World!")} >Test Button</Button>
        {/* <MovieCard
          movie={movie}
        /> */}

      </Layout>
    </>
  )
}

export default App
