import { Outlet, redirect, useLoaderData } from "react-router-dom";
import { Layout, Flex, Typography } from "antd";
// import "./App.css";
import NavigationBar from "../Components/NavigationBar.jsx";
import React, { useState } from "react";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants.js";
import { handleResponseError } from "../../Common/Services/ResponseErrorHandler.js";

const { Header, Footer, Content } = Layout;
const { Text } = Typography;

// const getModeratorData = async () => {
//   const axiosInstance = axios.create({
//     baseURL: BASE_URL,
//     withCredentials: true,
//   });
//   try {
//     const res = await axiosInstance.get(
//       `Moderators/GetInfoCurrent?includeData=${false}`
//     );
//     return { moderatorData: res.data.item, error: null };
//   } catch (err) {
//     if (err.response.status === 401 || err.response.status) {
//       return { moderatorData: null, error: err.response };
//     }
//     handleResponseError(err.response);
//     return { moderatorData: null, error: err.response };
//   }
// };

// export async function loader() {
//   const { moderatorData, error } = await getModeratorData();
//   if (!moderatorData) {
//     if (error.status === 401 || error.status === 403) {
//       return redirect("/login");
//     } else {
//       throw new Response("", {
//         status: error.status,
//       });
//     }
//   }
//   return { moderatorData };
// }

const backColor = "#F1F0EA";

export default function ModeratorApp() {
  const [loginState, setLoginState] = useState(
    document.cookie === "login-cookie=moderator"
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
            <Text>MyBank. 2024. Модератор системы.</Text>
          </Flex>
        </Footer>
      </Layout>
    </>
  );
}
