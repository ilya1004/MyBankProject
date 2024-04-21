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
          <Menu.Item key={1}>
            <Link to="/admin/management" style={{ fontSize: "20px" }}>
              Управление
            </Link>
          </Menu.Item>
          <Menu.Item key={2}>
            <Link to="/admin/users-info" style={{ fontSize: "20px" }}>
              Пользователи
            </Link>
          </Menu.Item>
          <Menu.Item key={3}>
            <Link to="/admin/moderators" style={{ fontSize: "20px" }}>
              Модераторы
            </Link>
          </Menu.Item>
          <Menu.Item key={4}>
            <Link to="/admin/profile" style={{ fontSize: "20px" }}>
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
