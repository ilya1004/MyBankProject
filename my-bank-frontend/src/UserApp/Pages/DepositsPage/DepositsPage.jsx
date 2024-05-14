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
      `DepositAccounts/GetAllInfoByCurrentUser?includeData=true&onlyActive=true`
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

  const depositsDataTable = [];
  for (let i = 0; i < depositsData.length; i++) {
    depositsDataTable.push({
      key: i.toString(),
      id: depositsData[i].id,
      name: depositsData[i].name,
      number: depositsData[i].number,
      currentBalance: depositsData[i].currentBalance,
      depositStartBalance: depositsData[i].depositStartBalance,
      creationDate: depositsData[i].creationDate,
      interestRate: depositsData[i].interestRate,
      depositTermInDays: depositsData[i].depositTermInDays,
      totalAccrualsNumber: depositsData[i].totalAccrualsNumber,
      madeAccrualsNumber: depositsData[i].madeAccrualsNumber,
      isRevocable: depositsData[i].isRevocable,
      hasCapitalisation: depositsData[i].hasCapitalisation,
      hasInterestWithdrawalOption: depositsData[i].hasInterestWithdrawalOption,
      isActive: depositsData[i].isActive,
      user: depositsData[i].user,
      currency: depositsData[i].currency,
    });
  }

  return { depositsData, depositsDataTable };
}

export default function DepositsPage() {
  const { depositsData, depositsDataTable } = useLoaderData();

  const navigate = useNavigate();

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    return `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
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
      if (years % 10 == 1) {
        return `${years} год`;
      } else if (2 <= years % 10 && years % 10 <= 4) {
        return `${years} годa`;
      } else {
        return `${years} лет`;
      }
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
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex style={{ width: "80%" }}>
          <Title style={{ marginLeft: "40px" }} level={2}>
            Мои вклады
          </Title>
        </Flex>
        <Flex align="center" justify="center" style={{ width: "100%" }}>
          <Table
            dataSource={depositsDataTable}
            style={{ width: "80%" }}
            expandable={{
              expandedCreditsTable: expandedRowRender,
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
            Открыть вклад
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
