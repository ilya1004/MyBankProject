import { Outlet } from "react-router-dom";
import { Layout } from "antd";

import "./App.css";
import NavigationBar from "../Components/NavigationBar.jsx";
import { useState } from "react";

const { Header, Footer, Content } = Layout;

export default function App() {
  const [loginState, setLoginState] = useState(
    document.cookie === "login-cookie=user"
  );

  return (
    <div className="app-main">
      <Layout>
        <Header
          style={{
            backgroundColor: "whitesmoke",
          }}
        >
          <NavigationBar
            loginState={loginState}
            setLoginState={setLoginState}
          />
        </Header>
        <Content
          style={{
            padding: "10px 0px 0px 0px",
            backgroundColor: "#F1F0E8",
          }}
        >
          <Outlet context={[loginState, setLoginState]} />
        </Content>
        <Footer
          style={{
            backgroundColor: "wheat",
          }}
        >
          Footer
        </Footer>
      </Layout>
    </div>
  );
}
