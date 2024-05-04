import axios from "axios";
import { useState } from "react";
import {
  Card,
  Flex,
  Typography,
  Button,
  Col,
  Row,
  Select,
  InputNumber,
  Input,
} from "antd";
import { useNavigate, useLoaderData, redirect } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colTextWidth = "200px";
const colInputWidth = "200px";

const getModeratorsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Moderators/GetAllInfo?includeData=${true}`
    );
    return { moderatorsData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorsData: null, error: err.response };
  }
};

export async function loader() {
  const { moderatorsData, error } = await getModeratorsData();
  if (!moderatorsData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }

  const selectModeratorsData = [];
  for (let i = 0; i < moderatorsData.length; i++) {
    selectModeratorsData.push({
      value: moderatorsData[i].id,
      label: moderatorsData[i].nickname,
    });
  }

  return { moderatorsData, selectModeratorsData };
}

export default function EditModeratorPage() {
  const [moderatorId, setModeratorId] = useState(-1);
  const [nickname, setNickname] = useState("");
  const [login, setLogin] = useState("");

  const navigate = useNavigate();

  const { moderatorsData, selectModeratorsData } = useLoaderData();

  const handleNickname = (e) => {
    setNickname(e.target.value);
  };

  const handleLogin = (e) => {
    setLogin(e.target.value);
  };

  const handleCancel = () => {
    navigate("/admin/moderators");
  };

  const editModerator = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      id: moderatorId,
      nickname: nickname.trim(),
      login: "#moderator#" + login.trim(),
    };
    try {
      await axiosInstance.put(`Moderators/UpdateInfo`, data);
      showMessageStc("Учетная запись модератора была изменена", "success");
      navigate("/admin/moderators");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    editModerator();
  };

  const handleSelectModerator = (id) => {
    setModeratorId(id);
    let item = moderatorsData.find((item) => item.id === id);
    setNickname(item.nickname);
    setLogin(item.login.substring(11));
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
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Title level={3}>Редактирование пакета карт</Title>
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
                <Text style={{ fontSize: "16px" }}>Выберите модератора:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  style={{ width: "220px" }}
                  onChange={handleSelectModerator}
                  options={selectModeratorsData}
                />
              </Col>
            </Row>

            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Никнейм:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  onChange={handleNickname}
                  value={nickname}
                  style={{ width: "220px" }}
                  count={{
                    show: true,
                    max: 30,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Логин:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  precision={2}
                  addonBefore={"#moderator#"}
                  style={{ width: "220px" }}
                  onChange={handleLogin}
                  value={login}
                  count={{
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
              Применить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
