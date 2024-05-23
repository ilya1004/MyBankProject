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
  Space,
} from "antd";
import {
  ExclamationCircleTwoTone,
  CheckCircleTwoTone,
  CloseCircleTwoTone,
} from "@ant-design/icons";
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

const { Title, Text, Paragraph } = Typography;
const { RangePicker } = DatePicker;
const { Column } = Table;

const getCardData = async (cardId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Cards/GetInfoByCurrentUser?cardId=${cardId}&includeData=${true}`
    );
    return { cardData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { cardData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { cardData: null, error: err.response };
  }
};

const getMonthTransactionsData = async (accountNumber) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    let dateS = dayjs().startOf("month").toJSON();
    let dateE = dayjs().startOf("day").toJSON();
    console.log(dateS);
    console.log(dateE);
    const res = await axiosInstance.get(
      `Transactions/GetAllInfoByPersonalAccountNumber?accountNumber=${accountNumber}&dateStart=${dateS}&dateEnd=${dateE}`
    );
    console.log(res.data);
    return { transactionsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { transactionsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { transactionsData: null, error: err.response };
  }
};

export async function loader({ params }) {
  const { cardData, error } = await getCardData(params.cardId);
  if (!cardData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  const { transactionsData, error: error1 } = await getMonthTransactionsData(
    cardData.personalAccount.number
  );
  if (!transactionsData) {
    if (error1.status === 401 || error1.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error1.status,
      });
    }
  }

  console.log(transactionsData);

  let transNum = transactionsData.length;
  let transSum = 0;

  for (let i = 0; i < transactionsData.length; i++) {
    transSum += transactionsData[i].paymentAmount;
  }

  console.log(transNum);
  console.log(transSum);

  return { cardData, transNum, transSum };
}

const infoLabelWidth = "230px";
const infoValueWidth = "270px";

export default function CardInfoPage() {
  const [dateStart, setDateStart] = useState(null);
  const [dateEnd, setDateEnd] = useState(null);
  const [transData, setTransData] = useState();
  const [openModal, setOpenModal] = useState(false);
  const [openModalDelCard, setOpenModalDelCard] = useState(false);
  const [newPincode, setNewPincode] = useState();
  const [cardName, setCardName] = useState();
  const [isSettingsOpened, setIsSettingsOpened] = useState(false);
  const [isChangingPincode, setIsChangingPincode] = useState(false);
  const [isChangingName, setIsChangingName] = useState(false);
  let revalidator = useRevalidator();
  const navigate = useNavigate();

  const { cardData, transNum, transSum } = useLoaderData();

  const convertNumberCard = () => {
    let numStr = cardData.number.toString();
    return `${numStr.substring(0, 1)}XXX XXXX XXXX ${numStr.substring(12, 16)}`;
  };

  const convertValidCard = () => {
    let dateObj = new Date(cardData.expirationDate);
    let year = dateObj.getUTCFullYear().toString().substring(2, 4);
    let month =
      dateObj.getUTCMonth() < 10
        ? "0" + dateObj.getMonth().toString()
        : dateObj.getMonth().toString();

    return `${month}/${year}`;
  };

  const convertAccountNumber = () => {
    let numStr = cardData.personalAccount.number;
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const convertDate = () => {
    let dateObj = new Date(cardData.creationDate);
    return `${dateObj.toLocaleDateString()}`;
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
      const res = await axiosInstance.get(
        `Transactions/GetAllInfoByPersonalAccountNumber?accountNumber=${cardData.personalAccount.number}&dateStart=${dateS}&dateEnd=${dateE}`
      );
      return res.data.list;
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
      record.accountSenderNumber === cardData.personalAccount.number
    ) {
      res = true;
    } else if (
      value === "Получение средств" &&
      record.accountRecipientNumber === cardData.personalAccount.number
    ) {
      res = true;
    }
    console.log(res);
    return res;
  };

  const convertOperationType = (accRecNumber) => {
    let txt = "";
    if (accRecNumber === cardData.personalAccount.number) {
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

  const setCardOperationsStatus = async (operStatus) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `Cards/UpdateOperationsStatus?cardId=${cardData.id}&isOpersAllowed=${operStatus}`
      );
      if (operStatus === false) {
        showMessageStc("Операции по карте были успешно запрещены", "success");
      } else {
        showMessageStc("Операции по карте были успешно разрешены", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const setCardNewPincode = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      id: cardData.id,
      pincode: newPincode,
    };
    try {
      const res = await axiosInstance.put(`Cards/UpdatePincode`, data);
      console.log(res.data["status"]);
      showMessageStc("Пин-код был успешно изменен", "success");
      revalidator.revalidate();
    } catch (err) {
      let sts = handleResponseError(err.response);
      showMessageStc(sts[0], sts[1]);
    }
  };

  const setCardNewName = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `Cards/UpdateName?cardId=${cardData.id}&name=${cardName}`
      );
      console.log(res.data["status"]);
      showMessageStc("Название карты было успешно изменено", "success");
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const deleteCard = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `Cards/UpdateStatus?cardId=${cardData.id}&isActive=${false}`
      );
      showMessageStc("Карта была успешно заблокирована", "success");
      navigate("/cards");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleOkModal = () => {
    setOpenModal(false);
    if (cardData.isOperationsAllowed === true) {
      setCardOperationsStatus(false);
    } else {
      setCardOperationsStatus(true);
    }
  };

  const handleCancelModal = () => {
    setOpenModal(false);
  };

  const handleOperStatus = () => {
    setOpenModal(true);
  };

  const handleChangePincode = () => {
    setIsChangingName(false);
    isChangingPincode === false
      ? setIsChangingPincode(true)
      : setIsChangingPincode(false);
  };

  const handleNewPincodeEdit = (e) => {
    setNewPincode(e.target.value);
  };

  const handleCancelPincode = () => {
    setIsChangingPincode(false);
  };

  const handleEnterPincode = () => {
    setCardNewPincode();
    setIsChangingPincode(false);
  };

  const handleOpenSettings = () => {
    setIsChangingPincode(false);
    setIsChangingName(false);
    if (isSettingsOpened === false) {
      setIsSettingsOpened(true);
    } else {
      setIsSettingsOpened(false);
    }
  };

  const handleCancelSett = () => {
    setIsSettingsOpened(false);
    setIsChangingName(false);
    setIsChangingPincode(false);
  };

  const handleChangeName = () => {
    isChangingName === false
      ? setIsChangingName(true)
      : setIsChangingName(false);
    setIsChangingPincode(false);
  };

  const handleNewCardNameEdit = (e) => {
    setCardName(e.target.value);
  };

  const handleCancelName = () => {
    setIsChangingName(false);
  };

  const handleEnterName = () => {
    setCardNewName();
    setIsChangingName(false);
    setIsSettingsOpened(false);
  };

  const handleDeleteCard = () => {
    setOpenModalDelCard(true);
  };

  const handleOkModalDelCard = () => {
    setOpenModalDelCard(false);
    deleteCard();
    setCardOperationsStatus(false);
  };

  const handleCancelModalDelCard = () => {
    setOpenModalDelCard(false);
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

  return (
    <Flex
      justify="flex-start"
      align="center"
      style={{
        margin: "0px 15px 20px 15px",
        minHeight: "80vh",
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
        <Title level={3}>Детальная информация по карте</Title>
      </Flex>
      <Flex
        align="flex-start"
        justify="center"
        gap={30}
        style={{ width: "90%" }}
      >
        <Flex vertical>
          <Card
            style={{
              width: "340px",
              height: "170px",
              backgroundColor: "#C9D6AD",
            }}
          >
            <Flex vertical gap={7}>
              <Title level={3} style={{ margin: "0px 0px 10px 0px" }}>
                {cardData.name}
              </Title>
              <Text strong style={{ margin: "0px" }}>
                {convertNumberCard()}
              </Text>
              <Text strong>{convertValidCard()}</Text>
              <Text strong>{cardData.personalAccount.currency.code}</Text>
            </Flex>
          </Card>
          <Flex
            justify="center"
            gap={10}
            style={{ margin: "20px 0px 0px 0px" }}
          >
            {cardData.isOperationsAllowed === true ? (
              <Button
                onClick={handleOperStatus}
                danger
                disabled={!cardData.isActive}
              >
                Запретить операции
              </Button>
            ) : (
              <Button onClick={handleOperStatus}>Разрешить операции</Button>
            )}
            <Modal
              title={
                <>
                  <Flex gap={10}>
                    <ExclamationCircleTwoTone
                      twoToneColor="orange"
                      style={{ fontSize: "22px" }}
                    />
                    <Text level={3} strong style={{ fontSize: "20px" }}>
                      {cardData.isOperationsAllowed === true
                        ? "Запрет операций по карте"
                        : "Разрешение операций по карте"}
                    </Text>
                  </Flex>
                </>
              }
              open={openModal}
              onOk={handleOkModal}
              onCancel={handleCancelModal}
              okButtonProps={{ type: "primary" }}
              okText="Подтвердить"
              cancelText="Отмена"
              type="confirm"
            >
              <Text style={{ fontSize: "16px" }}>
                {cardData.isOperationsAllowed === true
                  ? "Вы действительно хотите запретить операции по этой карте?"
                  : "Вы действительно хотите разрешить операции по этой карте?"}
              </Text>
            </Modal>
            <Button onClick={handleOpenSettings} disabled={!cardData.isActive}>
              Настройки карты
            </Button>
          </Flex>
        </Flex>
        <Card
          style={{
            height: "210px",
            width: "550px",
          }}
        >
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                Имя счета:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              <Text>{cardData.personalAccount.name}</Text>
            </Col>
          </Row>
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                Номер счета:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              <Link
                to={`/accounts/${cardData.personalAccount.id}`}
                component={Typography.Link}
              >
                {convertAccountNumber()}
              </Link>
            </Col>
          </Row>
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                ФИО владельца карт-счета:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              <Text>
                {cardData.user.surname +
                  " " +
                  cardData.user.name +
                  " " +
                  cardData.user.patronymic}
              </Text>
            </Col>
          </Row>
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                Дата выпуска карты:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              <Text>{convertDate()}</Text>
            </Col>
          </Row>
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                Операции по карте:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              {cardData.isOperationsAllowed === true ? (
                <Tag color={"green"} key="active">
                  Разрешены
                </Tag>
              ) : (
                <Tag color={"red"} key="no-active">
                  Запрещены
                </Tag>
              )}
            </Col>
          </Row>
          <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
            <Col style={{ width: infoLabelWidth }}>
              <Text type="secondary" style={{ fontSize: "14px" }}>
                Статус карты:
              </Text>
            </Col>
            <Col style={{ width: infoValueWidth }}>
              {cardData.isActive === true ? (
                <Tag color={"green"} key="active">
                  Активна
                </Tag>
              ) : (
                <Tag color={"red"} key="no-active">
                  Заблокирована
                </Tag>
              )}
            </Col>
          </Row>
        </Card>
        <Card
          title={
            <Flex vertical align="center" style={{ width: "100%" }}>
              <Text style={{ fontSize: "14px" }}>
                Критерии бесплатности за текущий месяц
              </Text>
            </Flex>
          }
          style={{
            height: "210px",
            width: "360px",
          }}
          styles={{ header: { padding: "0px" } }}
        >
          <Flex vertical gap={3}>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: "190px" }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Стоимость обслуживания:
                </Text>
              </Col>
              <Col style={{ width: "100px" }}>
                <Text>{`${cardData.cardPackage.price} ${cardData.personalAccount.currency.code}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: "190px" }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Количество операций:
                </Text>
              </Col>
              <Col style={{ width: "100px" }}>
                <Text>{`${transNum}/${cardData.cardPackage.operationsNumber}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: "190px" }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Сумма операций:
                </Text>
              </Col>
              <Col style={{ width: "fit-content" }}>
                <Text>{`${transSum.toFixed(2)}/${
                  cardData.cardPackage.operationsSum
                } ${cardData.personalAccount.currency.code}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
              <Col style={{ width: "190px" }}>
                <Text type="secondary" style={{ fontSize: "14px" }}>
                  Выполнено:
                </Text>
              </Col>
              <Col style={{ width: "100px" }}>
                {transNum >= cardData.cardPackage.operationsNumber ||
                transSum >= cardData.cardPackage.operationsSum ? (
                  <CheckCircleTwoTone
                    twoToneColor="#52c41a"
                    style={{ fontSize: "16px" }}
                  />
                ) : (
                  <CloseCircleTwoTone
                    twoToneColor="red"
                    style={{ fontSize: "16px" }}
                  />
                )}
              </Col>
            </Row>
          </Flex>
        </Card>
      </Flex>
      {isSettingsOpened === true ? (
        <Flex
          align="flex-start"
          justify="flex-start"
          style={{ width: "1000px", margin: "20px 0px 0px 0px" }}
          gap={90}
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
                <Button onClick={handleChangeName} type="primary">
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
                <Button onClick={handleChangePincode} type="primary">
                  Изменить пин-код
                </Button>
                <Button type="primary" danger onClick={handleDeleteCard}>
                  Заблокировать карту
                </Button>
              </Flex>
            </Flex>
          </Card>
          <Modal
            title="Блокировка карты"
            open={openModalDelCard}
            onOk={handleOkModalDelCard}
            onCancel={handleCancelModalDelCard}
            okButtonProps={{ type: "primary" }}
            okType="danger"
            okText="Заблокировать"
            cancelText="Отмена"
          >
            <Text style={{ fontSize: "16px" }}>
              Вы действительно хотите навсегда заблокировать эту карту?
            </Text>
          </Modal>
          {isChangingPincode === true ? (
            <Card style={{ width: "440px" }}>
              <Flex vertical align="center" justify="center" gap={30}>
                <Flex align="center" justify="center" gap={20}>
                  <Text style={{ width: "125px", fontSize: "16px" }}>
                    Новый пин-код:
                  </Text>
                  <Input
                    style={{ width: "170px" }}
                    onChange={handleNewPincodeEdit}
                  />
                </Flex>
                <Flex gap={20} align="center" justify="center">
                  <Button onClick={handleCancelPincode}>Отмена</Button>
                  <Button type="primary" onClick={handleEnterPincode}>
                    Применить
                  </Button>
                </Flex>
              </Flex>
            </Card>
          ) : null}
          {isChangingName === true ? (
            <Card style={{ width: "440px" }}>
              <Flex
                vertical
                align="center"
                justify="center"
                gap={30}
                style={{ width: "100%" }}
              >
                <Flex align="center" justify="center" gap={20}>
                  <Text style={{ width: "185px", fontSize: "16px" }}>
                    Новое название карты:
                  </Text>
                  <Input
                    style={{ width: "180px" }}
                    onChange={handleNewCardNameEdit}
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
          {isChangingName === false && isChangingPincode === false ? (
            <Space style={{ width: "460px" }}>
              <Text></Text>
            </Space>
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
              render={(id) => <Text>{id.toString().padStart(5, "0")}</Text>}
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
                <Text>
                  {amount.toFixed(2) +
                    " " +
                    cardData.personalAccount.currency.code}
                </Text>
              )}
            />
            <Column
              width="150px"
              title="Отправитель"
              dataIndex="userSenderNickname"
              key="userSenderNickname"
              render={(nickname) => (
                <Text>
                  {nickname === cardData.user.nickname ? "Вы" : nickname}
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
                  {nickname === cardData.user.nickname ? "Вы" : nickname}
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
