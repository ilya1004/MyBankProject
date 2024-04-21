import axios from "axios";
import { useState } from "react";
import { Card, Flex, Typography, Button, Col, Row, Select } from "antd";
import { useNavigate, useLoaderData } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const getModeratorsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`Moderators/GetAllInfo?includeData=${false}`);
    return { moderatorsData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorsData: null, error: err.response };
  }
};

export async function loader() {
  const { moderatorsData, error } = await getModeratorsData();
  if (!moderatorsData) {
    throw new Response("", {
      status: error.status,
    });
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

const infoLabelWidth = "200px";
const infoValueWidth = "200px";


export default function DelModeratorPage() {
	const [moderator, setModerator] = useState(null);

  const { moderatorsData, selectModeratorsData } = useLoaderData();

  const navigate = useNavigate();

  const deleteModerator = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `CardPackages/UpdateStatus?cardPackageId=${moderator.id}
        &isActive=${false}`	
      );
      showMessageStc("Модератор был успешно удален", "success");
      navigate("/admin/management");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    deleteModerator();
  };

  const handleCancel = () => {
    navigate("/admin/moderators");
  };

  const handleSelectModerator = (id) => {
    setModerator(moderatorsData.find((item) => item.id === id));
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
          <Title level={3}>Удаление пакета карт</Title>
        </Flex>
        <Card
          style={{
            width: "550px",
            // height: "400px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: "200px" }}>
                <Text style={{ fontSize: "16px" }}>Выберите модератора:</Text>
              </Col>
              <Col style={{ width: "200px" }}>
                <Select
                  style={{ width: "200px" }}
                  onChange={handleSelectModerator}
                  options={selectModeratorsData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Название:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>{moderator !== null ? moderator.name : null}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Цена:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>
                  {moderator !== null ? `${moderator.price} BYN` : null}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Количество операций:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>
                  {moderator !== null ? moderator.operationsNumber : null}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Сумма операций:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>
                  {moderator !== null ? moderator.operationsSum : null}
                </Text>
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
            <Button type="primary" onClick={handleEnter} danger>
              Удалить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );

}