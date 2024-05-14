import { Typography, Button, Flex, Card, Col, Row, Tag, Table } from "antd";
import { useLoaderData, useNavigate, redirect } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;
const { Column } = Table;

const getCreditData = async (accountId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditAccounts/GetInfoById?creditAccountId=${accountId}&includeData=${true}`
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
  return { creditData };
}

const infoLabelWidth = "220px";
const infoValueWidth = "280px";

export default function ModeratorCreditInfoPage() {
  const navigate = useNavigate();

  const { creditData } = useLoaderData();

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
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
          </Card>
        </Flex>
        <Flex
          justify="center"
          align="center"
          style={{
            width: "60%",
          }}
        >
          <Table
            dataSource={creditData.payments}
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
              // width="150px"
              title="Статус выплаты"
              dataIndex="status"
              key="status"
              render={(operStatus) => convertOperStatus(operStatus)}
            />
          </Table>
        </Flex>
      </Flex>
    </Flex>
  );
}
