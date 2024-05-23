import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Table, Tag } from "antd";
import {
  Link,
  useLoaderData,
  redirect,
  useNavigate,
  useRevalidator,
} from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

const getRequestsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CreditRequests/GetAllInfo?includeData=${true}&isAnswered=${false}`
    );
    return { requestsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { requestsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { requestsData: null, error: err.response };
  }
};

export async function loader() {
  const { requestsData, error } = await getRequestsData();
  if (!requestsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { requestsData };
}

export default function CreditRequests() {
  const revalidator = useRevalidator();
  const navigate = useNavigate();

  const { requestsData } = useLoaderData();

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

  const setCreditRequestApprovedState = async (id, state) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `CreditRequests/UpdateState?creditRequestId=${id}&isApproved=${state}`
      );
      if (state == false) {
        showMessageStc("Запрос на кредит был успешно отклонен", "info");
      } else {
        showMessageStc("Запрос на кредит был успешно одобрен", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleApproveCredit = (creditRequestId) => {
    setCreditRequestApprovedState(creditRequestId, true);
  };

  const handleRejectCredit = (creditRequestId) => {
    setCreditRequestApprovedState(creditRequestId, false);
  };

  const expandedRowRender = () => {
    return (
      <Table
        dataSource={requestsData}
        // pagination={{ position: ["none", "none"] }}
        pagination={"hideOnSinglePage"}
      >
        <Column
          width="170px"
          title="Процентная ставка"
          dataIndex="creditPackage"
          key="interestRate"
          render={(creditPackage) => (
            <Text>{`${creditPackage.interestRate} %`}</Text>
          )}
        />
        <Column
          width="230px"
          title="Способ начисления процентов"
          dataIndex="creditPackage"
          key="interestCalculationType"
          render={(creditPackage) => (
            <Text>
              {convertInterestCalculationType(
                creditPackage.interestCalculationType
              )}
            </Text>
          )}
        />
        <Column
          width="180px"
          title="Срок выдачи кредита"
          dataIndex="creditPackage"
          key="creditTermInDays"
          render={(creditPackage) => (
            <Text>{convertMonths(creditPackage.creditTermInDays)}</Text>
          )}
        />
        <Column
          width="240px"
          title="Возможность досрочного погашения"
          dataIndex="creditPackage"
          key="hasPrepaymentOption"
          render={(creditPackage) => (
            <Text>
              {creditPackage.hasPrepaymentOption === true ? (
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
          <Title style={{ marginLeft: "20px" }} level={2}>
            Запросы на выдачу кредитов
          </Title>
        </Flex>
        <Table
          dataSource={requestsData}
          style={{ width: "80%" }}
          expandable={{ expandedCreditsTable: expandedRowRender, defaultExpandedRowKeys: ["0"] }}
          pagination={{ pageSize: 8 }}
        >
          <Column
            title="Номер"
            dataIndex="id"
            key="id"
            width={"100px"}
            render={(id) => <Text>{id.toString().padStart(5, "0")}</Text>}
          />
          <Column
            width="180px"
            title="Пользователь"
            dataIndex="user"
            key="userNickname"
            render={(user) => (
              <Link
                to={`/moderator/users-info/${user.id}`}
                component={Typography.Link}
              >
                {user.nickname}
              </Link>
            )}
          />
          <Column
            width="180px"
            title="Пакет кредита"
            dataIndex="creditPackage"
            key="creditPackage"
            render={(creditPackage) => <Text>{creditPackage.name}</Text>}
          />
          <Column
            width="150px"
            title="Общая сумма"
            dataIndex="creditPackage"
            key="creditGrantedAmount"
            render={(creditPackage) => (
              <Text>{`${creditPackage.creditGrantedAmount} ${creditPackage.currency.code}`}</Text>
            )}
          />
          <Column
            width="180px"
            title="Дата создания запроса"
            dataIndex="creationDate"
            key="creationDate"
            render={(_, record) => (
              <Text>{convertDatetime(record.creationDate)}</Text>
            )}
          />
          <Column
            width="180px"
            title="Действия"
            dataIndex="actions"
            key="actions"
            render={(_, record) => (
              <Flex align="center" justify="flex-start" gap={15}>
                <Button
                  type="primary"
                  onClick={() => handleApproveCredit(record.id)}
                >
                  Одобрить
                </Button>
                <Button danger onClick={() => handleRejectCredit(record.id)}>
                  Отказать
                </Button>
              </Flex>
            )}
          />
        </Table>
      </Flex>
    </>
  );
}
