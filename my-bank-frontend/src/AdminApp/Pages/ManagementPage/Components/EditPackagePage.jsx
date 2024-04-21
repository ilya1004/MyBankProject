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
import { useNavigate, useLoaderData } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colTextWidth = "200px";
const colInputWidth = "200px";

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

export default function EditPackagePage() {
  const [packageId, setPackageId] = useState(-1);
  const [name, setName] = useState();
  const [price, setPrice] = useState();
  const [operationsNumber, setOperationsNumber] = useState();
  const [operationsSum, setOperationsSum] = useState();

  const navigate = useNavigate();

  const { packagesData, selectPackagesData } = useLoaderData();

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
      id: packageId,
      name: name.trim(),
      price: price,
      operationsNumber: operationsNumber,
      operationsSum: operationsSum,
      averageAccountBalance: 0,
      monthPayroll: 0,
    };
    try {
      await axiosInstance.put(`CardPackages/UpdateInfo`, data);
      showMessageStc("Пакет карт был успешно изменен", "success");
      navigate("/admin/management");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    addPackage();
  };

  const handleSelectPackage = (id) => {
    setPackageId(id);
    let item = packagesData.find((item) => item.id === id);
    setName(item.name);
    setPrice(item.price);
    setOperationsNumber(item.operationsNumber);
    setOperationsSum(item.operationsSum);
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
          {/* <Button style={{ margin: "18px 0px 0px 20px" }}>Назад</Button> */}
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
                <Text style={{ fontSize: "16px" }}>Выберите пакет:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  style={{ width: "200px" }}
                  onChange={handleSelectPackage}
                  options={selectPackagesData}
                />
              </Col>
            </Row>

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
              Применить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
