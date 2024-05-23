import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Tag,
  Table,
  DatePicker,
  Modal,
  Input,
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
import { mkConfig, generateCsv, download } from "export-to-csv";

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;
const { Column } = Table;

const getAccData = async (accountId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `PersonalAccounts/GetInfoByCurrentUser?personalAccountId=${accountId}&includeData=${true}`
    );
    return { accData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { accData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { accData: null, error: err.response };
  }
};

export async function loader({ params }) {
  const { accData, error } = await getAccData(params.accountId);
  if (!accData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { accData };
}

const infoLabelWidth = "220px";
const infoValueWidth = "280px";

export default function AccountInfoPage() {
  const [dateStart, setDateStart] = useState(null);
  const [dateEnd, setDateEnd] = useState(null);
  const [transData, setTransData] = useState();
  const [openModalCloseAcc, setOpenModalCloseAcc] = useState(false);
  const [accName, setAccName] = useState();
  const [isAccountClosable, setIsAccountClosable] = useState(false);
  const [modalText, setModalText] = useState("");
  const [isChangingName, setIsChangingName] = useState(false);
  const [isSettingsOpened, setIsSettingsOpened] = useState(false);

  const revalidator = useRevalidator();
  const navigate = useNavigate();

  const { accData } = useLoaderData();

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const getTransactionsData = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      let dateS = dateStart.toJSON();
      let dateE = dateEnd.toJSON();
      console.log(dateS);
      console.log(dateE);
      console.log(accData.number);
      const res = await axiosInstance.get(
        `Transactions/GetAllInfoByPersonalAccountNumber?accountNumber=${accData.number}&dateStart=${dateS}&dateEnd=${dateE}`
      );
      console.log(res.data.list);
      return res.data.list;
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const closeAcc = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `PersonalAccounts/CloseAccount?personalAccountId=${accData.id}`
      );
      console.log(res.data["status"]);
      if (res.data.status == true) {
        showMessageStc("Счет был успешно закрыт", "success");
        navigate("/accounts");
      } else {
        showMessageStc(res.data.message, "success");
      }
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const setAccNewName = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `PersonalAccounts/UpdateName?personalAccountId=${accData.id}&name=${accName}`
      );
      console.log(res.data["status"]);
      if (res.data.status == true) {
        showMessageStc("Название счета было успешно изменено", "success");
        revalidator.revalidate();
      } else {
        showMessageStc(res.data.message, "success");
      }
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const setAccountState = async (state) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `PersonalAccounts/UpdateNicknameTransfersState?personalAccountId=${accData.id}&state=${state}`
      );
      if (state === false) {
        showMessageStc("Счет был сделан неосновным", "success");
      } else {
        showMessageStc("Счет был сделан основным", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleShowTrans = async () => {
    if (dateEnd === null && dateStart === null) {
      showMessageStc("Вы не выбрали период для отображения выписки", "info");
      setTransData([]);
    } else {
      setTransData(await getTransactionsData());
    }
  };

  const filterType = (value, record) => {
    let res = false;
    if (
      value === "Отправка средств" &&
      record.accountSenderNumber === accData.number
    ) {
      res = true;
    } else if (
      value === "Получение средств" &&
      record.accountRecipientNumber === accData.number
    ) {
      res = true;
    }
    console.log(res);
    return res;
  };

  const convertOperationType = (accRecNumber) => {
    let txt = "";
    if (accRecNumber === accData.number) {
      txt = "Получение средств";
    } else {
      txt = "Отправка средств";
    }
    return <Text>{txt}</Text>;
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

  const handleEnterName = () => {
    setAccNewName();
    setIsChangingName(false);
  };

  const handleOkModalCloseAcc = () => {
    setOpenModalCloseAcc(false);
    closeAcc();
  };

  const handleCancelModalCloseAcc = () => {
    setOpenModalCloseAcc(false);
  };

  const handleDateRangeChange = (dates) => {
    if (dates == null) {
      setDateStart(null);
      setDateEnd(null);
    } else {
      setDateStart(dates[0]);
      setDateEnd(dates[1]);
    }
  };

  const handleCloseAccount = () => {
    if (accData.currentBalance !== 0) {
      setModalText("На счете ненулевой баланс");
      setIsAccountClosable(false);
    } else if (accData.cards.length !== 0) {
      setModalText("К счету привязаны какие-либо карты");
      setIsAccountClosable(false);
    } else {
      setModalText("Вы действительно хотите закрыть этот счет?");
      setIsAccountClosable(true);
    }
    setOpenModalCloseAcc(true);
  };

  const convertAccDate = (date) => {
    return new Date(date).toLocaleDateString();
  };

  const convertCardNumber = (number) => {
    let numStr = number.toString();
    return `${numStr.substring(0, 1)}XXX XXXX XXXX ${numStr.substring(12, 16)}`;
  };

  const csvConfig = mkConfig({
    useKeysAsHeaders: true,
    filename: "transaction",
  });

  const exportToCSV = async () => {
    if (dateEnd === null && dateStart === null) {
      showMessageStc("Вы не выбрали период для скачивания выписки", "info");
      setTransData([]);
    } else {
      const data = await getTransactionsData();
      const csv = generateCsv(csvConfig)(data);
      download(csvConfig)(csv);
    }
  };

  const handleChangeState = () => {
    if (accData.isForTransfersByNickname) {
      setAccountState(false);
    } else {
      setAccountState(true);
    }
  };

  const handleCancelSett = () => {
    setIsSettingsOpened(false);
    setIsChangingName(false);
  };

  const handleOpenSettings = () => {
    isSettingsOpened === false
      ? setIsSettingsOpened(true)
      : setIsSettingsOpened(false);
    setIsChangingName(false);
  };

  const handleMakeTransaction = () => {
    navigate("/make-transaction");
  };

  return (
    <Flex
      justify="flex-start"
      align="center"
      style={{
        margin: "0px 15px 20px 15px",
        height: "fit-content",
        width: "99%",
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
        <Title level={3}>Детальная информация по счету</Title>
      </Flex>
      <Flex align="flex-start" justify="center" gap={70}>
        <Flex vertical>
          <Card
            style={{
              height: "200px",
              width: "540px",
            }}
          >
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Имя счета:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>{accData.name}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Номер счета:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                {convertAccNumber(accData.number)}
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  ФИО владельца счета:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>
                  {accData.user.surname +
                    " " +
                    accData.user.name +
                    " " +
                    accData.user.patronymic}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Дата открытия счета:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                <Text>{convertAccDate(accData.creationDate)}</Text>
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
                >{`${accData.currentBalance} ${accData.currency.code}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: infoLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Основной счет:
                </Text>
              </Col>
              <Col style={{ width: infoValueWidth }}>
                {accData.isForTransfersByNickname === true ? (
                  <Tag color="green">Да</Tag>
                ) : (
                  <Tag color="red">Нет</Tag>
                )}
              </Col>
            </Row>
          </Card>
          <Flex
            justify="center"
            gap={20}
            style={{ margin: "20px 0px 0px 0px" }}
          >
            <Button
              type="primary"
              onClick={handleMakeTransaction}
              disabled={!accData.isActive}
            >
              Сделать перевод
            </Button>
            <Button onClick={handleOpenSettings} disabled={!accData.isActive}>
              Настройки счета
            </Button>
          </Flex>
        </Flex>
        <Card
          title={<Text>Привязанные карты</Text>}
          styles={{ body: { padding: "10px 15px 10px 15px" } }}
          style={{ width: "600px", height: "240px" }}
        >
          {accData.cards.length === 0 ? (
            <Flex align="center" justify="center" style={{ height: "140px" }}>
              <Text style={{ fontSize: "16px" }}>
                У вас нет привязанных карт к данному счету
              </Text>
            </Flex>
          ) : (
            <Table
              showHeader={false}
              dataSource={accData.cards}
              pagination={{ position: ["none", "none"] }}
            >
              <Column
                title="Название карты"
                key="name"
                width={"170px"}
                dataIndex="name"
                render={(_, record) => <Text>{record.name}</Text>}
              />
              <Column
                title="Номер карты"
                key="number"
                width={"290px"}
                dataIndex="number"
                render={(_, record) => (
                  <Link to={`/cards/${record.id}`} component={Typography.Link}>
                    {convertCardNumber(record.number)}
                  </Link>
                )}
              />
              <Column
                title="Статус"
                dataIndex="isActive"
                key="status"
                render={(isActive) =>
                  isActive === true ? (
                    <Tag color={"green"} key="active">
                      Активна
                    </Tag>
                  ) : (
                    <Tag color={"red"} key="no-active">
                      Неактивна
                    </Tag>
                  )
                }
              />
            </Table>
          )}
        </Card>
      </Flex>
      <Modal
        title={
          isAccountClosable === true
            ? "Закрытие счета"
            : "Вы не можете закрыть этот счет"
        }
        open={openModalCloseAcc}
        onOk={handleOkModalCloseAcc}
        onCancel={handleCancelModalCloseAcc}
        okButtonProps={{ type: "primary", disabled: !isAccountClosable }}
        okType="danger"
        okText="Закрыть"
        cancelText="Отмена"
      >
        <Text style={{ fontSize: "16px" }}>{modalText}</Text>
      </Modal>

      {isSettingsOpened === true ? (
        <Flex
          align="flex-start"
          justify="flex-start"
          style={{ width: "80%", margin: "20px 0px 0px 100px" }}
          gap={100}
        >
          <Card style={{ width: "420px" }}>
            <Flex align="flex-start" justify="center" gap={40}>
              <Flex
                align="center"
                justify="flex-start"
                gap={30}
                vertical
                style={{ width: "170px" }}
              >
                <Button type="primary" onClick={handleChangeName}>
                  Изменить имя
                </Button>

                <Button onClick={handleCancelSett}>Закрыть</Button>
              </Flex>
              <Flex
                align="center"
                justify="flex-start"
                gap={30}
                vertical
                style={{ width: "170px" }}
              >
                <Button onClick={handleChangeState}>
                  {accData.isForTransfersByNickname === true
                    ? "Сделать неосновным"
                    : "Сделать основным"}
                </Button>
                <Button onClick={handleCloseAccount} danger>
                  Закрыть счет
                </Button>
              </Flex>
            </Flex>
          </Card>

          {isChangingName === true ? (
            <Card style={{ width: "450px", margin: "0px 0px 0px 100px" }}>
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
                    onChange={(e) => {
                      setAccName(e.target.value);
                    }}
                  />
                </Flex>
                <Flex gap={20} align="center" justify="center">
                  <Button onClick={() => setIsChangingName(false)}>
                    Отмена
                  </Button>
                  <Button type="primary" onClick={handleEnterName}>
                    Применить
                  </Button>
                </Flex>
              </Flex>
            </Card>
          ) : null}
        </Flex>
      ) : (
        <Flex
          vertical
          gap={20}
          align="center"
          style={{
            margin: "20px 0px 0px 0px",
            width: "90%",
          }}
        >
          <Card style={{ width: "900px" }}>
            <Flex gap={10}>
              <Text style={{ padding: "3px 0px 0px 0px" }}>
                Выписка по счету за период:{" "}
              </Text>
              <RangePicker
                value={[dateStart, dateEnd]}
                maxDate={dayjs()}
                onChange={handleDateRangeChange}
                presets={[
                  {
                    label: "Последние 7 дней",
                    value: [
                      dayjs().startOf("day").add(-7, "d"),
                      dayjs().startOf("day"),
                    ],
                  },
                  {
                    label: "Последние 14 дней",
                    value: [
                      dayjs().startOf("day").add(-14, "d"),
                      dayjs().startOf("day"),
                    ],
                  },
                  {
                    label: "Последние 30 дней",
                    value: [
                      dayjs().startOf("day").add(-30, "d"),
                      dayjs().startOf("day"),
                    ],
                  },
                  {
                    label: "Последние 3 месяца",
                    value: [
                      dayjs().startOf("day").add(-3, "M"),
                      dayjs().startOf("day"),
                    ],
                  },
                  {
                    label: "Последние 6 месяцев",
                    value: [
                      dayjs().startOf("day").add(-6, "M"),
                      dayjs().startOf("day"),
                    ],
                  },
                ]}
              />
              <Button
                type="primary"
                style={{ margin: "0px 5px" }}
                onClick={handleShowTrans}
              >
                Показать
              </Button>
              <Button onClick={exportToCSV}>Экспорт в csv</Button>
            </Flex>
          </Card>
          <Table
            dataSource={transData}
            style={{ width: "100%" }}
            pagination={{ pageSize: 5 }}
          >
            <Column
              width="80px"
              title="Номер"
              dataIndex="id"
              key="id"
              render={(id) => <Text>{"000" + id}</Text>}
            />
            <Column
              width="160px"
              title="Время"
              dataIndex="datetime"
              key="datetime"
              render={(datetime) => convertDatetime(datetime)}
            />
            <Column
              width="160px"
              title="Тип операции"
              dataIndex="accountRecipientNumber"
              key="operation type"
              render={(accRecNumber) => convertOperationType(accRecNumber)}
              filters={[
                {
                  text: "Отправка средств",
                  value: "Отправка средств",
                },
                {
                  text: "Получение средств",
                  value: "Получение средств",
                },
              ]}
              onFilter={(value, record) => filterType(value, record)}
            />
            <Column
              width="150px"
              title="Статус операции"
              dataIndex="status"
              key="status"
              render={(operStatus) => convertOperStatus(operStatus)}
            />
            <Column
              width="220px"
              title="Сумма в валюте операции"
              dataIndex="paymentAmount"
              key="paymentAmount"
              render={(amount) => (
                <Text>{amount.toFixed(2) + " " + accData.currency.code}</Text>
              )}
            />
            <Column
              width="150px"
              title="Отправитель"
              dataIndex="userSenderNickname"
              key="userSenderNickname"
              render={(nickname) => (
                <Text>
                  {nickname === accData.user.nickname ? "Вы" : nickname}
                </Text>
              )}
            />
            <Column
              width="150px"
              title="Получатель"
              dataIndex="userRecipientNickname"
              key="userRecipientNickname"
              render={(nickname) => (
                <Text>
                  {nickname === accData.user.nickname ? "Вы" : nickname}
                </Text>
              )}
            />
            <Column
              title="Информация"
              dataIndex="information"
              key="information"
            />
          </Table>
        </Flex>
      )}
    </Flex>
  );
}
