import axios from "axios";
import { useState } from "react";
import {
  Card,
  Flex,
  Typography,
  Button,
  Col,
  Row,
  InputNumber,
  Input,
} from "antd";
import { useNavigate } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colTextWidth = "200px";
const colInputWidth = "200px";

export default function AddPackagePage() {
  const [name, setName] = useState();
  const [price, setPrice] = useState();
  const [operationsNumber, setOperationsNumber] = useState();
  const [operationsSum, setOperationsSum] = useState();

  const navigate = useNavigate();

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePrice = (value) => {
    setPrice(value);
  };

  const handleOperationsNumber = (value) => {
    setOperationsNumber(value);
  };

  const handleOperationsSum = (e) => {
    setOperationsSum(e);
  };

  const handleCancel = () => {
    navigate("/admin/management");
  };

  const addPackage = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      id: 0,
      name: name.trim(),
      price: price,
      operationsNumber: operationsNumber,
      operationsSum: operationsSum,
      averageAccountBalance: 0,
      monthPayroll: 0,
    };
    try {
      const res = await axiosInstance.post(`CardPackages/Add`, data);
      showMessageStc("Пакет карт был успешно добавлен", "success");
      navigate("/admin/management");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    addPackage();
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
          <Title level={3}>Добавление пакета карт</Title>
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
                <Text style={{ fontSize: "16px" }}>Название:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  onChange={handleName}
                  value={name}
                  style={{ width: "210px" }}
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
                <Text style={{ fontSize: "16px" }}>Цена:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  precision={2}
                  addonAfter={"BYN"}
                  style={{ width: "170px" }}
                  onChange={handlePrice}
                  value={price}
                  count={{
                    show: true,
                    max: 10,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Количество операций:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  style={{ width: "170px" }}
                  onChange={handleOperationsNumber}
                  value={operationsNumber}
                  count={{
                    show: true,
                    max: 10,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Сумма операций:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  precision={2}
                  addonAfter={"BYN"}
                  style={{ width: "170px" }}
                  onChange={handleOperationsSum}
                  value={operationsSum}
                  count={{
                    show: true,
                    max: 10,
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
