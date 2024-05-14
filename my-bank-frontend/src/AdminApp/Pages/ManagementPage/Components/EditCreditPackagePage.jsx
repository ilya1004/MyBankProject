import axios from "axios";
import { useState } from "react";
import {
  Select,
  Card,
  Flex,
  Typography,
  Button,
  Col,
  Row,
  InputNumber,
  Input,
  Checkbox,
} from "antd";
import { useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colTextWidth = "260px";
const colInputWidth = "200px";

const getPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditPackages/GetAllInfo?includeData=${true}&onlyActive=${false}`
    );
    return { packagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { packagesData: null, error: err.response };
  }
};

const getCurrenciesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`Currency/GetAllInfo`);
    return { currenciesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { currenciesData: null, error: err.response };
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

  const { currenciesData, error: error1 } = await getCurrenciesData();
  if (!currenciesData) {
    if (error1.status === 401 || error1.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error1.status,
      });
    }
  }
  let selectCurrenciesData = [];
  for (let i = 0; i < currenciesData.length; i++) {
    selectCurrenciesData.push({
      value: currenciesData[i].id,
      label: `${currenciesData[i].code}`,
    });
  }
  return {
    currenciesData,
    selectCurrenciesData,
    packagesData,
    selectPackagesData,
  };
}

export default function EditCreditPackagePage() {
  const [packageId, setPackageId] = useState(-1);
  const [name, setName] = useState();
  const [creditGrantedAmount, setCreditGrantedAmount] = useState();
  const [interestRate, setInterestRate] = useState();
  const [interestCalculationType, setInterestCalculationType] = useState(null);
  const [creditTermInDays, setCreditTermInDays] = useState();
  const [hasPrepaymentOption, setHasPrepaymentOption] = useState(false);
  const [currencyId, setCurrencyId] = useState(-1);

  const navigate = useNavigate();

  const {
    currenciesData,
    selectCurrenciesData,
    packagesData,
    selectPackagesData,
  } = useLoaderData();

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handleCancel = () => {
    navigate("/admin/management");
  };

  const editPackage = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      id: packageId,
      name: name,
      creditStartBalance: creditGrantedAmount,
      creditGrantedAmount: creditGrantedAmount,
      interestRate: interestRate,
      interestCalculationType: interestCalculationType,
      creditTermInDays: creditTermInDays,
      hasPrepaymentOption: hasPrepaymentOption,
      currencyId: currencyId,
    };
    try {
      const res = await axiosInstance.put(`CreditPackages/UpdateInfo`, data);
      showMessageStc("Пакет кредитов был успешно изменен", "success");
      navigate("/admin/management");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    editPackage();
  };

  const handleSelectPackage = (id) => {
    setPackageId(id);
    let item = packagesData.find((item) => item.id === id);
    setName(item.name);
    setCreditGrantedAmount(item.creditGrantedAmount);
    setInterestRate(item.interestRate);
    setInterestCalculationType(item.interestCalculationType);
    setCreditTermInDays(item.creditTermInDays);
    setHasPrepaymentOption(item.hasPrepaymentOption);
    setCurrencyId(item.currencyId);
  };

  return (
    <>
      <Flex
        vertical
        align="center"
        justify="flex-start"
        style={{
          minHeight: "80vh",
          height: "fit-content",
        }}
      >
        <Flex align="center" style={{ margin: "0px 0px 10px 0px" }}>
          <Title level={3}>Редактирование пакета кредитов</Title>
        </Flex>
        <Card
          title="Введите данные"
          style={{
            width: "580px",
            margin: "0px 0px 20px 0px"
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
                <Text style={{ fontSize: "16px" }}>Валюта:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  value={
                    currencyId !== -1
                      ? currenciesData.find((item) => item.id === currencyId)
                          .code
                      : ""
                  }
                  style={{ width: "100px" }}
                  onChange={(value) => setCurrencyId(value)}
                  options={selectCurrenciesData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Размер кредита:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  min={0}
                  precision={2}
                  addonAfter={
                    currencyId !== -1
                      ? currenciesData.find((item) => item.id == currencyId)
                          .code
                      : null
                  }
                  style={{ width: "170px" }}
                  onChange={(value) => setCreditGrantedAmount(value)}
                  value={creditGrantedAmount}
                  count={{
                    max: 10,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Процентная ставка:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  precision={1}
                  min={-100}
                  max={100}
                  maxLength={5}
                  addonAfter={"%"}
                  style={{ width: "130px" }}
                  onChange={(value) => setInterestRate(value)}
                  value={interestRate}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>
                  Тип подсчета процентов:
                </Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  value={
                    interestCalculationType === "annuity"
                      ? "Аннуитетный"
                      : "Дифференцированный"
                  }
                  style={{ width: "200px" }}
                  onChange={(value) => setInterestCalculationType(value)}
                  options={[
                    {
                      value: "annuity",
                      label: "Аннуитетный",
                    },
                    {
                      value: "differential",
                      label: "Дифференцированный",
                    },
                  ]}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>
                  Срок кредита (в месяцах):
                </Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <InputNumber
                  min={0}
                  max={600}
                  maxLength={5}
                  style={{ width: "130px" }}
                  onChange={(value) => setCreditTermInDays(value * 30)}
                  value={creditTermInDays / 30}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Досрочного погашение:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Checkbox
                  onChange={(e) => setHasPrepaymentOption(e.target.checked)}
                  checked={hasPrepaymentOption}
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
