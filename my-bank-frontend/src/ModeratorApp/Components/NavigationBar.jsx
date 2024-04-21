import { Link } from "react-router-dom";
import { Flex, Image, Menu, Button, Typography } from "antd";
import { HomeOutlined } from "@ant-design/icons";

import BankLogo from "../../Common/Assets/bank_logo.jpg";

export default function NavigationBar() {
  const imageSize = "70px";

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
            {/* className="link" */}
            <Link to="profile" style={{ fontSize: "20px" }}>
              Мой профиль
            </Link>
          </Menu.Item>
        </Menu>
        <Link to="/login">
          <Button>Войти</Button>
        </Link>
      </Flex>
    </>
  );
}
