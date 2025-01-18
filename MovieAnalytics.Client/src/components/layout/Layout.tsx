import { SidebarInset, SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar"
import { AppSidebar } from "@/components/app-sidebar"

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <>
      <SidebarProvider>
        <AppSidebar />
        {/* <SidebarInset> */}
        <main>
          {/* <SidebarTrigger /> */}
          {children}
        </main>
        {/* </SidebarInset> */}

      </SidebarProvider>
    </>
  )
}