import { RouterProvider, createBrowserRouter } from "react-router-dom";
import React from "react";
import ReactDOM from "react-dom/client";
import "./Common/Utils/index.css";
import App from "./UserApp/Pages/App";
import ErrorPage from "./Common/Pages/ErrorPage";
import MainPage, {
  loader as mainLoader,
} from "./UserApp/Pages/MainPage/MainPage";
import CardsPage, {
  loader as cardsLoader,
} from "./UserApp/Pages/CardsPage/CardsPage";
import AccountsPage, {
  loader as accountsLoader,
} from "./UserApp/Pages/AccountsPage/AccountsPage";
import CreditsPage, {
  loader as creditsLoader,
} from "./UserApp/Pages/CreditsPage/CreditsPage";
import DepositsPage, {
  loader as depositsLoader,
} from "./UserApp/Pages/DepositsPage/DepositsPage";
import ProfilePage, {
  loader as userLoader,
} from "./UserApp/Pages/ProfilePage/ProfilePage";
import LoginPage from "./Common/Pages/LoginPage";
import SignUpPage from "./UserApp/Pages/SignUpPage/SignUpPage";
import CardInfo, {
  loader as cardInfoLoader,
} from "./UserApp/Pages/CardsPage/CardInfoPage";
import AddCardPage, {
  loader as addCardLoader,
} from "./UserApp/Pages/CardsPage/AddCardPage";
import AddAccountPage, {
  loader as addAccountLoader,
} from "./UserApp/Pages/AccountsPage/AddAccountPage";
import AccountInfoPage, {
  loader as accInfoLoader,
} from "./UserApp/Pages/AccountsPage/AccountInfoPage";
import CreditInfoPage, {
  loader as creditInfoLoader,
} from "./UserApp/Pages/CreditsPage/CreditInfoPage";
// import AddCreditPage, {loader as addCreditLoader} from "./UserApp/Pages/CreditsPage/AddCreditPage";

import ModeratorApp, {loader as appModeratorLoader} from "./ModeratorApp/Pages/App";
import {
  ProfilePage as ModerProfilePage,
  loader as moderProfileLoader,
} from "./ModeratorApp/Pages/ProfilePage/ProfilePage";
import {
  UsersInfo as ModerUsersInfo,
  loader as moderUsersInfoLoader,
} from "./ModeratorApp/Pages/UsersInfo/UsersInfo";
import CreditRequests from "./ModeratorApp/Pages/CreditRequests/CreditRequests";

import AdminApp, {loader as adminAppLoader} from "./AdminApp/Pages/App";
import {
  ProfilePage as AdminProfilePage,
  loader as adminProfileLoader,
} from "./AdminApp/Pages/ProfilePage/ProfilePage";
import {
  Users as AdminUsersInfo,
  loader as adminUsersInfoLoader,
} from "./AdminApp/Pages/UsersInfo/Users";
import ModeratorsPage, {
  loader as moderatorsLoader,
} from "./AdminApp/Pages/ModeratorsPage/ModeratorsPage";
import ManagementPage, {
  loader as managementLoader,
} from "./AdminApp/Pages/ManagementPage/ManagementPage";
import AddPackagePage from "./AdminApp/Pages/ManagementPage/Components/AddPackagePage";
import EditPackagePage, {
  loader as editPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/EditPackagePage";
import DelPackagePage, {
  loader as delPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/DelPackagePage";
import AddModeratorPage from "./AdminApp/Pages/ModeratorsPage/Components/AddModeratorPage";
import EditModeratorPage, {
  loader as editModeratorLoader,
} from "./AdminApp/Pages/ModeratorsPage/Components/EditModeratorPage";
import DelModeratorPage, {
  loader as delModeratorLoader,
} from "./AdminApp/Pages/ModeratorsPage/Components/DelModeratorPage";
import ModeratorInfoPage, {
  loader as moderatorInfoLoader,
} from "./AdminApp/Pages/ModeratorsPage/ModeratorInfoPage";

const routers = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <MainPage />,
        loader: mainLoader,
      },
      {
        path: "cards",
        element: <CardsPage />,
        loader: cardsLoader,
      },
      {
        path: "cards/:cardId",
        element: <CardInfo />,
        loader: cardInfoLoader,
      },
      {
        path: "cards/create",
        element: <AddCardPage />,
        loader: addCardLoader,
      },
      {
        path: "accounts",
        element: <AccountsPage />,
        loader: accountsLoader,
      },
      {
        path: "accounts/:accountId",
        element: <AccountInfoPage />,
        loader: accInfoLoader,
      },
      {
        path: "accounts/create",
        element: <AddAccountPage />,
        loader: addAccountLoader,
      },
      {
        path: "credits",
        element: <CreditsPage />,
        loader: creditsLoader,
      },
      {
        path: "credits/:accountId",
        element: <CreditInfoPage />,
        loader: creditInfoLoader,
      },
      // {
      //   path: "credits/add",
      //   element: <AddCreditPage />,
      //   loader: addCreditLoader
      // },
      {
        path: "deposits",
        element: <DepositsPage />,
        loader: depositsLoader,
      },
      {
        path: "profile",
        element: <ProfilePage />,
        loader: userLoader,
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
  {
    path: "/moderator",
    element: <ModeratorApp />,
    errorElement: <ErrorPage />,
    loader: appModeratorLoader,
    children: [
      {
        path: "credit-requests",
        element: <CreditRequests />,
      },
      {
        path: "users-info",
        element: <ModerUsersInfo />,
        loader: moderUsersInfoLoader,
      },
      {
        path: "profile",
        element: <ModerProfilePage />,
        loader: moderProfileLoader,
      },
    ],
  },
  {
    path: "/admin",
    element: <AdminApp />,
    errorElement: <ErrorPage />,
    loader: adminAppLoader,
    children: [
      {
        path: "management",
        element: <ManagementPage />,
        loader: managementLoader,
      },
      {
        path: "management/add-package",
        element: <AddPackagePage />,
      },
      {
        path: "management/edit-package",
        element: <EditPackagePage />,
        loader: editPackageLoader,
      },
      {
        path: "management/delete-package",
        element: <DelPackagePage />,
        loader: delPackageLoader,
      },
      {
        path: "users",
        element: <AdminUsersInfo />,
        loader: adminUsersInfoLoader,
      },
      {
        path: "moderators",
        element: <ModeratorsPage />,
        loader: moderatorsLoader,
      },
      {
        path: "moderators/:moderatorId",
        element: <ModeratorInfoPage />,
        loader: moderatorInfoLoader,
      },
      {
        path: "moderators/add",
        element: <AddModeratorPage />,
      },
      {
        path: "moderators/edit",
        element: <EditModeratorPage />,
        loader: editModeratorLoader,
      },
      {
        path: "moderators/delete",
        element: <DelModeratorPage />,
        loader: delModeratorLoader,
      },
      {
        path: "profile",
        element: <AdminProfilePage />,
        loader: adminProfileLoader,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={routers} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
// reportWebVitals();
