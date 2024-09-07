import '@/styles/index.css'
import { ThemeProvider } from "@/components/theme-provider"
import { router } from '@/routes'
import { RouterProvider } from 'react-router-dom'

export function App() {
  return (
    <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <RouterProvider router={router} />
    </ThemeProvider>
  )
}
