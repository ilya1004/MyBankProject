import React from "react";
import { Typography, Divider, Flex, List, Card, Col, Row, Tag } from "antd";
import CurrencyConventer from "./Components/CurrencyConventer";
import axios from "axios";
import { useLoaderData } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text, Paragraph } = Typography;

const getCurrencyRatesData = async () => {
  const axiosInstance = axios.create();
  try {
    // const axiosInstance = axios.create({
    //   baseURL: BASE_URL,
    //   withCredentials: true,
    // });
    // const res = await axiosInstance.get(`Currency/GetAllInfo`);
    // return { currenciesData: res.data.list, error: null };

    const res = await axiosInstance.get(
      `https://api.nbrb.by/exrates/rates?periodicity=0`
    );
    return { currenciesData: res.data, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { currenciesData: null, error: err.response };
  }
};

const getCardPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CardPackages/GetAllInfo?onlyActive=${true}`
    );
    return { cardPackagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { cardPackagesData: null, error: err.response };
  }
};

const getCreditPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditPackages/GetAllInfo?includeData=${true}&onlyActive=${true}`
    );
    return { creditPackagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { creditPackagesData: null, error: err.response };
  }
};

const getDepositPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `DepositPackages/GetAllInfo?includeData=${true}&onlyActive=${true}`
    );
    return { depositPackagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { depositPackagesData: null, error: err.response };
  }
};

export async function loader() {
  const { currenciesData, error } = await getCurrencyRatesData();
  if (!currenciesData) {
    showMessageStc(
      "Произошла ошибка при получении данных о курсах валют.",
      "error"
    );
  }
  const { cardPackagesData, error: error1 } = await getCardPackagesData();
  if (!cardPackagesData) {
    showMessageStc(
      "Произошла ошибка при получении данных о пакетах карт.",
      "error"
    );
  }
  const { creditPackagesData, error: error2 } = await getCreditPackagesData();
  if (!creditPackagesData) {
    showMessageStc(
      "Произошла ошибка при получении данных о пакетах кредитов.",
      "error"
    );
  }
  const { depositPackagesData, error: error3 } = await getDepositPackagesData();
  if (!depositPackagesData) {
    showMessageStc(
      "Произошла ошибка при получении данных о пакетах депозитов.",
      "error"
    );
  }
  return {
    currenciesData,
    cardPackagesData,
    creditPackagesData,
    depositPackagesData,
  };
}

const colCreditPackLW = "200px";
const colCreditPackTW = "190px";

const colDepositPackLW = "200px";
const colDepositPackTW = "240px";

export default function MainPage() {
  const {
    currenciesData,
    cardPackagesData,
    creditPackagesData,
    depositPackagesData,
  } = useLoaderData();

  const itemColor = "#FFFFF";

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

  const itemCardPackage = (item) => {
    return (
      <List.Item key={item.id}>
        <Card
          style={{
            width: "350px",
            backgroundColor: itemColor,
          }}
        >
          <Title level={2} style={{ margin: "10px 0px 0px 0px" }}>
            {item.name}
          </Title>
          <Title
            level={4}
            style={{ margin: "15px 0px 5px 0px" }}
          >{`${item.price} BYN`}</Title>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Условия бесплатности (в месяц):`}</Paragraph>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Количество операций: ${item.operationsNumber}`}</Paragraph>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Сумма операций: ${item.operationsSum} BYN`}</Paragraph>
        </Card>
      </List.Item>
    );
  };

  const itemCreditPackage = (item) => {
    return (
      <List.Item key={item.id}>
        <Card
          style={{
            width: "440px",
            backgroundColor: itemColor,
          }}
        >
          <Flex vertical gap={10} style={{ width: "100%" }} align="center">
            <Title level={3} style={{ margin: "0px 0px 10px 0px" }}>
              {item.name}
            </Title>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colCreditPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Размер кредита:
                </Text>
              </Col>
              <Col style={{ width: colCreditPackTW }}>
                <Text>{`${item.creditStartBalance} ${item.currency.code}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colCreditPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Процентная ставка:
                </Text>
              </Col>
              <Col style={{ width: colCreditPackTW }}>
                <Text>{`${item.interestRate} %`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colCreditPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Тип выплаты процентов:
                </Text>
              </Col>
              <Col style={{ width: colCreditPackTW }}>
                {item.interestCalculationType === "annuity" ? (
                  <Tag color="green">Аннуитетный</Tag>
                ) : (
                  <Tag color="green">Дифференцированный</Tag>
                )}
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colCreditPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Срок выдачи кредита:
                </Text>
              </Col>
              <Col style={{ width: colCreditPackTW }}>
                <Text>{convertMonths(item.creditTermInDays)}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colCreditPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Другие опции:
                </Text>
              </Col>
              <Col style={{ width: colCreditPackTW }}>
                <Flex>
                  {item.hasPrepaymentOption === true ? (
                    <Tag color="green" style={{ width: "fit-content" }}>
                      Досрочное погашение
                    </Tag>
                  ) : (
                    <Tag color="orange" style={{ width: "fit-content" }}>
                      Без досрочного погашения
                    </Tag>
                  )}
                </Flex>
              </Col>
            </Row>
          </Flex>
        </Card>
      </List.Item>
    );
  };

  const itemDepositPackage = (item) => {
    return (
      <List.Item key={item.id}>
        <Card
          style={{
            width: "490px",
            backgroundColor: itemColor,
          }}
        >
          <Flex vertical gap={10} style={{ width: "100%" }} align="center">
            <Title level={3} style={{ margin: "0px 0px 10px 0px" }}>
              {item.name}
            </Title>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colDepositPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Размер депозита:
                </Text>
              </Col>
              <Col style={{ width: colDepositPackTW }}>
                <Text>{`${item.depositStartBalance} ${item.currency.code}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colDepositPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Процентная ставка:
                </Text>
              </Col>
              <Col style={{ width: colDepositPackTW }}>
                <Text>{`${item.interestRate} %`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colDepositPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Срок выдачи депозита:
                </Text>
              </Col>
              <Col style={{ width: colDepositPackTW }}>
                <Text>{convertMonths(item.depositTermInDays)}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colDepositPackLW }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Другие опции:
                </Text>
              </Col>
              <Col style={{ width: colDepositPackTW }}>
                <Flex gap={10} vertical>
                  {item.isRevocable === true ? (
                    <Tag color="green" style={{ width: "fit-content" }}>
                      Отзывной
                    </Tag>
                  ) : (
                    <Tag color="orange" style={{ width: "fit-content" }}>
                      Безотзывной
                    </Tag>
                  )}
                  {item.hasCapitalisation === true ? (
                    <Tag color="green" style={{ width: "fit-content" }}>
                      С капитализацией
                    </Tag>
                  ) : (
                    <Tag color="orange" style={{ width: "fit-content" }}>
                      Без капитализации
                    </Tag>
                  )}
                  {item.hasInterestWithdrawalOption === true ? (
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
          </Flex>
        </Card>
      </List.Item>
    );
  };

  return (
    <>
      <Flex vertical style={{ minHeight: "80vh", height: "fit-content" }}>
        <Flex align="flex-start" justify="center">
          <Title level={2} style={{ margin: "15px 0px 15px 0px" }}>
            Конвертер валют
          </Title>
        </Flex>
        <CurrencyConventer currenciesData={currenciesData} />
        <Divider style={{ margin: "20px 0px" }} />
        <Flex align="flex-start" justify="center">
          <Title level={2} style={{ margin: "0px 0px 15px 0px" }}>
            Пакеты карт
          </Title>
        </Flex>
        <Flex
          align="center"
          justify="flex-start"
          style={{ margin: "0px 0px 0px 60px", width: "90%" }}
        >
          <List
            itemLayout="horizontal"
            grid={{
              gutter: 20,
              column: cardPackagesData.length,
            }}
            style={{
              margin: "0px 30px",
            }}
            dataSource={cardPackagesData}
            renderItem={(item) => itemCardPackage(item)}
          />
        </Flex>
        <Divider style={{ margin: "20px 0px" }} />
        <Flex align="flex-start" justify="center">
          <Title level={2} style={{ margin: "0px 0px 15px 0px" }}>
            Пакеты кредитов
          </Title>
        </Flex>
        <Flex
          align="center"
          justify="flex-start"
          style={{ margin: "0px 0px 0px 60px", width: "90%" }}
        >
          <List
            itemLayout="horizontal"
            grid={{
              gutter: 20,
              column: creditPackagesData.length,
            }}
            style={{
              margin: "0px 30px",
            }}
            dataSource={creditPackagesData}
            renderItem={(item) => itemCreditPackage(item)}
          />
        </Flex>
        <Divider style={{ margin: "20px 0px" }} />
        <Flex align="flex-start" justify="center">
          <Title level={2} style={{ margin: "0px 0px 15px 0px" }}>
            Пакеты депозитов
          </Title>
        </Flex>
        <Flex
          align="center"
          justify="flex-start"
          style={{ margin: "0px 0px 10px 60px", width: "90%" }}
        >
          <List
            itemLayout="horizontal"
            grid={{
              gutter: 20,
              column: depositPackagesData.length,
            }}
            style={{
              margin: "0px 30px",
            }}
            dataSource={depositPackagesData}
            renderItem={(item) => itemDepositPackage(item)}
          />
        </Flex>
      </Flex>
    </>
  );
}
