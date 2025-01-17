import { Button } from "@/components/ui/button"
import Layout from "@/components/layout/Layout"

import './App.css'

function App() {

  return (
    <>
      <Layout>
        <Button onClick={() => console.log("Hello World!")} >Test Button</Button>

      </Layout>
    </>
  )
}

export default App
