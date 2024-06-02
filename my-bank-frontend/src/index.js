import { RouterProvider, createBrowserRouter } from "react-router-dom";
import React from "react";
import ReactDOM from "react-dom/client";
import "./Common/Utils/index.css";
import App from "./UserApp/Pages/App";
import ErrorPage from "./UserApp/Pages/ErrorPage";
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
import AddCreditPage, {
  loader as addCreditLoader,
} from "./UserApp/Pages/CreditsPage/AddCreditPage";
import AddDepositPage, {
  loader as addDepositLoader,
} from "./UserApp/Pages/DepositsPage/AddDepositPage";
import DepositInfoPage, {
  loader as depositInfoLoader,
} from "./UserApp/Pages/DepositsPage/DepositInfoPage";
import MessagesPage, {
  loader as messagesLoader,
} from "./UserApp/Pages/MessagesPage/MessagesPage";
import MakeTransactionPage, {
  loader as makeTransactionLoader,
} from "./UserApp/Pages/TransactionsPage/MakeTransactionPage";

import ModeratorApp, {
  loader as appModeratorLoader,
} from "./ModeratorApp/Pages/App";
import {
  ProfilePage as ModerProfilePage,
  loader as moderProfileLoader,
} from "./ModeratorApp/Pages/ProfilePage/ProfilePage";
import {
  ModeratorUsersPage,
  loader as moderUsersInfoLoader,
} from "./ModeratorApp/Pages/UsersPage/UsersPage";
import CreditRequests, {
  loader as creditRequestsLoader,
} from "./ModeratorApp/Pages/CreditRequests/CreditRequests";
import ModerMessagesPage, {
  loader as moderMessagesLoader,
} from "./ModeratorApp/Pages/MessagesPage/MessagesPage";
import ModeratorUserInfoPage, {
  loader as moderUserInfoLoader,
} from "./ModeratorApp/Pages/UsersPage/UserInfoPage";
import ModeratorCreditsPage, {
  loader as moderCreditsLoader,
} from "./ModeratorApp/Pages/CreditsPage/CreditsPage";
import ModeratorCreditInfoPage, {
  loader as moderCreditInfoLoader,
} from "./ModeratorApp/Pages/CreditsPage/CreditInfoPage";

import AdminApp, { loader as adminAppLoader } from "./AdminApp/Pages/App";
import {
  ProfilePage as AdminProfilePage,
  loader as adminProfileLoader,
} from "./AdminApp/Pages/ProfilePage/ProfilePage";
import {
  AdminUsersPage as AdminUsersInfo,
  loader as adminUsersInfoLoader,
} from "./AdminApp/Pages/UsersPage/UsersPage";
import ModeratorsPage, {
  loader as moderatorsLoader,
} from "./AdminApp/Pages/ModeratorsPage/ModeratorsPage";
import ManagementPage, {
  loader as managementLoader,
} from "./AdminApp/Pages/ManagementPage/ManagementPage";
import AddCardPackagePage from "./AdminApp/Pages/ManagementPage/Components/AddCardPackagePage";
import EditCardPackagePage, {
  loader as editCardPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/EditCardPackagePage";
import DelCardPackagePage, {
  loader as delPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/DelCardPackagePage";
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
import AdminUserInfoPage, {
  loader as adminUserInfoLoader,
} from "./AdminApp/Pages/UsersPage/UserInfoPage";
import AdminMessagesPage, {
  loader as adminMessagesLoader,
} from "./AdminApp/Pages/MessagesPage/MessagesPage";
import AddCreditPackagePage, {
  loader as addCreditPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/AddCreditPackagePage";
import EditCreditPackagePage, {
  loader as editCreditPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/EditCreditPackagePage";
import AddDepositPackagePage, {
  loader as addDepositPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/AddDepositPackagePage";
import EditDepositPackagePage, {
  loader as editDepositPackageLoader,
} from "./AdminApp/Pages/ManagementPage/Components/EditDepositPackagePage";

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
        path: "make-transaction",
        element: <MakeTransactionPage />,
        loader: makeTransactionLoader,
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
      {
        path: "credits/add",
        element: <AddCreditPage />,
        loader: addCreditLoader,
      },
      {
        path: "deposits",
        element: <DepositsPage />,
        loader: depositsLoader,
      },
      {
        path: "deposits/:accountId",
        element: <DepositInfoPage />,
        loader: depositInfoLoader,
      },
      {
        path: "deposits/add",
        element: <AddDepositPage />,
        loader: addDepositLoader,
      },
      {
        path: "messages",
        element: <MessagesPage />,
        loader: messagesLoader,
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
        loader: creditRequestsLoader,
      },
      {
        path: "users",
        element: <ModeratorUsersPage />,
        loader: moderUsersInfoLoader,
      },
      {
        path: "users/:userId",
        element: <ModeratorUserInfoPage />,
        loader: moderUserInfoLoader,
      },
      {
        path: "credits",
        element: <ModeratorCreditsPage />,
        loader: moderCreditsLoader,
      },
      {
        path: "credits/:accountId",
        element: <ModeratorCreditInfoPage />,
        loader: moderCreditInfoLoader,
      },
      {
        path: "messages",
        element: <ModerMessagesPage />,
        loader: moderMessagesLoader,
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
        path: "management/add-card-package",
        element: <AddCardPackagePage />,
      },
      {
        path: "management/edit-card-package",
        element: <EditCardPackagePage />,
        loader: editCardPackageLoader,
      },
      {
        path: "management/add-credit-package",
        element: <AddCreditPackagePage />,
        loader: addCreditPackageLoader,
      },
      {
        path: "management/edit-credit-package",
        element: <EditCreditPackagePage />,
        loader: editCreditPackageLoader,
      },
      {
        path: "management/add-deposit-package",
        element: <AddDepositPackagePage />,
        loader: addDepositPackageLoader,
      },
      {
        path: "management/edit-deposit-package",
        element: <EditDepositPackagePage />,
        loader: editDepositPackageLoader,
      },
      {
        path: "users",
        element: <AdminUsersInfo />,
        loader: adminUsersInfoLoader,
      },
      {
        path: "users/:userId",
        element: <AdminUserInfoPage />,
        loader: adminUserInfoLoader,
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
        path: "messages",
        element: <AdminMessagesPage />,
        loader: adminMessagesLoader,
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
