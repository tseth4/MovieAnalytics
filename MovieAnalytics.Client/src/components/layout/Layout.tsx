const navItem = [
  { label: 'Home', href: '/' },
  { label: 'Movies', href: '/movies' },
  { label: 'Profile', href: '/profile' },
  { label: 'Analytics', href: '/analytics' },
]

import NavBar from "../nav/NavBar"

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <>
      <NavBar
        brandName="{MA}"
        items={navItem}
      />
      <main className="py-12">
        {children}
      </main>

    </>
  )
}