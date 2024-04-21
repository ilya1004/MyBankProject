import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Input,
  Select,
  Popover,
} from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";
import { useState } from "react";
import { useLoaderData, Link, useNavigate } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;

const getCurrenciesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`Currency/GetAllInfo`);
    console.log(res.data["list"]);
    return res.data["list"];
  } catch (err) {
    handleResponseError(err.response);
  }
};

export async function loader() {
  const currenciesRaw = await getCurrenciesData();
  const currenciesData = [];
  for (let i = 0; i < currenciesRaw.length; i++) {
    currenciesData.push({
      value: currenciesRaw[i].id,
      label: currenciesRaw[i].code,
    });
  }
  return { currenciesData };
}

export default function AddAccountPage() {
  const [name, setName] = useState();
  const [currencyId, setCurrencyId] = useState();

  const { currenciesData } = useLoaderData();

  const { navigate } = useNavigate();

  const addAccount = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      name: name,
      currencyId: currencyId,
    };
    try {
      const res = await axiosInstance.post(`PersonalAccounts/Add`, data);
      console.log(res.data["id"]);
      showMessageStc("Счет был успешно создан", "success");
      navigate("/accounts");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleCardName = (e) => {
    setName(e.target.value);
  };

  const handleCurrencyId = (e) => {
    setCurrencyId(e);
  };

  const handleCancel = () => {
    navigate("/accounts");
  };

  const handleEnter = () => {
    addAccount();
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
          <Button
            style={{ margin: "18px 0px 0px 20px" }}
            onClick={() => navigate(-1)}
          >
            Назад
          </Button>
          <Title level={3}>Открытие счета</Title>
        </Flex>
        <Card
          title="Введите данные"
          style={{
            width: "460px",
            height: "270px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: "150px" }}>
                <Text style={{ fontSize: "16px" }}>Название:</Text>
              </Col>
              <Col style={{ width: "200px" }}>
                <Input
                  onChange={handleCardName}
                  value={name}
                  style={{ width: "210px" }}
                  count={{
                    show: true,
                    max: 20,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: "150px" }}>
                <Text style={{ fontSize: "16px" }}>Валюта счета:</Text>
              </Col>
              <Col style={{ width: "200px" }}>
                <Select
                  defaultValue=""
                  style={{ width: "120px" }}
                  onChange={handleCurrencyId}
                  options={currenciesData}
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
              Создать
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
