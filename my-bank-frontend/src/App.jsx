import MainPage from "./pages/MainPage";
import CardsPage from "./pages/CardsPage";
import PersonalAccountsPage from "./pages/PersonalAccountsPage";
import CreditAccountsPage from "./pages/CreditAccountsPage";
import DepositAccountsPage from "./pages/DepositAccountsPage";
import ProfilePage from "./pages/ProfilePage";
import LoginPage from "./pages/LoginPage/LoginPage";
import BankLogo from "./assets/bank_logo.jpg";
import SignUpPage from "./pages/SignUpPage/SignUpPage";

import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import { Button, Menu, Image, List, Flex, Layout } from "antd";
import { HomeOutlined } from "@ant-design/icons";

import "./utils/App.css";
import { Header } from "antd/es/layout/layout";

const imageSize = "70px";

export default function App() {
  return (
    <div className="App">
      <Router>
        <Flex
          justify="center"
          align="center"
          gap={"large"}
          style={{
            width: "100%",
            // margin: "5px 0px"
          }}
        >
          <Image
            height={imageSize}
            width={imageSize}
            src={BankLogo}
            preview={false}
            style={{
              margin: "0px 0px 0px 0px",
            }}
          />
          <Menu
            mode="horizontal"
            theme={"light"}
            style={{
              width: "60%",
              margin: "0px 20px",
            }}
          >
            <Menu.Item icon={<HomeOutlined />}>
              <Link className="link" to="/">
                Главная
              </Link>
            </Menu.Item>
            <Menu.Item>
              <Link className="link" to="/cards">
                Мои карты
              </Link>
            </Menu.Item>
            <Menu.Item>
              <Link className="link" to="/personal-accounts">
                Мои счета
              </Link>
            </Menu.Item>
            <Menu.Item>
              <Link className="link" to="/credit-accounts">
                Мои депозиты
              </Link>
            </Menu.Item>
            <Menu.Item>
              <Link className="link" to="/deposit-accounts">
                Мои депозиты
              </Link>
            </Menu.Item>
            <Menu.Item>
              <Link className="link" to="/my-profile">
                Мой профиль
              </Link>
            </Menu.Item>
          </Menu>

          <Button>
            <Link to="/login">Войти</Link>
          </Button>
        </Flex>

        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="cards" element={<CardsPage />} />
          <Route path="personal-accounts" element={<PersonalAccountsPage />} />
          <Route path="credit-accounts" element={<CreditAccountsPage />} />
          <Route path="deposit-accounts" element={<DepositAccountsPage />} />
          <Route path="my-profile" element={<ProfilePage />} />
          <Route path="login" element={<LoginPage />} />
          <Route path="register" element={<SignUpPage />} />
          {/* <Route index element={<div>No page is selected.</div> } /> */}
        </Routes>
      </Router>
    </div>
  );
}

{
  /* <div>
          <nav className="nav-header">
            <div>
              <Link to="/">
                <img className="img-logo" src={BankLogo} alt="logo" />
              </Link>
            </div>
            <ul className="nav-list">
              <li>
                <Link className="link" to="/">
                  Main page
                </Link>
              </li>
              <li>
                <Link className="link" to="/cards">
                  My cards
                </Link>
              </li>
              <li>
                <Link className="link" to="/personal-accounts">
                  My accounts
                </Link>
              </li>
              <li>
                <Link className="link" to="/credit-accounts">
                  My credits
                </Link>
              </li>
              <li>
                <Link className="link" to="/deposit-accounts">
                  My deposits
                </Link>
              </li>
            </ul>
            <div className="div-profile">
              <Link className="link-profile" to="my-profile">
                <Button>Мой профиль</Button>
              </Link>
            </div>
            <div className="div-login">
              <Link to="login">
                <Button>Войти</Button>
              </Link>
            </div>
          </nav>
        </div> */
}
