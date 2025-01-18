import { SidebarProvider } from "@/components/ui/sidebar"
import { AppSidebar } from "@/components/app-sidebar"

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <>
      <SidebarProvider className="w-full">
        <AppSidebar />
        <main className="w-full">
          {/* <SidebarTrigger /> */}
          {children}
        </main>
      </SidebarProvider>
    </>
  )
}