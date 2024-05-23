import axios from "axios";
import { useState } from "react";
import { Card, Flex, Tag, Typography, Button, Col, Row, Table } from "antd";
import { useNavigate, useLoaderData, redirect, Link } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;

const { Column } = Table;

const infoLabelWidth = "250px";
const infoValueWidth = "200px";

const getModeratorData = async (moderatorId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Moderators/GetInfoById?moderatorId=${moderatorId}&includeData=${true}`
    );
    console.log(res.data.item);
    return { moderatorData: res.data.item, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorData: null, error: err.response };
  }
};

export async function loader({ params }) {
  const { moderatorData, error } = await getModeratorData(params.moderatorId);
  if (!moderatorData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return {
    moderatorData,
    creditRequestsData: moderatorData.creditRequestsReplied,
    creditAccountsData: moderatorData.creditsApproved,
  };
}

export default function ModeratorInfoPage() {
  const { moderatorData, creditRequestsData, creditAccountsData } =
    useLoaderData();

  const navigate = useNavigate();

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    return `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
  };

  const convertInterestCalculationType = (interestCalculationType) => {
    if (interestCalculationType === "annuity") {
      return "Аннуитетный";
    } else if (interestCalculationType === "differential") {
      return "Дифференцированный";
    } else {
      return "";
    }
  };

  const convertMonths = (days) => {
    let months = days / 30;
    if (months.toString()[-1] === 1) {
      return `${months} месяц`;
    } else if (2 <= months.toString()[-1] && months.toString()[-1] <= 4) {
      return `${months} месяца`;
    } else {
      return `${months} месяцев`;
    }
  };

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const expandedRowRender = (record) => {
    return (
      <Table dataSource={[record]} pagination={false}>
        <Column
          width="170px"
          title="Сумма для выплаты"
          dataIndex="creditStartBalance"
          key="creditStartBalance"
          render={(_, record) => (
            <Text>
              {`${record.creditStartBalance} ${record.currency.code}`}
            </Text>
          )}
        />
        <Column
          width="170px"
          title="Процентная ставка"
          dataIndex="interestRate"
          key="interestRate"
          render={(value) => <Text>{`${value} %`}</Text>}
        />
        <Column
          width="200px"
          title="Способ начисления процентов"
          dataIndex="interestCalculationType"
          key="interestCalculationType"
          render={(value) => (
            <Text>{convertInterestCalculationType(value)}</Text>
          )}
        />
        <Column
          width="200px"
          title="Срок выдачи кредита"
          dataIndex="creditTermInDays"
          key="creditTermInDays"
          render={(days) => <Text>{convertMonths(days)}</Text>}
        />
        <Column
          width="200px"
          title="Выполненные платежи"
          dataIndex="payments"
          key="payments"
          render={(_, record) => (
            <Text>{`${record.madePaymentsNumber}/${record.totalPaymentsNumber}`}</Text>
          )}
        />
        <Column
          width="240px"
          title="Возможность досрочного погашения"
          dataIndex="hasPrepaymentOption"
          key="hasPrepaymentOption"
          render={(hasPrepaymentOption) => (
            <Text>
              {hasPrepaymentOption === true ? (
                <Tag color="green">Да</Tag>
              ) : (
                <Tag color="red">Нет</Tag>
              )}
            </Text>
          )}
        />
        <Column
          title="Статус"
          dataIndex="isActive"
          key="status"
          render={(isActive) =>
            isActive === true ? (
              <Tag color={"green"} key="active">
                Активен
              </Tag>
            ) : (
              <Tag color={"red"} key="no-active">
                Неактивен
              </Tag>
            )
          }
        />
      </Table>
    );
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "82vh" }}
      >
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Button
            style={{ margin: "18px 0px 0px 20px" }}
            onClick={() => navigate(-1)}
          >
            Назад
          </Button>
          <Title level={3}>Информация о модераторе</Title>
        </Flex>
        <Flex align="center" justify="flex-start">
          <Card>
            <Flex vertical gap={16} style={{ width: "100%" }} align="center">
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Никнейм:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{moderatorData.nickname}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Логин:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{moderatorData.login}</Text>
                </Col>
              </Row>
              <Row gutter={[16, 16]} style={{ marginBottom: "5px" }}>
                <Col style={{ width: infoLabelWidth }}>
                  <Text type="secondary" style={{ fontSize: "14px" }}>
                    Дата создания учетной записи:
                  </Text>
                </Col>
                <Col style={{ width: infoValueWidth }}>
                  <Text>{convertDatetime(moderatorData.creationDate)}</Text>
                </Col>
              </Row>
            </Flex>
          </Card>
        </Flex>
        <Flex align="center" style={{ margin: "0px 0px 10px 0px" }}>
          <Title level={4}>Обработанные запросы на выдачу кредита</Title>
        </Flex>
        <Flex align="center" justify="center" style={{ width: "100%" }}>
          <Table dataSource={creditRequestsData} style={{ width: "90%" }}>
            <Column
              width="40px"
              title="Номер"
              dataIndex="id"
              key="id"
              render={(id) => <Text>{id.toString().padStart(5, "0")}</Text>}
            />
            <Column
              width="110px"
              title="Размер кредита"
              dataIndex="startBalance"
              key="startBalance"
              render={(_, record) => (
                <Text>
                  {`${record.creditPackage.creditStartBalance} ${record.creditPackage.currency.code}`}
                </Text>
              )}
            />
            <Column
              width="130px"
              title="Процентная ставка"
              dataIndex="interestRate"
              key="interestRate"
              render={(_, record) => (
                <Text>{`${record.creditPackage.interestRate} %`}</Text>
              )}
            />
            <Column
              width="190px"
              title="Способ начисления процентов"
              dataIndex="interestCalculationType"
              key="interestCalculationType"
              render={(_, record) => (
                <Text>
                  {convertInterestCalculationType(
                    record.creditPackage.interestCalculationType
                  )}
                </Text>
              )}
            />
            <Column
              width="150px"
              title="Срок выдачи кредита"
              dataIndex="creditTermInDays"
              key="creditTermInDays"
              render={(_, record) => (
                <Text>
                  {convertMonths(record.creditPackage.creditTermInDays)}
                </Text>
              )}
            />
            <Column
              width="240px"
              title="Возможность досрочного погашения"
              dataIndex="hasPrepaymentOption"
              key="hasPrepaymentOption"
              render={(_, record) => (
                <Text>
                  {record.creditPackage.hasPrepaymentOption === true ? (
                    <Tag color="green">Да</Tag>
                  ) : (
                    <Tag color="red">Нет</Tag>
                  )}
                </Text>
              )}
            />
            <Column
              width="140px"
              title="Статус одобрения"
              dataIndex="isApproved"
              key="isApproved"
              render={(isApproved) => (
                <Text>
                  {isApproved === true ? (
                    <CheckCircleTwoTone
                      twoToneColor="#52c41a"
                      style={{ fontSize: "20px" }}
                    />
                  ) : (
                    <CloseCircleTwoTone
                      twoToneColor="red"
                      style={{ fontSize: "18px" }}
                    />
                  )}
                </Text>
              )}
            />
          </Table>
        </Flex>
        <Flex align="center" style={{ margin: "0px 0px 10px 0px" }}>
          <Title level={4}>Информация о выданных кредитах</Title>
        </Flex>
        <Flex align="center" justify="center" style={{ width: "100%" }}>
          <Table
            dataSource={creditAccountsData}
            style={{ width: "90%" }}
            expandable={{
              expandedRowRender: (record) => expandedRowRender(record),
            }}
          >
            <Column
              width="80px"
              title="Номер"
              dataIndex="id"
              key="id"
              render={(id) => <Text>{id.toString().padStart(5, "0")}</Text>}
            />
            <Column
              width="180px"
              title="Название"
              dataIndex="name"
              key="name"
              render={(name) => <Text>{name}</Text>}
            />
            <Column
              width="290px"
              title="Номер"
              dataIndex="number"
              key="number"
              render={(number) => <Text>{convertAccNumber(number)}</Text>}
            />
            <Column
              width="220px"
              title="Никнейм владельца"
              dataIndex="userNickname"
              key="userNickname"
              render={(_, record) => (
                <Link
                  to={`/admin/users/${record.user.id}`}
                  component={Typography.Link}
                >{`${record.user.nickname}`}</Link>
              )}
            />
            <Column
              width="220px"
              title="ФИО владельца"
              dataIndex="userFio"
              key="userFio"
              render={(_, record) => (
                <Text>{`${record.user.surname} ${record.user.name} ${record.user.patronymic}`}</Text>
              )}
            />
            <Column
              width="160px"
              title="Текущий баланс"
              dataIndex="currentBalance"
              key="currentBalance"
              render={(_, record) => (
                <Text>{`${record.currentBalance} ${record.currency.code}`}</Text>
              )}
            />
            <Column
              width="180px"
              title="Дата создания"
              dataIndex="creationDate"
              key="creationDate"
              render={(_, record) => (
                <Text>{convertDatetime(record.creationDate)}</Text>
              )}
            />
          </Table>
        </Flex>
      </Flex>
    </>
  );
}
