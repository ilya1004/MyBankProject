import { Outlet } from "react-router-dom";
import { Layout } from "antd";

import { AppContextProvider } from "./store/Contexts/AppContextProvider.jsx";

import "./utils/App.css";
import NavigationBar from "./components/NavigationBar/NavigationBar.jsx";

const { Header, Footer, Content } = Layout;

export default function App() {
  return (
    <div className="app-main">
      <AppContextProvider>
        <Layout>
          <Header
            style={{
              backgroundColor: "whitesmoke",
            }}
          >
            <NavigationBar />
          </Header>
          <Content
            style={{
              padding: "10px 0px 0px 0px",
              backgroundColor: "#F1F0E8",
            }}
          >
            <Outlet />
          </Content>
          <Footer
            style={{
              backgroundColor: "wheat",
            }}
          >
            Footer
          </Footer>
        </Layout>
      </AppContextProvider>
    </div>
  );
}

{
  /* <Routes>
  <Route index path="/*" element={<MainPage />} />

                <Route element={<PrivateRoute />}>
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
                </Route>

                <Route path="login/*" element={<LoginPage />} />
                <Route path="register/*" element={<SignUpPage />} />
              </Routes> */
}
