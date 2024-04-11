import { RouterProvider, createBrowserRouter } from "react-router-dom";
import React, { Children } from "react";
import ReactDOM from "react-dom/client";
import "./utils/index.css";
import App from "./App";
import { AuthProvider } from "./store/AuthContext/AuthProvider";
import ErrorPage from "./pages/ErrorPage/ErrorPage";
import MainPage from "./pages/MainPage/MainPage";
import CardsPage, { cardsLoader } from "./pages/CardsPage/CardsPage";
import AccountsPage from "./pages/AccountsPage/AccountsPage";
import CreditsPage from "./pages/CreditsPage/CreditsPage";
import DepositsPage from "./pages/DepositsPage/DepositsPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import LoginPage from "./pages/LoginPage/LoginPage";
import SignUpPage from "./pages/SignUpPage/SignUpPage";
import CardInfo from "./pages/CardsPage/Components/CardInfo";

const routers = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <MainPage />,
      },
      {
        path: "cards",
        element: <CardsPage />,
        loader: cardsLoader,
      },
      {
        path: "cards/:cardId",
        element: <CardInfo />,
      },
      {
        path: "accounts",
        element: <AccountsPage />,
      },
      {
        path: "credits",
        element: <CreditsPage />,
      },
      {
        path: "deposits",
        element: <DepositsPage />,
      },
      {
        path: "profile",
        element: <ProfilePage />,
      },
      {
        path: "login",
        element: <LoginPage />,
      },
      {
        path: "register",
        element: <SignUpPage />,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={routers} />
    </AuthProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
// reportWebVitals();
