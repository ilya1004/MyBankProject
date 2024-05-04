import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Table, Tag } from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

const getDepositsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `DepositAccounts/GetInfoByCurrentUser?includeData=true&onlyActive=true`
    );
    return { depositsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { depositsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { depositsData: null, error: err.response };
  }
};

export async function loader() {
  const { depositsData, error } = await getDepositsData();
  if (!depositsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { depositsData };
}

export default function DepositsPage() {
  const { depositsData } = useLoaderData();

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

  const expandedRowRender = () => {
    return (
      <Table
        dataSource={depositsData}
        pagination={{ position: ["none", "none"] }}
      >
        <Column
          width="170px"
          title="Процентная ставка"
          dataIndex="interestRate"
          key="interestRate"
          render={(value) => <Text>{`${value} %`}</Text>}
        />
        <Column
          width="180px"
          title="Срок размещения вклада"
          dataIndex="depositTermInDays"
          key="depositTermInDays"
          render={(days) => <Text>{convertMonths(days)}</Text>}
        />
        <Column
          width="200px"
          title="Начисления процентов"
          dataIndex="accruals"
          key="accruals"
          render={(_, record) => (
            <Text>{`${record.madeAccrualsNumber}/${record.totalAccrualsNumber}`}</Text>
          )}
        />
        <Column
          width="160px"
          title="Отзывной"
          dataIndex="isRevocable"
          key="isRevocable"
          render={(isRevocable) => (
            <Text>
              {isRevocable === true ? (
                <Tag color="green">Да</Tag>
              ) : (
                <Tag color="red">Нет</Tag>
              )}
            </Text>
          )}
        />
        <Column
          width="160px"
          title="Капитализация"
          dataIndex="hasCapitalisation"
          key="hasCapitalisation"
          render={(hasCapitalisation) => (
            <Text>
              {hasCapitalisation === true ? (
                <Tag color="green">Да</Tag>
              ) : (
                <Tag color="red">Нет</Tag>
              )}
            </Text>
          )}
        />
        <Column
          width="160px"
          title="Возможность снятия процентов"
          dataIndex="hasInterestWithdrawalOption"
          key="hasInterestWithdrawalOption"
          render={(hasInterestWithdrawalOption) => (
            <Text>
              {hasInterestWithdrawalOption === true ? (
                <Tag color="green">Да</Tag>
              ) : (
                <Tag color="red">Нет</Tag>
              )}
            </Text>
          )}
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
        <Flex style={{ width: "80%" }}>
          <Title style={{ marginLeft: "40px" }} level={2}>
            Мои вклады
          </Title>
        </Flex>
        <Flex align="center" justify="center" style={{ width: "100%" }}>
          <Table
            dataSource={depositsData}
            style={{ width: "80%" }}
            expandable={{ expandedRowRender, defaultExpandedRowKeys: ["0"] }}
            pagination={{ position: ["none", "none"] }}
          >
            <Column
              width="180px"
              title="Название"
              dataIndex="name"
              key="name"
              render={(name) => <Text>{name}</Text>}
            />
            <Column
              width="280px"
              title="Номер"
              dataIndex="number"
              key="number"
              render={(_, record) => (
                <Link to={`${record.id}`}>
                  {convertAccNumber(record.number)}
                </Link>
              )}
            />
            <Column
              width="150px"
              title="Текущий баланс"
              dataIndex="currentBalance"
              key="currentBalance"
              render={(_, record) => (
                <Text>{`${record.currentBalance} ${record.currency.code}`}</Text>
              )}
            />
            {/* <Column
              width="150px"
              title="Общая сумма"
              dataIndex="creditStartBalance"
              key="creditStartBalance"
              render={(_, record) => (
                <Text>{`${record.creditStartBalance} ${record.currency.code}`}</Text>
              )}
            /> */}
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
        <Flex justify="space-between" style={{ width: "80%" }}>
          <Button
            style={{ margin: "20px 0px 20px 20px" }}
            type="primary"
            onClick={() => navigate('add')}
          >
            Открыть вклад
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
