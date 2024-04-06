import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import { Button, Menu, Image, Flex, Layout } from "antd";

import MainPage from "./pages/MainPage/MainPage.jsx";
import CardsPage from "./pages/CardsPage/CardsPage.jsx";
import PersonalAccountsPage from "./pages/PersonalAccountsPage/PersonalAccountsPage.jsx";
import CreditAccountsPage from "./pages/CreditAccountsPage/CreditAccountsPage.jsx";
import DepositAccountsPage from "./pages/DepositAccountsPage/DepositAccountsPage.jsx";
import ProfilePage from "./pages/ProfilePage/ProfilePage.jsx";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import SignUpPage from "./pages/SignUpPage/SignUpPage.jsx";
import { PrivateRoute } from "./components/PrivateRoute/PrivateRoute.jsx";

import {
  AppContextProvider,
  useAppContext,
} from "./store/Contexts/AppContextProvider.jsx";

import "./utils/App.css";
import NavigationBar from "./components/NavigationBar/NavigationBar.jsx";

const { Header, Footer, Content } = Layout;

export default function App() {
  return (
    <div className="app-main">
      <AppContextProvider>
        <Router>
          <Layout>
            <Header>
              <NavigationBar />
            </Header>
            <Content>
              <Routes>
                <Route index path="/*" element={<MainPage />} />

                {/* <Route element={<PrivateRoute />}> */}
                <Route path="cards/*" element={<CardsPage />} />
                <Route
                  path="personal-accounts/*"
                  element={<PersonalAccountsPage />}
                />
                <Route
                  path="credit-accounts/*"
                  element={<CreditAccountsPage />}
                />
                <Route
                  path="deposit-accounts/*"
                  element={<DepositAccountsPage />}
                />
                <Route path="my-profile/*" element={<ProfilePage />} />
                {/* </Route> */}

                <Route path="login/*" element={<LoginPage />} />
                <Route path="register/*" element={<SignUpPage />} />
                {/* <Route index element={<div>No page is selected.</div> } /> */}
              </Routes>
            </Content>
            <Footer>Footer</Footer>
          </Layout>
        </Router>
      </AppContextProvider>
    </div>
  );
}
