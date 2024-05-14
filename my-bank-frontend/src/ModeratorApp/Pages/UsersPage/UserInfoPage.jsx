import axios from "axios";
import { useState } from "react";
import { Tag, Card, Flex, Typography, Button, Table } from "antd";
import {
  EnvironmentOutlined,
  PhoneOutlined,
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
} from "@ant-design/icons";
import {
  useNavigate,
  useLoaderData,
  useRevalidator,
  redirect,
  Link,
} from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;

const { Column } = Table;

export const widthCardAcc = "300px";
export const heightCardAcc = "145px";

const getUserData = async (userId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `User/GetInfoById?userId=${userId}&includeData=${true}`
    );
    return { userData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { userData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { userData: null, error: err.response };
  }
};

export async function loader({ params }) {
  const { userData, error } = await getUserData(params.userId);
  if (!userData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }

  const creditsDataTable = [];
  for (let i = 0; i < userData.creditAccounts.length; i++) {
    creditsDataTable.push({
      key: i.toString(),
      id: userData.creditAccounts[i].id,
      name: userData.creditAccounts[i].name,
      number: userData.creditAccounts[i].number,
      currentBalance: userData.creditAccounts[i].currentBalance,
      creditStartBalance: userData.creditAccounts[i].creditStartBalance,
      creditGrantedAmount: userData.creditAccounts[i].creditGrantedAmount,
      creationDate: userData.creditAccounts[i].creationDate,
      interestRate: userData.creditAccounts[i].interestRate,
      interestCalculationType:
        userData.creditAccounts[i].interestCalculationType,
      creditTermInDays: userData.creditAccounts[i].creditTermInDays,
      totalPaymentsNumber: userData.creditAccounts[i].totalPaymentsNumber,
      madePaymentsNumber: userData.creditAccounts[i].madePaymentsNumber,
      hasPrepaymentOption: userData.creditAccounts[i].hasPrepaymentOption,
      isActive: userData.creditAccounts[i].isActive,
      user: userData.creditAccounts[i].user,
      currency: userData.creditAccounts[i].currency,
    });
  }

  const depositsDataTable = [];
  for (let i = 0; i < userData.depositAccounts.length; i++) {
    depositsDataTable.push({
      key: i.toString(),
      id: userData.depositAccounts[i].id,
      name: userData.depositAccounts[i].name,
      number: userData.depositAccounts[i].number,
      currentBalance: userData.depositAccounts[i].currentBalance,
      depositStartBalance: userData.depositAccounts[i].depositStartBalance,
      creationDate: userData.depositAccounts[i].creationDate,
      interestRate: userData.depositAccounts[i].interestRate,
      depositTermInDays: userData.depositAccounts[i].depositTermInDays,
      totalAccrualsNumber: userData.depositAccounts[i].totalAccrualsNumber,
      madeAccrualsNumber: userData.depositAccounts[i].madeAccrualsNumber,
      isRevocable: userData.depositAccounts[i].isRevocable,
      hasCapitalisation: userData.depositAccounts[i].hasCapitalisation,
      hasInterestWithdrawalOption:
        userData.depositAccounts[i].hasInterestWithdrawalOption,
      isActive: userData.depositAccounts[i].isActive,
      user: userData.depositAccounts[i].user,
      currency: userData.depositAccounts[i].currency,
    });
  }

  return { userData, creditsDataTable, depositsDataTable };
}

export default function ModeratorUserInfoPage() {
  const [buttonText, setButtonText] = useState("");
  const revalidator = useRevalidator();
  const navigate = useNavigate();

  const { userData, creditsDataTable, depositsDataTable } = useLoaderData();

  // const setUserStatus = async (status) => {
  //   const axiosInstance = axios.create({
  //     baseURL: BASE_URL,
  //     withCredentials: true,
  //   });
  //   try {
  //     const res = await axiosInstance.put(
  //       `User/UpdateStatus?userId=${userData.id}&isActive=${status}`
  //     );
  //     if (status === false) {
  //       showMessageStc("Пользователь был успешно заблокирован", "success");
  //     } else {
  //       showMessageStc("Пользователь был успешно разблокирован", "success");
  //     }
  //   } catch (err) {
  //     handleResponseError(err.response);
  //   }
  // };

  const printRegDate = (date) => {
    let dateObj = new Date(date);
    return `Клиент банка с ${dateObj.toLocaleDateString()}`;
  };

  // const handleChangeUserStatus = () => {
  //   userData.isActive === true ? setUserStatus(false) : setUserStatus(true);
  // };

  const dateDifferenceInYears = (date1, date2) =>
    Math.floor(
      Math.max(
        (date2.getFullYear() - date1.getFullYear()) * 12 +
          date2.getMonth() -
          date1.getMonth(),
        0
      ) / 12
    );

  const printYears = (date) => {
    let dateObj = new Date(date);
    let dateNow = new Date();
    let years = Math.floor(
      Math.max(
        (dateNow.getFullYear() - dateObj.getFullYear()) * 12 +
          dateNow.getMonth() -
          dateObj.getMonth(),
        0
      ) / 12
    );

    if (years % 10 == 1) {
      return `${years} год`;
    } else if (2 <= years % 10 && years % 10 <= 4) {
      return `${years} годa`;
    } else {
      return `${years} лет`;
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

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    let txt = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
    return <Text>{txt}</Text>;
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
    let months = Math.floor(days / 30);
    if (months < 24 || months % 12 !== 0) {
      if (months % 10 === 1) {
        return `${months} месяц`;
      } else if (2 <= months % 10 && months % 10 <= 4) {
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

  const expandedCreditsTable = (record) => {
    return (
      <Table dataSource={[record]} pagination={{ position: ["none", "none"] }}>
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
      </Table>
    );
  };

  // const expandedDepositsTable = (record) => {
  //   return (
  //     <Table dataSource={[record]} pagination={{ position: ["none", "none"] }}>
  //       <Column
  //         width="170px"
  //         title="Процентная ставка"
  //         dataIndex="interestRate"
  //         key="interestRate"
  //         render={(value) => <Text>{`${value} %`}</Text>}
  //       />
  //       <Column
  //         width="180px"
  //         title="Срок размещения вклада"
  //         dataIndex="depositTermInDays"
  //         key="depositTermInDays"
  //         render={(days) => <Text>{convertMonths(days)}</Text>}
  //       />
  //       <Column
  //         width="200px"
  //         title="Начисления процентов"
  //         dataIndex="accruals"
  //         key="accruals"
  //         render={(_, record) => (
  //           <Text>{`${record.madeAccrualsNumber}/${record.totalAccrualsNumber}`}</Text>
  //         )}
  //       />
  //       <Column
  //         width="160px"
  //         title="Отзывной"
  //         dataIndex="isRevocable"
  //         key="isRevocable"
  //         render={(isRevocable) => (
  //           <Text>
  //             {isRevocable === true ? (
  //               <Tag color="green">Да</Tag>
  //             ) : (
  //               <Tag color="red">Нет</Tag>
  //             )}
  //           </Text>
  //         )}
  //       />
  //       <Column
  //         width="160px"
  //         title="Капитализация"
  //         dataIndex="hasCapitalisation"
  //         key="hasCapitalisation"
  //         render={(hasCapitalisation) => (
  //           <Text>
  //             {hasCapitalisation === true ? (
  //               <Tag color="green">Да</Tag>
  //             ) : (
  //               <Tag color="red">Нет</Tag>
  //             )}
  //           </Text>
  //         )}
  //       />
  //       <Column
  //         width="160px"
  //         title="Возможность снятия процентов"
  //         dataIndex="hasInterestWithdrawalOption"
  //         key="hasInterestWithdrawalOption"
  //         render={(hasInterestWithdrawalOption) => (
  //           <Text>
  //             {hasInterestWithdrawalOption === true ? (
  //               <Tag color="green">Да</Tag>
  //             ) : (
  //               <Tag color="red">Нет</Tag>
  //             )}
  //           </Text>
  //         )}
  //       />
  //     </Table>
  //   );
  // };

  return (
    <>
      <Flex
        justify="center"
        align="flex-start"
        style={{
          margin: "10px 15px 20px 15px",
          height: "fit-content",
          minHeight: "90vh",
        }}
      >
        <Flex
          gap={20}
          justify="flex-start"
          align="center"
          vertical
          style={{
            width: "30%",
          }}
        >
          <Card
            style={{
              width: "400px",
            }}
          >
            <Flex vertical gap={10}>
              <Title
                level={2}
                style={{ margin: "0px" }}
              >{`${userData.nickname}`}</Title>
              <Title
                level={4}
                style={{
                  margin: "0px",
                }}
              >{`${userData.surname} ${userData.name} ${
                userData.patronymic
              } (${printYears(userData.birthdayDate)})`}</Title>
              <Flex gap={7}>
                <EnvironmentOutlined />
                <Text>{`${userData.citizenship}`}</Text>
              </Flex>
              <Flex gap={7}>
                <PhoneOutlined />
                <Text>{`+${userData.phoneNumber}`}</Text>
              </Flex>
              <Flex gap={7}>
                <CalendarOutlined />
                <Text>{printRegDate(userData.registrationDate)}</Text>
              </Flex>
              <Flex gap={7}>
                <IdcardOutlined style={{ margin: "1px 0px 0px 0px" }} />
                <Text>{`${userData.passportSeries}${userData.passportNumber}`}</Text>
              </Flex>
            </Flex>
          </Card>
          <Flex align="center">
            <Button onClick={() => navigate(-1)}>Назад</Button>
          </Flex>
        </Flex>

        <Flex
          gap={30}
          vertical
          justify="flex-start"
          align="center"
          style={{
            width: "70%",
          }}
        >
          <Table
            dataSource={userData.personalAccounts}
            pagination={{ position: ["none", "none"] }}
            style={{ width: "90%" }}
            title={() => (
              <Title level={4} style={{ margin: "0px 10px" }}>
                Лицевые счета
              </Title>
            )}
          >
            <Column
              title="Название"
              dataIndex="name"
              key="name"
              width="130px"
            />
            <Column
              title="Номер счета"
              dataIndex="number"
              key="number"
              width="330px"
              render={(_, record) => (
                <Text>{convertAccNumber(record.number)}</Text>
              )}
            />
            <Column
              title="Дата создания"
              dataIndex="creationDate"
              key="creationDate"
              width="180px"
              render={(creationDate) => (
                <Text>{convertDatetime(creationDate)}</Text>
              )}
            />
            <Column
              title="Текущий баланс"
              dataIndex="currentBalance"
              key="currentBalance"
              width="170px"
              render={(_, record) => (
                <>
                  <Text>{`${record.currentBalance} `}</Text>
                  <Text>{record.currency.code}</Text>
                </>
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
          <Table
            dataSource={userData.cards}
            style={{ width: "90%" }}
            pagination={{ position: ["none", "none"] }}
            title={() => (
              <Title level={4} style={{ margin: "0px 10px" }}>
                Карты
              </Title>
            )}
          >
            <Column
              title="Название"
              key="name"
              width="130px"
              dataIndex="name"
              render={(_, record) => <Text>{record.name}</Text>}
            />
            <Column
              title="Номер карты"
              key="number"
              width="220px"
              dataIndex="number"
              render={(_, record) => (
                <Text>{convertAccNumber(record.number)}</Text>
              )}
            />
            <Column
              title="Счет"
              dataIndex="personalAccount"
              key="account"
              width="130px"
              render={(personalAccount) => (
                <Flex>
                  <Text>{personalAccount.name}</Text>
                </Flex>
              )}
            />
            <Column
              title="Номер счета"
              key="accNumber"
              width="330px"
              dataIndex="accNumber"
              render={(_, record) => (
                <Text>{convertAccNumber(record.personalAccount.number)}</Text>
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
          <Table
            dataSource={creditsDataTable}
            style={{ width: "90%" }}
            expandable={{
              expandedRowRender: (record) => expandedCreditsTable(record),
            }}
            pagination={{ position: ["none", "none"] }}
            title={() => (
              <Title level={4} style={{ margin: "0px 10px" }}>
                Кредиты
              </Title>
            )}
          >
            <Column
              width="140px"
              title="Название"
              dataIndex="name"
              key="name"
              render={(name, record) => <Text>{name}</Text>}
            />
            <Column
              width="280px"
              title="Номер"
              dataIndex="number"
              key="number"
              render={(_, record) => (
                <Link to={`/moderator/credits/${record.id}`}>
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
              width="130px"
              title="Общая сумма"
              dataIndex="creditStartBalance"
              key="creditStartBalance"
              render={(_, record) => (
                <Text>{`${record.creditStartBalance} ${record.currency.code}`}</Text>
              )}
            />
            <Column
              width="160px"
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
