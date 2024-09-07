import { createBrowserRouter } from "react-router-dom";
import { NotFound } from "@/pages/not-found";
import { Error } from "@/pages/error";
import { AppLayout } from "@/pages/_layouts/app";
import { Home } from "@/pages/home";

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
      ],
    },
    {
      path: '*',
      element: <NotFound />,
    },
  ])