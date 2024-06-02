import { Outlet, redirect } from "react-router-dom";
import { Layout, Flex, Typography } from "antd";
import { useState } from "react";
// import "./App.css";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants.js";
import NavigationBar from "../Components/NavigationBar.jsx";
import { handleResponseError } from "../../Common/Services/ResponseErrorHandler.js";
import { formToJSON } from "axios";

const { Header, Footer, Content } = Layout;
const { Text } = Typography;

const getAdminData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Admin/GetInfoCurrent?includeData=${false}`
    );
    return { adminData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401 || err.response.status) {
      return { adminData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { adminData: null, error: err.response };
  }
};

export async function loader() {
  const { adminData, error } = await getAdminData();
  if (!adminData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { adminData };
}

const backColor = "#F1F0EA";

export default function AdminApp() {
  const [loginState, setLoginState] = useState(
    document.cookie === "login-cookie=admin"
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
            <Text>MyBank. 2024. Администратор системы.</Text>
          </Flex>
        </Footer>
      </Layout>
    </>
  );
}
