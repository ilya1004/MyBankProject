import { useContext } from "react";
import { Link } from "react-router-dom";
import { Flex, Image, Menu, Button } from "antd";
import { HomeOutlined } from "@ant-design/icons";

import useAuth from "../../hooks/useAuth";
import BankLogo from "../../assets/bank_logo.jpg";
import {
  AppContextProvider,
  useAppContext,
} from "../../store/Contexts/AppContextProvider";

export default function NavigationBar() {
  const { isAuthenticated, setAuth, role, setRole } = useAuth();

  // const { test, setTest } = useAppContext();

  // console.log(test);

  const imageSize = "70px";

  const handleExit = () => {
    setAuth(false);
    setRole("");
  };

  return (
    <Flex
      justify="center"
      align="center"
      gap={"large"}
      style={{
        width: "100%",
        // margin: "5px 0px"
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
          <Link className="link" to="/">
            Главная
          </Link>
        </Menu.Item>
        <Menu.Item key={2}>
          <Link className="link" to="/cards">
            Мои карты
          </Link>
        </Menu.Item>
        <Menu.Item key={3}>
          <Link className="link" to="/accounts">
            Мои счета
          </Link>
        </Menu.Item>
        <Menu.Item key={4}>
          <Link className="link" to="/credits">
            Мои кредиты
          </Link>
        </Menu.Item>
        <Menu.Item key={5}>
          <Link className="link" to="/deposits">
            Мои депозиты
          </Link>
        </Menu.Item>
        <Menu.Item key={6}>
          <Link className="link" to="/profile">
            Мой профиль
          </Link>
        </Menu.Item>
      </Menu>

      {isAuthenticated === true && role === "user" ? (
        <Button onClick={handleExit}>Выход</Button>
      ) : (
        <Link to="/login">
          <Button>Войти</Button>
        </Link>
      )}
    </Flex>
  );
}
