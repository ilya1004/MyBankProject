import { Outlet } from "react-router-dom";
import { Layout, Flex, Typography } from "antd";
// import "./App.css";
import NavigationBar from "../Components/NavigationBar.jsx";
import { useState } from "react";

const { Header, Footer, Content } = Layout;
const { Text } = Typography;

const backColor = "#F1F0EA";

export default function UserApp() {
  const [loginState, setLoginState] = useState(
    document.cookie === "login-cookie=user"
  );

  return (
    <>
      <Layout>
        <Header
          style={{
            padding: "10px 0px 0px 0px",
            height: "fit-content",
            backgroundColor: backColor,
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
            backgroundColor: backColor,
          }}
        >
          <Outlet context={[loginState, setLoginState]} />
        </Content>
        <Footer
          style={{
            backgroundColor: "#F5DEBE",
          }}
        >
          <Flex justify="center">
            <Text>MyBank. 2024</Text>
          </Flex>
        </Footer>
      </Layout>
    </>
  );
}
