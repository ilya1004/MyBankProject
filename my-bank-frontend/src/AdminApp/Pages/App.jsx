import { Outlet } from "react-router-dom";
import { Layout } from "antd";

import "./App.css";
import NavigationBar from "../Components/NavigationBar.jsx";

const { Header, Footer, Content } = Layout;

export function App() {
  return (
    <div className="app-main">
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
    </div>
  );
}
