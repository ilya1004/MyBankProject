import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Tag,
  Table,
  Modal,
  Input,
  Select,
} from "antd";
import { useState } from "react";
import {
  useLoaderData,
  Link,
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
import dayjs from "dayjs";

const { Title, Text, Paragraph } = Typography;
const { Column } = Table;

const getDepositData = async (accountId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `DepositAccounts/GetInfoByCurrentUser?depositAccountId=${accountId}&includeData=${true}`
    );
    return { depositData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { depositData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { depositData: null, error: err.response };
  }
};

// const getNextPaymentData = async (accountId) => {
//   const axiosInstance = axios.create({
//     baseURL: BASE_URL,
//     withCredentials: true,
//   });
//   try {
//     const res = await axiosInstance.get(
//       `CreditAccounts/GetNextPayment?creditAccountId=${accountId}`
//     );
//     return { paymentData: res.data.item, error: null };
//   } catch (err) {
//     if (err.response.status === 401) {
//       return { paymentData: null, error: err.response };
//     }
//     handleResponseError(err.response);
//     return { paymentData: null, error: err.response };
//   }
// };

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
  const { depositData, error } = await getDepositData(params.accountId);
  console.log(depositData);
  if (!depositData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
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
  return { depositData, accountsData, selectAccountsData };
}

const infoLabelWidth = "220px";
const infoValueWidth = "280px";

export default function DepositInfoPage() {
  const [isOpenModalRevokeDeposit, setIsOpenModalRevokeDeposit] =
    useState(false);
  const [accName, setAccName] = useState();
  const [isChangingName, setIsChangingName] = useState(false);
  const [isRevokingDeposit, setIsRevokingDeposit] = useState(false);
  const [isWithdrawingInterests, setIsWithdrawingInterests] = useState(false);
  const [personalAccountId, setPersonalAccountId] = useState(-1);
  const [isSettingsOpened, setIsSettingsOpened] = useState(false);

  const revalidator = useRevalidator();
  const navigate = useNavigate();

  const { depositData, accountsData, selectAccountsData } = useLoaderData();

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  // const closeAcc = async () => {
  //   const axiosInstance = axios.create({
  //     baseURL: BASE_URL,
  //     withCredentials: true,
  //   });
  //   try {
  //     const res = await axiosInstance.put(
  //       `PersonalAccounts/CloseAccount?personalAccountId=${creditData.id}`
  //     );
  //     console.log(res.data["status"]);
  //     if (res.data.status == true) {
  //       showMessageStc("Счет был успешно закрыт", "success");
  //       navigate("/accounts");
  //     } else {
  //       showMessageStc(res.data.message, "success");
  //     }
  //   } catch (err) {
  //     handleResponseError(err.response);
  //   }
  // };

  const setAccNewName = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `DepositAccounts/UpdateName?depositAccountId=${depositData.id}&name=${accName}`
      );
      if (res.data.status == true) {
        showMessageStc("Название депозита было успешно изменено", "success");
        revalidator.revalidate();
      } else {
        showMessageStc(res.data.message, "error");
      }
    } catch (err) {
      handleResponseError(err.response);
    }
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
    setIsSettingsOpened(false);
  };

  // const handleOkModalCloseAcc = () => {
  // setOpenModalCloseAcc(false);
  // closeAcc();
  // };

  // const handleCancelModalCloseAcc = () => {
  //   setOpenModalCloseAcc(false);
  // };

  //   const handleCloseAccount = () => {
  //     if (creditData.currentBalance !== 0) {
  //       setModalText("На счете ненулевой баланс");
  //       setIsAccountClosable(false);
  //     } else {
  //       setModalText("Вы действительно хотите закрыть этот счет?");
  //       setIsAccountClosable(true);
  //     }
  //     setOpenModalCloseAcc(true);
  //   };

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

  const handlePersonalAccount = (id) => {
    setPersonalAccountId(id);
  };

  const printClosingDate = () => {
    let crDate = new Date(depositData.creationDate);
    crDate.setDate(crDate.getDate() + depositData.depositTermInDays);
    return crDate.toLocaleDateString();
  };

  const handleOpenSettings = () => {
    isSettingsOpened === false
      ? setIsSettingsOpened(true)
      : setIsSettingsOpened(false);
  };

  const handleCloseSettings = () => {
    setIsSettingsOpened(false);
  };

  const handleRevokeDeposit = () => {
    if (depositData.isRevocable === false) {
      showMessageStc("Вы не можете отозвать этот депозит", "error");
      return;
    }
    isRevokingDeposit === false
      ? setIsRevokingDeposit(true)
      : setIsRevokingDeposit(false);
  };

  const withdrawInterests = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `DepositAccounts/WithdrawInterests?depositAccountId=${depositData.id}&personalAccountId=${personalAccountId}`
      );
      if (res.data.status == true) {
        showMessageStc(
          "Проценты с депозитного счета были успешно сняты",
          "success"
        );
        revalidator.revalidate();
      } else {
        showMessageStc(res.data.message, "error");
      }
      setPersonalAccountId(-1);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleWithdrawInterests = () => {
    if (
      Math.abs(depositData.currentBalance - depositData.depositStartBalance) >
      0.0001
    ) {
      isWithdrawingInterests === false
        ? setIsWithdrawingInterests(true)
        : setIsWithdrawingInterests(false);
    } else {
      showMessageStc("У вас нет процентов для снятия", "info");
    }
  };

  const handleCancelWithdrawalInterests = () => {
    setIsWithdrawingInterests(false);
    setPersonalAccountId(-1);
  };

  const handleEnterWithdrawalInterests = () => {
    withdrawInterests();
    setIsWithdrawingInterests(false);
    setIsSettingsOpened(false);
    setPersonalAccountId(-1);
  };

  const revokeDeposit = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `DepositAccounts/Revoke?depositAccountId=${depositData.id}&personalAccountId=${personalAccountId}`
      );
      if (res.data.status == true) {
        showMessageStc(
          "Проценты с депозитного счета были успешно сняты",
          "success"
        );
        revalidator.revalidate();
      } else {
        showMessageStc(res.data.message, "error");
      }
      setPersonalAccountId(-1);
      setIsOpenModalRevokeDeposit(false);
      setIsRevokingDeposit(false);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleOpenModalRevokeDeposit = () => {
    if (personalAccountId === -1) {
      showMessageStc(
        "Вы не выбрали счет для перевода средств после отзыва депозита",
        "error"
      );
      return;
    }
    setIsOpenModalRevokeDeposit(true);
  };

  const handleCancelRevokeDeposit = () => {
    setIsOpenModalRevokeDeposit(false);
    setIsRevokingDeposit(false);
  };

  const handleOkModalRevokeDeposit = () => {
    if (personalAccountId === -1) {
      showMessageStc(
        "Вы не выбрали счет для перевода средств после отзыва депозита",
        "error"
      );
      return;
    }
    revokeDeposit();
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
        <Title level={3}>Детальная информация по депозиту</Title>
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
                  <Text>{depositData.name}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Номер депозитного счета:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  {convertAccNumber(depositData.number)}
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
                    {depositData.user.surname +
                      " " +
                      depositData.user.name +
                      " " +
                      depositData.user.patronymic}
                  </Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Дата открытия депозита:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertDate(depositData.creationDate)}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Срок возврата депозита:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertMonths(depositData.depositTermInDays)}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Дата возврата депозита:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{printClosingDate()}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Начальный баланс:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{`${depositData.depositStartBalance} ${depositData.currency.code}`}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Текущий баланс:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text
                    strong
                  >{`${depositData.currentBalance} ${depositData.currency.code}`}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "15px" }}>
                    Другие опции:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Flex gap={10} vertical>
                    {depositData.isRevocable === true ? (
                      <Tag color="green" style={{ width: "fit-content" }}>
                        Отзывной
                      </Tag>
                    ) : (
                      <Tag color="orange" style={{ width: "fit-content" }}>
                        Безотзывной
                      </Tag>
                    )}
                    {depositData.hasCapitalisation === true ? (
                      <Tag color="green" style={{ width: "fit-content" }}>
                        С капитализацией
                      </Tag>
                    ) : (
                      <Tag color="orange" style={{ width: "fit-content" }}>
                        Без капитализации
                      </Tag>
                    )}
                    {depositData.hasInterestWithdrawalOption === true ? (
                      <Tag color="green" style={{ width: "fit-content" }}>
                        Возможность снятия процентов
                      </Tag>
                    ) : (
                      <Tag color="orange" style={{ width: "fit-content" }}>
                        Без возможности снятия процентов
                      </Tag>
                    )}
                  </Flex>
                </Col>
              </Row>
            </Card>
            <Flex justify="center">
              <Button onClick={handleOpenSettings}>Управление депозитом</Button>
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
                    <Button type="primary" onClick={handleWithdrawInterests}>
                      Снять проценты
                    </Button>
                    <Button type="primary" onClick={handleRevokeDeposit}>
                      Отозвать депозит
                    </Button>
                  </Flex>
                </Flex>
              </Card>
              {isWithdrawingInterests === true ? (
                <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                  <Flex
                    vertical
                    align="center"
                    justify="center"
                    gap={25}
                    style={{ width: "100%" }}
                  >
                    <Flex vertical align="center" justify="center" gap={20}>
                      <Flex gap={10}>
                        <Text style={{ fontSize: "16px" }}>
                          Счет для снятия процентов:
                        </Text>
                        <Select
                          defaultValue=""
                          style={{ minWidth: "170px" }}
                          onChange={handlePersonalAccount}
                          options={selectAccountsData}
                        />
                      </Flex>
                      <Flex gap={5}>
                        <Text style={{ fontSize: "16px" }}>
                          Сумма для снятия:
                        </Text>
                        <Text strong style={{ fontSize: "16px" }}>
                          {`${
                            depositData.currentBalance -
                            depositData.depositStartBalance
                          } ${depositData.currency.code}`}
                        </Text>
                      </Flex>
                    </Flex>

                    <Flex gap={20} align="center" justify="center">
                      <Button onClick={handleCancelWithdrawalInterests}>
                        Отмена
                      </Button>
                      <Button
                        type="primary"
                        onClick={handleEnterWithdrawalInterests}
                      >
                        Снять проценты
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
              {isRevokingDeposit === true ? (
                <Card style={{ width: "450px", margin: "0px 0px 0px 0px" }}>
                  <Flex
                    vertical
                    align="center"
                    justify="center"
                    gap={25}
                    style={{ width: "100%" }}
                  >
                    <Flex vertical align="center" justify="center" gap={20}>
                      <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                        <Col style={{ width: "200px" }}>
                          <Text type="secondary" style={{ fontSize: "14px" }}>
                            Счет для перевода средств:
                          </Text>
                        </Col>
                        <Col style={{ width: "170px" }}>
                          <Select
                            defaultValue=""
                            style={{ width: "160px" }}
                            onChange={handlePersonalAccount}
                            options={selectAccountsData}
                          />
                        </Col>
                      </Row>
                      <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                        <Col style={{ width: "200px" }}>
                          <Text type="secondary" style={{ fontSize: "14px" }}>
                            Сумма к снятию:
                          </Text>
                        </Col>
                        <Col style={{ width: "170px" }}>
                          <Text
                            strong
                            style={{
                              fontSize: "16px",
                              margin: "0px 0px 0px 5px",
                            }}
                          >
                            {`${depositData.currentBalance} ${depositData.currency.code}`}
                          </Text>
                        </Col>
                      </Row>
                    </Flex>

                    <Flex gap={20} align="center" justify="center">
                      <Button onClick={handleCancelRevokeDeposit}>
                        Отмена
                      </Button>
                      <Button
                        type="primary"
                        onClick={handleOpenModalRevokeDeposit}
                      >
                        Отозвать депозит
                      </Button>
                    </Flex>
                  </Flex>
                </Card>
              ) : null}
              <Modal
                title={"Отзыв депозита и закрытие счета"}
                open={isOpenModalRevokeDeposit}
                onOk={handleOkModalRevokeDeposit}
                onCancel={handleCancelRevokeDeposit}
                okButtonProps={{
                  type: "primary",
                }}
                okText="Подтвердить"
                cancelText="Отмена"
              >
                <Text style={{ fontSize: "16px" }}>
                  {
                    "Вы уверены, что хотите отозвать депозит и перевести средства на выбранный счет?"
                  }
                </Text>
              </Modal>
            </Flex>
          ) : (
            <Table
              dataSource={depositData.accruals}
              title={() => (
                <Text
                  strong
                  style={{ fontSize: "16px", margin: "0px 0px 0px 10px" }}
                >
                  Начисления по депозиту
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
                title="Размер начисления"
                dataIndex="accrualAmount"
                key="accrualAmount"
                render={(amount) => (
                  <Text>{amount + " " + depositData.currency.code}</Text>
                )}
              />
              <Column
                width="180px"
                title="Номер начисления"
                dataIndex="accrualNumber"
                key="accrualNumber"
                render={(value) => (
                  <Text>{`${value}/${depositData.totalAccrualsNumber}`}</Text>
                )}
              />
              <Column
                width="120px"
                title="Дата"
                dataIndex="datetime"
                key="datetime"
                render={(datetime) => convertDate(datetime)}
              />
              <Column
                // width="150px"
                title="Статус начисления"
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
