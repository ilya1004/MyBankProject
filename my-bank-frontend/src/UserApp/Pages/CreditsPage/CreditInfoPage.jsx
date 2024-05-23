import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Tag,
  Table,
  Input,
  Select,
} from "antd";
import { useState } from "react";
import {
  useLoaderData,
  useRevalidator,
  useNavigate,
  redirect,
} from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;
const { Column } = Table;

const getCreditData = async (accountId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditAccounts/GetInfoByCurrentUser?creditAccountId=${accountId}&includeData=${true}`
    );
    return { creditData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { creditData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { creditData: null, error: err.response };
  }
};

const getNextPaymentData = async (accountId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditAccounts/GetNextPayment?creditAccountId=${accountId}`
    );
    return { paymentData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { paymentData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { paymentData: null, error: err.response };
  }
};

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

export async function loader({ params }) {
  const { creditData, error } = await getCreditData(params.accountId);
  if (!creditData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  const { paymentData, error: error1 } = await getNextPaymentData(
    params.accountId
  );
  if (!paymentData) {
    if (error1.status === 401 || error1.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error1.status,
      });
    }
  }
  const { accountsData, error: error2 } = await getAccountsData();
  if (!accountsData) {
    if (error2.status === 401 || error2.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error2.status,
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
  return { creditData, paymentData, accountsData, selectAccountsData };
}

const infoLabelWidth = "220px";
const infoValueWidth = "280px";

export default function CreditInfoPage() {
  const [isSettingsOpened, setIsSettingsOpened] = useState(false);
  const [accName, setAccName] = useState();
  const [isChangingName, setIsChangingName] = useState(false);
  const [isMakingPayment, setIsMakingPayment] = useState(false);
  const [isMakingPrepayment, setIsMakingPrepayment] = useState(false);
  const [personalAccountId, setPersonalAccountId] = useState(-1);

  const revalidator = useRevalidator();
  const navigate = useNavigate();

  const { creditData, paymentData, accountsData, selectAccountsData } =
    useLoaderData();

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const setAccNewName = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `CreditAccounts/UpdateName?creditAccountId=${creditData.id}&name=${accName}`
      );
      if (res.data.status === true) {
        showMessageStc("Название кредита было успешно изменено", "success");
        revalidator.revalidate();
      } else {
        showMessageStc(res.data.message, "error");
      }
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const makePayment = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    let persnNum = accountsData.find(
      (item) => item.id === personalAccountId
    ).number;
    const data = {
      id: 0,
      paymentAmount: paymentData.paymentAmount,
      paymentNumber: paymentData.paymentNumber,
      creditAccountId: creditData.id,
      creditAccountNumber: creditData.number,
      personalAccountId: personalAccountId,
      personalAccountNumber: persnNum,
      userNickname: creditData.user.nickname,
    };
    try {
      const res = await axiosInstance.post(`CreditPayments/Add`, data);
      if (res.status === 200) {
        const res = await axiosInstance.get(
          `CreditAccounts/GetInfoByCurrentUser?creditAccountId=${
            creditData.id
          }&includeData=${true}`
        );
        let credit = res.data.item;
        if (credit.currentBalance === 0 && credit.isActive === false) {
          showMessageStc("Кредит был успешно выплачен", "success");
          navigate("/credits", { replace: true });
        } else {
          showMessageStc(
            "Выплата по кредиту была успешно осуществлена",
            "success"
          );
        }
        revalidator.revalidate()
      } else {
        showMessageStc(res.data.message, "error");
      }
      setPersonalAccountId(-1);
      setIsMakingPayment(false);
      setIsSettingsOpened(false);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    let txt = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
    return <Text>{txt}</Text>;
  };

  const convertOperStatus = (operStatus) => {
    if (operStatus === true) {
      return <Tag color={"green"}>Завершено успешно</Tag>;
    } else {
      return <Tag color={"red"}>Ошибка операции</Tag>;
    }
  };

  const handleChangeName = () => {
    isChangingName === false
      ? setIsChangingName(true)
      : setIsChangingName(false);
  };

  const handleNewAccNameEdit = (e) => {
    setAccName(e.target.value);
  };

  const handleCancelName = () => {
    setIsChangingName(false);
  };

  const handleEnterName = () => {
    setAccNewName();
    setIsChangingName(false);
  };

  const handleMakePrepayment = () => {
    isMakingPrepayment === false
      ? setIsMakingPrepayment(true)
      : setIsMakingPrepayment(false);
  };

  const convertDate = (date) => {
    return new Date(date).toLocaleDateString();
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

  const handleAddPayment = () => {
    let currDate = new Date();
    let paymentDate = new Date(paymentData.datetime);
    if (paymentDate - currDate >= 0) {
      let diffDate = new Date(paymentDate - currDate);
      let diffInMonths = diffDate.getUTCMonth();
      if (diffInMonths >= 1) {
        showMessageStc(
          "Вы уже делали выплату по кредиту в этом месяце",
          "info"
        );
        return;
      }
    }
    isMakingPayment === true
      ? setIsMakingPayment(false)
      : setIsMakingPayment(true);
  };

  const handlePersonalAccount = (e) => {
    setPersonalAccountId(e);
  };

  const handleCancelPayment = () => {
    setIsMakingPayment(false);
  };

  const handleEnterPayment = () => {
    if (personalAccountId === -1) {
      showMessageStc("Вы не выбрали лицевой счет для снятия средств", "error");
      return;
    }
    if (
      accountsData.find((item) => item.id === personalAccountId)
        .currentBalance >= paymentData.paymentAmount
    ) {
      makePayment();
    } else {
      showMessageStc(
        "На выбранном лицевом счете недостаточно средств",
        "error"
      );
    }
  };

  const handleOpenSettings = () => {
    isSettingsOpened === false
      ? setIsSettingsOpened(true)
      : setIsSettingsOpened(false);
  };

  const handleCloseSettings = () => {
    setIsSettingsOpened(false);
  };

  const makePrepayment = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `CreditAccounts/MakePrepayment?creditAccountId=${creditData.id}&personalAccountId=${personalAccountId}`
      );
      if (res.data.status === true) {
        showMessageStc("Кредит был успешно досрочно выплачен", "success");
        navigate("/credits", { replace: true });
      } else {
        showMessageStc(res.data.message, "error");
      }
      setPersonalAccountId(-1);
      setIsMakingPrepayment(false);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleCancelPrepayment = () => {
    setIsMakingPrepayment(false);
  };

  const handleEnterPrepayment = () => {
    if (personalAccountId === -1) {
      showMessageStc("Вы не выбрали лицевой счет для снятия средств", "error");
      return;
    }
    if (
      accountsData.find((item) => item.id === personalAccountId)
        .currentBalance >= creditData.currentBalance
    ) {
      makePrepayment();
    } else {
      showMessageStc(
        "На выбранном лицевом счете недостаточно средств",
        "error"
      );
    }
  };

  return (
    <Flex
      align="center"
      justify="flex-start"
      style={{
        margin: "0px 15px",
        minHeight: "80vh",
        height: "fit-content",
      }}
      vertical
    >
      <Flex align="center" gap={30} style={{ margin: "0px 0px 20px 0px" }}>
        <Button
          style={{ margin: "18px 0px 0px 20px" }}
          onClick={() => navigate(-1)}
        >
          Назад
        </Button>
        <Title level={3}>Детальная информация по кредиту</Title>
      </Flex>
      <Flex
        align="flex-start"
        justify="center"
        gap={40}
        style={{ width: "90%" }}
      >
        <Flex justify="center" align="flex-start">
          <Flex align="center" justify="flex-start" gap={20} vertical>
            <Card
              style={{
                // height: "180px",
                width: "540px",
              }}
            >
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Название:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{creditData.name}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Номер кредитного счета:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  {convertAccNumber(creditData.number)}
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    ФИО владельца:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>
                    {creditData.user.surname +
                      " " +
                      creditData.user.name +
                      " " +
                      creditData.user.patronymic}
                  </Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Дата взятия кредита:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertDate(creditData.creationDate)}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Срок выдачи кредита:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertMonths(creditData.creditTermInDays)}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Текущий остаток:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text
                    strong
                  >{`${creditData.currentBalance} ${creditData.currency.code}`}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Дата следующей выплаты:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertDate(paymentData.datetime)}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Досрочное погашение:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  {creditData.hasPrepaymentOption === true ? (
                    <Tag color="green">Да</Tag>
                  ) : (
                    <Tag color="red">Нет</Tag>
                  )}
                </Col>
              </Row>
            </Card>
            <Flex justify="center">
              <Button onClick={handleOpenSettings}>Управление кредитом</Button>
            </Flex>
          </Flex>
        </Flex>

        <Flex
          justify="center"
          align="center"
          style={{
            width: "60%",
          }}
        >
          {isSettingsOpened === true ? (
            <Flex vertical gap={20}>
              <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                <Flex justify="center" gap={50}>
                  <Flex align="center" gap={30} vertical>
                    <Button onClick={handleChangeName}>Изменить имя</Button>
                    <Button onClick={handleCloseSettings}>Закрыть</Button>
                  </Flex>
                  <Flex align="center" gap={30} vertical>
                    <Button onClick={handleAddPayment} type="primary">
                      Сделать выплату
                    </Button>
                    <Button
                      type="primary"
                      disabled={!creditData.hasPrepaymentOption}
                      onClick={handleMakePrepayment}
                    >
                      Погасить досрочно
                    </Button>
                  </Flex>
                </Flex>
              </Card>
              {isMakingPrepayment === true ? (
                <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                  <Flex
                    vertical
                    align="center"
                    justify="center"
                    gap={25}
                    style={{ width: "100%" }}
                  >
                    <Flex vertical align="center" justify="center" gap={20}>
                      <Select
                        defaultValue=""
                        style={{ minWidth: "170px" }}
                        onChange={handlePersonalAccount}
                        options={selectAccountsData}
                      />
                      <Flex gap={5}>
                        <Text>Остаток на счету:</Text>
                        <Text strong>
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
                      </Flex>
                      <Flex gap={10}>
                        <Text style={{ fontSize: "16px" }}>
                          Размер выплаты:
                        </Text>
                        <Input
                          disabled={true}
                          defaultValue={creditData.currentBalance}
                          style={{
                            width: "120px",
                            height: "28px",
                          }}
                        />
                      </Flex>
                    </Flex>

                    <Flex gap={20} align="center" justify="center">
                      <Button onClick={handleCancelPrepayment}>Отмена</Button>
                      <Button type="primary" onClick={handleEnterPrepayment}>
                        Выплатить досрочно
                      </Button>
                    </Flex>
                  </Flex>
                </Card>
              ) : null}
              {isChangingName === true ? (
                <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                  <Flex
                    vertical
                    align="center"
                    justify="center"
                    gap={25}
                    style={{ width: "100%" }}
                  >
                    <Flex align="center" justify="center" gap={20}>
                      <Text style={{ width: "185px", fontSize: "16px" }}>
                        Новое название счета:
                      </Text>
                      <Input
                        maxLength={30}
                        style={{ width: "180px" }}
                        onChange={handleNewAccNameEdit}
                      />
                    </Flex>
                    <Flex gap={20} align="center" justify="center">
                      <Button onClick={handleCancelName}>Отмена</Button>
                      <Button type="primary" onClick={handleEnterName}>
                        Применить
                      </Button>
                    </Flex>
                  </Flex>
                </Card>
              ) : null}
              {isMakingPayment === true ? (
                <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                  <Flex
                    vertical
                    align="center"
                    justify="center"
                    gap={25}
                    style={{ width: "100%" }}
                  >
                    <Flex vertical align="center" justify="center" gap={20}>
                      <Select
                        defaultValue=""
                        style={{ minWidth: "170px" }}
                        onChange={handlePersonalAccount}
                        options={selectAccountsData}
                      />
                      <Flex gap={5}>
                        <Text>Остаток на счету:</Text>
                        <Text strong>
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
                      </Flex>
                      <Flex gap={10}>
                        <Text style={{ fontSize: "16px" }}>
                          Размер выплаты:
                        </Text>
                        <Input
                          disabled={true}
                          defaultValue={paymentData.paymentAmount.toFixed(2)}
                          style={{
                            width: "120px",
                            height: "28px",
                          }}
                        />
                      </Flex>
                    </Flex>

                    <Flex gap={20} align="center" justify="center">
                      <Button onClick={handleCancelPayment}>Отмена</Button>
                      <Button type="primary" onClick={handleEnterPayment}>
                        Сделать выплату
                      </Button>
                    </Flex>
                  </Flex>
                </Card>
              ) : null}
            </Flex>
          ) : (
            <Table
              dataSource={creditData.payments}
              pagination={{ pageSize: 7 }}
              title={() => (
                <Text
                  strong
                  style={{ fontSize: "16px", margin: "0px 0px 0px 10px" }}
                >
                  Выплаты по кредиту
                </Text>
              )}
            >
              <Column
                width="90px"
                title="Номер"
                dataIndex="id"
                key="id"
                render={(id) => <Text>{"000" + id}</Text>}
              />
              <Column
                width="180px"
                title="Размер выплаты"
                dataIndex="paymentAmount"
                key="paymentAmount"
                render={(amount) => (
                  <Text>{amount + " " + creditData.currency.code}</Text>
                )}
              />
              <Column
                width="170px"
                title="Номер выплаты"
                dataIndex="paymentNumber"
                key="paymentNumber"
                render={(value) => (
                  <Text>{`${value}/${creditData.totalPaymentsNumber}`}</Text>
                )}
              />
              <Column
                width="180px"
                title="Время"
                dataIndex="datetime"
                key="datetime"
                render={(datetime) => convertDatetime(datetime)}
              />
              <Column
                title="Статус выплаты"
                dataIndex="status"
                key="status"
                render={(operStatus) => convertOperStatus(operStatus)}
              />
            </Table>
          )}
        </Flex>
      </Flex>
    </Flex>
  );
}
