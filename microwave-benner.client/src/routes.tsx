import { createBrowserRouter } from 'react-router-dom'

import { AppLayout } from '@/pages/_layouts/app'
import { Error } from '@/pages/error'
import { Home } from '@/pages/home'
import { NotFound } from '@/pages/not-found'
import { Programs } from '@/pages/programs'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <AppLayout />,
    errorElement: <Error />,
    children: [
      {
        path: '/',
        element: <Home />,
      },
      {
        path: '/programs',
        element: <Programs />,
      },
    ],
  },
  {
    path: '*',
    element: <NotFound />,
  },
])
