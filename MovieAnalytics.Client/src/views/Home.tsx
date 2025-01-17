// import Layout from "@/components/layout/Layout";
import { Button } from "@/components/ui/button";

export default function Home() {
  return (
    <>
      {/* <Layout> */}
      Welcome home!
        <Button onClick={() => console.log("Hello World!")} >Test Button</Button>
        {/* <MovieCard
          movie={movie}
        /> */}

      {/* </Layout> */}
    </>
  )
}