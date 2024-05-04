import { Outlet, useLoaderData, redirect } from "react-router-dom";
import { Layout, Flex } from "antd";
import { useState } from "react";
import "./App.css";
import NavigationBar from "../Components/NavigationBar.jsx";
import { BASE_URL } from "../../Common/Store/constants.js";
import axios from "axios";
import { handleResponseError } from "../../Common/Services/ResponseErrorHandler.js";

const { Header, Footer, Content } = Layout;

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

export default function AdminApp() {
  const { adminData } = useLoaderData();

  const [loginState, setLoginState] = useState(
    document.cookie === "login-cookie=admin"
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
