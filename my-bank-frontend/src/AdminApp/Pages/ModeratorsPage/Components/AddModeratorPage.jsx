import axios from "axios";
import { useState } from "react";
import { Card, Flex, Typography, Button, Col, Row, Input } from "antd";
import { useNavigate } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colTextWidth = "200px";
const colInputWidth = "220px";

export default function AddModeratorPage() {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [password2, setPassword2] = useState("");
  const [nickname, setNickname] = useState("");

  const navigate = useNavigate();

  const handleLogin = (e) => {
    setLogin(e.target.value);
  };

  const handlePassword = (e) => {
    setPassword(e.target.value);
  };

  const handlePassword2 = (e) => {
    setPassword2(e.target.value);
  };

  const handleNickname = (e) => {
    setNickname(e.target.value);
  };

  const handleCancel = () => {
    navigate("admin/moderators");
  };

  const addModerator = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      login: "#moderator#" + login.trim(),
      password: password.trim(),
      nickname: nickname.trim(),
    };
    try {
      await axiosInstance.post(`Moderators/Add`, data);
      showMessageStc("Модератор был успешно добавлен", "success");
      navigate("/moderators");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    if (password !== password2) {
      showMessageStc("Пароли не совпадают. Повторите попытку");
    }
    addModerator();
  };

  return (
    <>
      <Flex
        vertical
        align="center"
        justify="flex-start"
        style={{
          minHeight: "90vh",
        }}
      >
        <Flex align="center" style={{ margin: "0px 0px 10px 0px" }}>
          <Title level={3}>Добавление модератора</Title>
        </Flex>
        <Card
          title="Введите данные"
          style={{
            width: "550px",
            // height: "400px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Логин:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  addonBefore={"#moderator#"}
                  onChange={handleLogin}
                  value={login}
                  style={{ width: "220px" }}
                  count={{
                    max: 30,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Пароль:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input.Password
                  precision={2}
                  style={{ width: "220px" }}
                  onChange={handlePassword}
                  value={password}
                  count={{
                    // show: true,
                    max: 30,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Подтвердите пароль:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input.Password
                  style={{ width: "220px" }}
                  onChange={handlePassword2}
                  value={password2}
                  count={{
                    // show: true,
                    max: 30,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Никнейм:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  precision={2}
                  style={{ width: "220px" }}
                  onChange={handleNickname}
                  value={nickname}
                  count={{
                    show: true,
                    max: 30,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
          </Flex>
          <Flex
            gap={20}
            align="center"
            justify="center"
            style={{
              margin: "30px 0px 0px 0px",
            }}
          >
            <Button onClick={handleCancel}>Отмена</Button>
            <Button type="primary" onClick={handleEnter}>
              Добавить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
