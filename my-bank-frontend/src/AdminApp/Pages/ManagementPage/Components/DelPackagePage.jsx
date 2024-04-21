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

const getPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`CardPackages/GetAllInfo`);
    return { packagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { packagesData: null, error: err.response };
  }
};

export async function loader() {
  const { packagesData, error } = await getPackagesData();
  if (!packagesData) {
    throw new Response("", {
      status: error.status,
    });
  }

  const selectPackagesData = [];
  for (let i = 0; i < packagesData.length; i++) {
    selectPackagesData.push({
      value: packagesData[i].id,
      label: packagesData[i].name,
    });
  }

  return { packagesData, selectPackagesData };
}

const infoLabelWidth = "200px";
const infoValueWidth = "200px";

export default function DelPackagePage() {
  const [packageItem, setPackageItem] = useState(null);

  const { packagesData, selectPackagesData } = useLoaderData();

  const navigate = useNavigate();

  const deletePackage = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `CardPackages/UpdateStatus?cardPackageId=${packageItem.id}
        &isActive=${false}`
      );
      showMessageStc("Пакет карт был успешно удален", "success");
      navigate("/admin/management");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    deletePackage();
  };

  const handleCancel = () => {
    navigate("/admin/management");
  };

  const handleSelectPackage = (id) => {
    setPackageItem(packagesData.find((item) => item.id === id));
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
                <Text style={{ fontSize: "16px" }}>Выберите пакет:</Text>
              </Col>
              <Col style={{ width: "200px" }}>
                <Select
                  style={{ width: "200px" }}
                  onChange={handleSelectPackage}
                  options={selectPackagesData}
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
                <Text>{packageItem !== null ? packageItem.name : null}</Text>
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
                  {packageItem !== null ? `${packageItem.price} BYN` : null}
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
                  {packageItem !== null ? packageItem.operationsNumber : null}
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
                  {packageItem !== null ? packageItem.operationsSum : null}
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
