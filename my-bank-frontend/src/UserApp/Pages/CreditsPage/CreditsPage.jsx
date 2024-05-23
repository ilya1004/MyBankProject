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

const getCreditsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditAccounts/GetAllInfoByCurrentUser?includeData=${true}&onlyActive=${true}`
    );
    return { creditsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { creditsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { creditsData: null, error: err.response };
  }
};

export async function loader() {
  const { creditsData, error } = await getCreditsData();
  if (!creditsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }

  const creditsDataTable = [];
  for (let i = 0; i < creditsData.length; i++) {
    creditsDataTable.push({
      key: i.toString(),
      id: creditsData[i].id,
      name: creditsData[i].name,
      number: creditsData[i].number,
      currentBalance: creditsData[i].currentBalance,
      creditStartBalance: creditsData[i].creditStartBalance,
      creditGrantedAmount: creditsData[i].creditGrantedAmount,
      creationDate: creditsData[i].creationDate,
      interestRate: creditsData[i].interestRate,
      interestCalculationType: creditsData[i].interestCalculationType,
      creditTermInDays: creditsData[i].creditTermInDays,
      totalPaymentsNumber: creditsData[i].totalPaymentsNumber,
      madePaymentsNumber: creditsData[i].madePaymentsNumber,
      hasPrepaymentOption: creditsData[i].hasPrepaymentOption,
      isActive: creditsData[i].isActive,
      user: creditsData[i].user,
      currency: creditsData[i].currency,
    });
  }

  return { creditsData, creditsDataTable };
}

export default function CreditsPage() {
  const { creditsData, creditsDataTable } = useLoaderData();

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
      <Table
        dataSource={[record]} 
        // pagination={{ position: ["none", "none"] }}
        pagination={false}
      >
        <Column
          width="170px"
          title="Процентная ставка"
          dataIndex="interestRate"
          key="interestRate"
          render={(value) => <Text>{`${value} %`}</Text>}
        />
        <Column
          width="230px"
          title="Способ начисления процентов"
          dataIndex="interestCalculationType"
          key="interestCalculationType"
          render={(value) => (
            <Text>{convertInterestCalculationType(value)}</Text>
          )}
        />
        <Column
          width="180px"
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
          render={(hasPrepaymentOption) =>
            hasPrepaymentOption === true ? (
              <Tag color="green">Да</Tag>
            ) : (
              <Tag color="red">Нет</Tag>
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
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex style={{ width: "80%" }}>
          <Title style={{ marginLeft: "40px" }} level={2}>
            Мои кредиты
          </Title>
        </Flex>
        <Flex align="center" justify="center" style={{ width: "100%" }}>
          <Table
            dataSource={creditsDataTable}
            style={{ width: "80%" }}
            expandable={{
              expandedRowRender: (record) => expandedRowRender(record),
            }}
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
            <Column
              width="150px"
              title="Общая сумма"
              dataIndex="creditStartBalance"
              key="creditStartBalance"
              render={(_, record) => (
                <Text>{`${record.creditStartBalance} ${record.currency.code}`}</Text>
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
        <Flex justify="space-between" style={{ width: "80%" }}>
          <Button
            style={{ margin: "20px 0px 20px 20px" }}
            type="primary"
            onClick={() => navigate("add")}
          >
            Оформить кредит
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
