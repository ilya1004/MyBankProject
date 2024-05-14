import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { Flex, Image, Menu, Button, Typography } from "antd";
import { HomeOutlined, DownOutlined } from "@ant-design/icons";
import BankLogo from "../../Common/Assets/bank_logo.jpg";
import { handleResponseError } from "../../Common/Services/ResponseErrorHandler";
import { showMessageStc } from "../../Common/Services/ResponseErrorHandler";
import axios from "axios";
import { BASE_URL } from "../../Common/Store/constants";

const { Text } = Typography;
const { SubMenu, Item } = Menu;

export default function NavigationBar({ loginState, setLoginState }) {
  const [buttonText, setButtonText] = useState("Выйти");
  const navigate = useNavigate();
  const imageSize = "70px";
  const padd = "0px 12px";

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
          <Menu.Item key={1} icon={<HomeOutlined />} style={{ padding: padd }}>
            <Link to="/" style={{ fontSize: "20px" }}>
              Главная
            </Link>
          </Menu.Item>
          <Menu.Item key={2} style={{ padding: padd }}>
            <Link to="/cards" style={{ fontSize: "20px" }}>
              Мои карты
            </Link>
          </Menu.Item>
          <Menu.Item key={3} style={{ padding: padd }}>
            <Link to="/accounts" style={{ fontSize: "20px" }}>
              Мои счета
            </Link>
          </Menu.Item>
          <Menu.Item key={4} style={{ padding: padd }}>
            <Link to="/credits" style={{ fontSize: "20px" }}>
              Мои кредиты
            </Link>
          </Menu.Item>
          <Menu.Item key={5} style={{ padding: padd }}>
            <Link to="/deposits" style={{ fontSize: "20px" }}>
              Мои депозиты
            </Link>
          </Menu.Item>
          <Menu.Item key={7} style={{ padding: padd }}>
            <Link to="/profile" style={{ fontSize: "20px" }}>
              Мой профиль
            </Link>
          </Menu.Item>
          <SubMenu
            key={8}
            style={{ padding: padd }}
            title={
              <Flex justify="center" align="center" style={{ height: "60px" }}>
                <Text
                  style={{
                    fontSize: "20px",
                    padding: "1px 0px 0px 0px",
                  }}
                >
                  Другое
                </Text>
                <DownOutlined style={{margin: "6px 0px 0px 7px"}}/>
              </Flex>
            }
          >
            <Menu.Item key={9} style={{ padding: padd }}>
              <Link to="/make-transaction" style={{ fontSize: "20px" }}>
                Перевести средства
              </Link>
            </Menu.Item>
            <Menu.Item key={10} style={{ padding: padd }}>
              <Link to="/messages" style={{ fontSize: "20px" }}>
                Мои сообщения
              </Link>
            </Menu.Item>
          </SubMenu>
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
