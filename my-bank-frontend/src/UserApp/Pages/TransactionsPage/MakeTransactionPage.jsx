import axios from "axios";
import {
  Flex,
  Typography,
  Button,
  Table,
  Card,
  Col,
  Row,
  Input,
  Select,
  InputNumber,
} from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";
import { useState } from "react";

const { Title, Text } = Typography;

const getAccountsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `PersonalAccounts/GetAllInfoByCurrentUser?includeData=${true}`
    );
    return { accountsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { accountsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { accountsData: null, error: err.response };
  }
};

export async function loader() {
  const { accountsData, error } = await getAccountsData();
  if (!accountsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  let selectAccountsData = [];
  for (let i = 0; i < accountsData.length; i++) {
    selectAccountsData.push({
      value: accountsData[i].id,
      label: accountsData[i].name,
    });
  }
  return { accountsData, selectAccountsData };
}

const infoLabelWidth = "270px";
const infoValueWidth = "240px";

export default function MakeTransactionPage() {
  const [personalAccountId, setPersonalAccountId] = useState(-1);
  const [transactionAmount, setTransactionAmount] = useState();
  const [transactionType, setTransactionType] = useState("");
  const [accountId, setAccountId] = useState(-1);
  const [accountName, setAccountName] = useState("");
  const [cardNumber, setCardNumber] = useState(null);
  const [accountNumber, setAccountNumber] = useState(null);
  const [userNickname, setUserNickname] = useState(null);

  const { accountsData, selectAccountsData } = useLoaderData();

  const navigate = useNavigate();

  const makeTransaction = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    let info = "";
    if (transactionType === "my-account" || transactionType === "account") {
      info = "Перевод средств по номеру счета";
    } else if (transactionType === "card") {
      info = "Перевод средств по номеру карты";
    } else {
      info = "Перевод средств по никнейму пользователя";
    }
    const data = {
      paymentAmount: transactionAmount,
      currencyId: accountsData.find((item) => item.id === personalAccountId)
        .currency.id,
      information: info,
      transactionType: transactionType,
      accountSenderNumber: accountsData.find(
        (item) => item.id === personalAccountId
      ).number,
      userSenderNickname: accountsData.find(
        (item) => item.id === personalAccountId
      ).user.nickname,
      cardRecipientNumber: cardNumber,
      accountRecipientNumber:
        transactionType === "my-account"
          ? accountsData.find((item) => item.id === accountId).number
          : accountNumber,
      userRecipientNickname: userNickname !== null ? userNickname.trim() : null,
    };
    try {
      const res = await axiosInstance.post(`Transactions/Add`, data);
      if (res.data.status === true) {
        showMessageStc("Перевод был произведен успешно ", "success");
      } else {
        showMessageStc("Произошла неизвестная ошибка", "error");
      }
      navigate(-1);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    makeTransaction();
  };

  const handleCancel = () => {
    navigate(-1);
  };

  const isNumber = (value) => {
    return /^\d+$/.test(value);
  };

  const handleCardNumber = (e) => {
    if (!isNumber(e.target.value.at(-1)) && e.target.value.length !== 0) {
      return;
    }
    setCardNumber(e.target.value);
  };

  const isValidAccNumber = (value) => {
    for (let i = 0; i < value.length; i++) {
      if (/(\d|[A-Z])/.test(value[i]) === false) {
        return false;
      }
    }
    return true;
  };

  const handleAccountNumber = (e) => {
    if (!isValidAccNumber(e.target.value) && e.target.value.length !== 0) {
      return;
    }
    setAccountNumber(e.target.value);
  };

  const handleUserNickname = (e) => {
    setUserNickname(e.target.value);
  };

  const handleTransactionAmount = (value) => {
    if (personalAccountId !== -1) {
      let balanceValue = accountsData.find(
        (item) => item.id === personalAccountId
      ).currentBalance;
      if (value > balanceValue) {
        return;
      }
    }
    setTransactionAmount(value);
  };

  const handleTransactionType = (value) => {
    setTransactionType(value);
    setAccountId(-1);
    setAccountName("");
    setCardNumber(null);
    setAccountNumber(null);
    setUserNickname(null);
  };

  const handleAccountId = (id) => {
    if (id === personalAccountId) {
      showMessageStc("Выберите другой счет для перевода", "error");
      setAccountName("");
      return;
    }
    setAccountId(id);
    setAccountName(accountsData.find((item) => item.id === id).name);
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
          <Button
            style={{ margin: "18px 0px 0px 20px" }}
            onClick={() => navigate(-1)}
          >
            Назад
          </Button>
          <Title level={3}>Перевод средств</Title>
        </Flex>
        <Card
          title="Введите данные"
          style={{
            width: "610px",
            // height: "400px",
          }}
        >
          <Flex vertical gap={10}>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Выберите счет для снятия средств:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Select
                  defaultValue=""
                  style={{ minWidth: "170px" }}
                  onChange={(id) => setPersonalAccountId(id)}
                  options={selectAccountsData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Остаток на счету:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text strong style={{ margin: "0px 0px 0px 10px" }}>
                  {personalAccountId !== -1
                    ? `${
                        accountsData.find(
                          (item) => item.id === personalAccountId
                        ).currentBalance
                      } ${
                        accountsData.find(
                          (item) => item.id === personalAccountId
                        ).currency.code
                      }`
                    : ""}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Введите сумму для перевода:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <InputNumber
                  style={{ width: "170px" }}
                  addonAfter={
                    personalAccountId !== -1
                      ? accountsData.find(
                          (item) => item.id === personalAccountId
                        ).currency.code
                      : null
                  }
                  min={1}
                  defaultValue={1}
                  value={transactionAmount}
                  onChange={handleTransactionAmount}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Выберите вид перевода:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Select
                  defaultValue=""
                  style={{ width: "220px" }}
                  onChange={handleTransactionType}
                  options={[
                    { value: "my-account", label: "На другой мой счет" },
                    { value: "card", label: "По номеру карты" },
                    { value: "account", label: "По номеру счета" },
                    { value: "nickname", label: "По никнейму пользователя" },
                  ]}
                />
              </Col>
            </Row>
            {transactionType === "my-account" ? (
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Выберите ваш счет:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Select
                    defaultValue=""
                    value={accountName}
                    style={{ minWidth: "170px" }}
                    onChange={handleAccountId}
                    options={selectAccountsData}
                  />
                </Col>
              </Row>
            ) : null}
            {transactionType === "card" ? (
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Введите номер карты:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Input
                    count={{
                      show: true,
                      max: 16,
                      exceedFormatter: (txt, { max }) => txt.slice(0, max),
                    }}
                    onChange={handleCardNumber}
                    value={cardNumber}
                    style={{ width: "200px" }}
                  />
                </Col>
              </Row>
            ) : null}
            {transactionType === "account" ? (
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Введите номер счета:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Input
                    count={{
                      show: true,
                      max: 28,
                      exceedFormatter: (txt, { max }) => txt.slice(0, max),
                    }}
                    onChange={handleAccountNumber}
                    value={accountNumber}
                    style={{ width: "290px" }}
                  />
                </Col>
              </Row>
            ) : null}
            {transactionType === "nickname" ? (
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Введите никнейм пользователя:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Input
                    count={{
                      max: 30,
                      exceedFormatter: (txt, { max }) => txt.slice(0, max),
                    }}
                    onChange={handleUserNickname}
                    value={userNickname}
                    style={{ width: "200px" }}
                  />
                </Col>
              </Row>
            ) : null}
          </Flex>
          <Flex
            gap={20}
            align="center"
            justify="center"
            style={{
              margin: "20px 0px 0px 0px",
            }}
          >
            <Button onClick={() => navigate(-1)}>Отмена</Button>
            <Button type="primary" onClick={handleEnter}>
              Перевести
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
