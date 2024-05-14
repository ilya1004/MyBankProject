import { Link, useNavigate } from "react-router-dom";
import { Flex, Image, Menu, Button, Typography } from "antd";
import { HomeOutlined } from "@ant-design/icons";
import { useState, useEffect } from "react";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants";
import {
  showMessageStc,
  handleResponseError,
} from "../../Common/Services/ResponseErrorHandler";

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

  const logoutAdmin = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.post(`Admin/Logout`);
      setLoginState(false);
      showMessageStc("Вы успешно вышли из учетной записи", "success");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleLoginLogout = () => {
    if (document.cookie === "login-cookie=admin") {
      document.cookie = `login-cookie=${"admin"}; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; SameSite=Lax`;
      logoutAdmin();
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
            margin: "0px",
            borderRadius: "10px",
          }}
        />
        <Menu
          mode="horizontal"
          theme="light"
          style={{
            width: "fit-content",
            margin: "0px 15px",
            padding: "0px 10px",
            borderRadius: "30px",
          }}
        >
          <Menu.Item key={1}>
            <Link to="management" style={{ fontSize: "20px" }}>
              Управление
            </Link>
          </Menu.Item>
          <Menu.Item key={2}>
            <Link to="users" style={{ fontSize: "20px" }}>
              Пользователи
            </Link>
          </Menu.Item>
          <Menu.Item key={3}>
            <Link to="moderators" style={{ fontSize: "20px" }}>
              Модераторы
            </Link>
          </Menu.Item>
          <Menu.Item key={4}>
            <Link to="messages" style={{ fontSize: "20px" }}>
              Мои сообщения
            </Link>
          </Menu.Item>
          <Menu.Item key={5}>
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
            width: "70px",
          }}
        >
          <Button onClick={handleLoginLogout}>{buttonText}</Button>
        </Flex>
      </Flex>
    </>
  );
}
