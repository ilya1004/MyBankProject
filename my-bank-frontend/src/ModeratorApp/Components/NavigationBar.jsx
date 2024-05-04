import { Link, useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants";
import { showMessageStc, handleResponseError } from "../../Common/Services/ResponseErrorHandler";
import { Flex, Image, Menu, Button } from "antd";
import { HomeOutlined } from "@ant-design/icons";

import BankLogo from "../../Common/Assets/bank_logo.jpg";

export default function NavigationBar({ loginState, setLoginState }) {
  const [buttonText, setButtonText] = useState("Выйти");
  const navigate = useNavigate();
  const imageSize = "70px";

  useEffect(() => {
    if (loginState === true) {
      setButtonText("Выйти");
    } else {
      setButtonText("Войти");
    }
  }, [loginState]);

  const logoutUser = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.post(`Moderator/Logout`);
      setLoginState(false);
      showMessageStc("Вы успешно вышли из учетной записи", "success");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleLoginLogout = () => {
    if (document.cookie === "login-cookie=moderator") {
      document.cookie = `login-cookie=${"moderator"}; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; SameSite=Lax`;
      logoutUser();
      setButtonText("Войти");
    }
    navigate("/login");
  };

  return (
    <>
      <Flex
        justify="center"
        align="flex-start"
        gap={30}
        style={{
          width: "100%",
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
          style={{
            width: "65%",
            margin: "0px 20px",
          }}
        >
          <Menu.Item key={1} icon={<HomeOutlined />}>
            <Link to="/" style={{ fontSize: "20px" }}>
              Главная
            </Link>
          </Menu.Item>
          <Menu.Item key={2}>
            <Link to="credit-requests" style={{ fontSize: "20px" }}>
              Выдача кредитов
            </Link>
          </Menu.Item>
          <Menu.Item key={3}>
            <Link to="users-info" style={{ fontSize: "20px" }}>
              Пользователи
            </Link>
          </Menu.Item>
          <Menu.Item key={4}>
            <Link to="profile" style={{ fontSize: "20px" }}>
              Мой профиль
            </Link>
          </Menu.Item>
        </Menu>
        <Flex
          align="center"
          justify="center"
          style={{
            height: "65px",
            width: "80px",
          }}
        >
          <Button onClick={handleLoginLogout}>{buttonText}</Button>
        </Flex>
      </Flex>
    </>
  );
}
