import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { Flex, Image, Menu, Button } from "antd";
import { HomeOutlined } from "@ant-design/icons";
import BankLogo from "../../Common/Assets/bank_logo.jpg";
import { handleResponseError } from "../../Common/Services/ResponseErrorHandler";
import { showMessageStc } from "../../Common/Services/ResponseErrorHandler";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants";

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
      await axiosInstance.post(`User/Logout`);
      setLoginState(false);
      showMessageStc("Вы успешно вышли из учетной записи", "success");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleLoginLogout = () => {
    if (document.cookie === "login-cookie=user") {
      document.cookie = `login-cookie=${"user"}; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; SameSite=Lax`;
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
          theme={"light"}
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
            <Link to="/cards" style={{ fontSize: "20px" }}>
              Мои карты
            </Link>
          </Menu.Item>
          <Menu.Item key={3}>
            <Link to="/accounts" style={{ fontSize: "20px" }}>
              Мои счета
            </Link>
          </Menu.Item>
          <Menu.Item key={4}>
            <Link to="/credits" style={{ fontSize: "20px" }}>
              Мои кредиты
            </Link>
          </Menu.Item>
          <Menu.Item key={5}>
            <Link to="/deposits" style={{ fontSize: "20px" }}>
              Мои депозиты
            </Link>
          </Menu.Item>
          <Menu.Item key={6}>
            <Link to="/profile" style={{ fontSize: "20px" }}>
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
