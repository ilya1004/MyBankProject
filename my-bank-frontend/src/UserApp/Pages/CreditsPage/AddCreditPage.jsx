import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Input,
  Select,
  Tag,
} from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { useState } from "react";
import { useLoaderData, Link, useNavigate, redirect } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colLabelWidth = "370px";
const colTextWidth = "200px";

const getCreditPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditPackages/GetAllInfo?includeData=${true}`
    );
    return { packagesData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { packagesData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { packagesData: null, error: err.response };
  }
};

export async function loader() {
  const { packagesData, error } = await getCreditPackagesData();
  if (!packagesData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  let selectPackagesData = [];
  for (let i = 0; i < packagesData.length; i++) {
    selectPackagesData.push({
      value: packagesData[i].id,
      label: packagesData[i].name,
    });
  }
  return { packagesData, selectPackagesData };
}

export default function AddCreditPage() {
  const [packageId, setPackageId] = useState(-1);
  const [name, setName] = useState("");
  const [grantedAmount, setGrantedAmount] = useState("");
  const [interestRate, setInterestRate] = useState("");
  const [interestCalculationType, setInterestCalculationType] = useState("");
  const [termInDays, setTermInDays] = useState("");
  const [hasPrepaymentOption, setHasPrepaymentOption] = useState("");
  const [currencyCode, setCurrencyCode] = useState("");

  const { packagesData, selectPackagesData } = useLoaderData();

  const navigate = useNavigate(-1);

  const handleSelectPackage = (value) => {
    setPackageId(value);
    let item = packagesData.find((item) => item.id === value);
    setName(item.name);
    setGrantedAmount(item.creditGrantedAmount);
    setInterestRate(item.interestRate);
    setInterestCalculationType(item.interestCalculationType);
    setTermInDays(item.creditTermInDays);
    setHasPrepaymentOption(item.hasPrepaymentOption);
    setCurrencyCode(item.currency.code);
  };

  const convertMonths = (days) => {
    let months = Math.floor(days / 30);
    if (months < 24 || months % 12 !== 0) {
      if (months % 10 === 1) {
        return `${months} месяц`;
      } else if (2 <= months % 10 && months % 10 <= 4 && months < 5) {
        return `${months} месяца`;
      } else {
        return `${months} месяцев`;
      }
    } else {
      let years = months / 12;
      if (years % 10 === 1) {
        return `${years} год`;
      } else if (2 <= years % 10 && years % 10 <= 4) {
        return `${years} годa`;
      } else {
        return `${years} лет`;
      }
    }
  };

  const addCreditRequest = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      name: `Мой ${name}`,
      creditPackageId: packageId,
      userId: 0,
    };
    try {
      const res = await axiosInstance.post(`CreditRequests/Add`, data);
      showMessageStc(
        "Запрос на выдачу кредита был успешно отправлен",
        "success"
      );
      navigate(-1);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    addCreditRequest();
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <>
      <Flex
        vertical
        align="center"
        justify="flex-start"
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Link to="/credits">
            <Button style={{ margin: "18px 0px 0px 20px" }}>Назад</Button>
          </Link>
          <Title level={3}>Оформление кредита</Title>
        </Flex>
        <Card
          title="Выберите кредит"
          style={{
            width: "700px",
            // height: "400px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text style={{ fontSize: "16px" }}>Пакет кредита:</Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Select
                  defaultValue=""
                  style={{ width: "200px" }}
                  onChange={handleSelectPackage}
                  options={selectPackagesData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Название кредита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{packageId !== -1 ? `Мой ${name}` : null}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Размер кредита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{`${grantedAmount} ${currencyCode}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Процентная ставка:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{packageId !== -1 ? `${interestRate} %` : null}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Тип выплаты процентов:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                {packageId !== -1 ? (
                  interestCalculationType === "annuity" ? (
                    <Tag color="green">Аннуитетный</Tag>
                  ) : (
                    <Tag color="green">Дифференцированный</Tag>
                  )
                ) : null}
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Срок выдачи кредита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>
                  {packageId !== -1 ? convertMonths(termInDays) : null}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Возможность досрочной выплаты кредита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                {packageId !== -1 ? (
                  hasPrepaymentOption === true ? (
                    <CheckCircleTwoTone
                      twoToneColor="#52c41a"
                      style={{ fontSize: "18px" }}
                    />
                  ) : (
                    <CloseCircleTwoTone
                      twoToneColor="red"
                      style={{ fontSize: "18px" }}
                    />
                  )
                ) : null}
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
              Отправить запрос
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
