import axios from "axios";
import { useState } from "react";
import { Flex, Typography, Button, Table, Tag, Divider } from "antd";
import {
  useNavigate,
  useLoaderData,
  redirect,
  useRevalidator,
} from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;
const { Column } = Table;

const getCardPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `CardPackages/GetAllInfo?onlyActive=${false}`
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
      `CreditPackages/GetAllInfo?includeData=${true}&onlyActive=${false}`
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
      `DepositPackages/GetAllInfo?includeData=${true}&onlyActive=${false}`
    );
    return { depositPackagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { depositPackagesData: null, error: err.response };
  }
};

export async function loader() {
  const { cardPackagesData, error } = await getCardPackagesData();
  if (!cardPackagesData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  const { creditPackagesData, error: error1 } = await getCreditPackagesData();
  if (!creditPackagesData) {
    if (error1.status === 401 || error1.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error1.status,
      });
    }
  }
  const { depositPackagesData, error: error2 } = await getDepositPackagesData();
  if (!depositPackagesData) {
    if (error2.status === 401 || error2.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error2.status,
      });
    }
  }
  return { cardPackagesData, creditPackagesData, depositPackagesData };
}

const widthPerc = "85%";

export default function ManagementPage() {
  const { cardPackagesData, creditPackagesData, depositPackagesData } =
    useLoaderData();

  const navigate = useNavigate();
  const revalidator = useRevalidator();

  const handleAddCardPackage = () => {
    navigate("add-card-package");
  };

  const handleEditCardPackage = () => {
    navigate("edit-card-package");
  };

  const handleAddCreditPackage = () => {
    navigate("add-credit-package");
  };

  const handleEditCreditPackage = () => {
    navigate("edit-credit-package");
  };

  const handleAddDepositPackage = () => {
    navigate("add-deposit-package");
  };

  const handleEditDepositPackage = () => {
    navigate("edit-deposit-package");
  };

  const setCardPackageStatus = async (id, status) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `CardPackages/UpdateStatus?cardPackageId=${id}&isActive=${status}`
      );
      if (status === false) {
        showMessageStc("Пакет карт был сделан неактивным", "success");
      } else {
        showMessageStc("Пакет карт был восстановлен", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleDeleteCardPackage = (id) => {
    setCardPackageStatus(id, false);
  };

  const handleRestoreCardPackage = (id) => {
    setCardPackageStatus(id, true);
  };

  const setCreditPackageStatus = async (id, status) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `CreditPackages/UpdateStatus?creditPackageId=${id}&isActive=${status}`
      );
      if (status === false) {
        showMessageStc("Пакет кредитов был сделан неактивным", "success");
      } else {
        showMessageStc("Пакет кредитов был восстановлен", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleDeleteCreditPackage = (id) => {
    setCreditPackageStatus(id, false);
  };

  const handleRestoreCreditPackage = (id) => {
    setCreditPackageStatus(id, true);
  };

  const setDepositPackageStatus = async (id, status) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `DepositPackages/UpdateStatus?depositPackageId=${id}&isActive=${status}`
      );
      if (status === false) {
        showMessageStc("Пакет депозитов был сделан неактивным", "success");
      } else {
        showMessageStc("Пакет депозитов был восстановлен", "success");
      }
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleDeleteDepositPackage = (id) => {
    setDepositPackageStatus(id, false);
  };

  const handleRestoreDepositPackage = (id) => {
    setDepositPackageStatus(id, true);
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
    <>
      <Flex
        align="center"
        justify="flex-start"
        style={{
          minHeight: "80vh",
          height: "fit-content",
        }}
        vertical
      >
        <Flex
          justify="center"
          align="center"
          vertical
          style={{ width: "100%" }}
        >
          <Flex style={{ width: widthPerc, margin: "0px 0px 10px 30px" }}>
            <Title level={2}>Пакеты карт</Title>
          </Flex>
          <Table
            dataSource={cardPackagesData}
            style={{ width: widthPerc }}
            pagination={{ position: ["none", "none"] }}
          >
            <Column
              title="Номер"
              dataIndex="id"
              key="id"
              width="120px"
              render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
            />
            <Column
              title="Название"
              dataIndex="name"
              key="name"
              width="300px"
              render={(name) => <Text>{name}</Text>}
            />
            <Column
              title="Цена"
              dataIndex="price"
              key="price"
              width="170px"
              render={(price) => <Text>{`${price.toString()} BYN`}</Text>}
            />
            <Column
              title="Количество операций"
              dataIndex="operationsNumber"
              key="operationsNumber"
              width="200px"
              render={(operationsNumber) => (
                <Text>{operationsNumber.toString()}</Text>
              )}
            />
            <Column
              title="Сумма операций"
              dataIndex="operationsSum"
              key="operationsSum"
              width="170px"
              render={(operationsSum) => (
                <Text>{`${operationsSum.toString()} BYN`}</Text>
              )}
            />
            <Column
              title="Статус"
              dataIndex="isActive"
              key="status"
              width="130px"
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
            <Column
              title="Действия"
              dataIndex="actions"
              key="actions"
              render={(_, record) => (
                <Flex justify="flex-start">
                  {record.isActive === true ? (
                    <Button
                      type="dashed"
                      danger
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleDeleteCardPackage(record.id)}
                    >
                      Удалить
                    </Button>
                  ) : (
                    <Button
                      type="dashed"
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleRestoreCardPackage(record.id)}
                    >
                      Восстановить
                    </Button>
                  )}
                </Flex>
              )}
            />
          </Table>
          <Flex
            justify="flex-start"
            gap={20}
            style={{ width: widthPerc, margin: "20px 0px 0px 20px" }}
          >
            <Button type="primary" onClick={handleAddCardPackage}>
              Добавить
            </Button>
            <Button onClick={handleEditCardPackage}>Редактировать</Button>
          </Flex>
        </Flex>
        <Divider style={{ margin: "20px 0px 0px 0px" }} />
        <Flex
          justify="center"
          align="center"
          vertical
          style={{ width: "100%" }}
        >
          <Flex style={{ width: widthPerc, margin: "0px 0px 10px 30px" }}>
            <Title level={2}>Пакеты кредитов</Title>
          </Flex>
          <Table
            dataSource={creditPackagesData}
            style={{ width: widthPerc }}
            pagination={{ position: ["none", "none"] }}
          >
            <Column
              title="Номер"
              dataIndex="id"
              key="id"
              width="100px"
              render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
            />
            <Column
              title="Название"
              dataIndex="name"
              key="name"
              width="150px"
              render={(name) => <Text>{name}</Text>}
            />
            <Column
              title="Размер кредита"
              dataIndex="creditGrantedAmount"
              key="creditGrantedAmount"
              width="140px"
              render={(value, record) => (
                <Text>{`${value} ${record.currency.code}`}</Text>
              )}
            />
            <Column
              title="Процентная ставка"
              dataIndex="interestRate"
              key="interestRate"
              width="140px"
              render={(value) => <Text>{`${value}%`}</Text>}
            />
            <Column
              title="Тип выплаты процентов"
              dataIndex="interestCalculationType"
              key="interestCalculationType"
              width="140px"
              render={(_, record) =>
                record.interestCalculationType === "annuity" ? (
                  <Tag color="green">Аннуитетный</Tag>
                ) : (
                  <Tag color="green">Дифференцированный</Tag>
                )
              }
            />
            <Column
              title="Срок выдачи"
              dataIndex="creditTermInDays"
              key="creditTermInDays"
              width="120px"
              render={(value) => <Text>{`${convertMonths(value)} `}</Text>}
            />
            <Column
              title="Досрочное погашение"
              dataIndex="hasPrepaymentOption"
              key="hasPrepaymentOption"
              width="120px"
              render={(_, record) =>
                record.hasPrepaymentOption === true ? (
                  <Tag color="green" style={{ width: "fit-content" }}>
                    Досрочное погашение
                  </Tag>
                ) : (
                  <Tag color="orange" style={{ width: "fit-content" }}>
                    Без досрочного погашения
                  </Tag>
                )
              }
            />
            <Column
              title="Статус"
              dataIndex="isActive"
              key="status"
              width="110px"
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
            <Column
              title="Действия"
              dataIndex="actions"
              key="actions"
              render={(_, record) => (
                <Flex justify="flex-start">
                  {record.isActive === true ? (
                    <Button
                      type="dashed"
                      danger
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleDeleteCreditPackage(record.id)}
                    >
                      Удалить
                    </Button>
                  ) : (
                    <Button
                      type="dashed"
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleRestoreCreditPackage(record.id)}
                    >
                      Восстановить
                    </Button>
                  )}
                </Flex>
              )}
            />
          </Table>
          <Flex
            justify="flex-start"
            gap={20}
            style={{ width: widthPerc, margin: "20px 0px 0px 20px" }}
          >
            <Button type="primary" onClick={handleAddCreditPackage}>
              Добавить
            </Button>
            <Button onClick={handleEditCreditPackage}>Редактировать</Button>
          </Flex>
        </Flex>
        <Divider style={{ margin: "20px 0px 0px 0px" }} />
        <Flex
          justify="center"
          align="center"
          vertical
          style={{ width: "100%" }}
        >
          <Flex style={{ width: widthPerc, margin: "0px 0px 10px 30px" }}>
            <Title level={2}>Пакеты депозитов</Title>
          </Flex>
          <Table
            dataSource={depositPackagesData}
            style={{ width: "90%" }}
            pagination={{ position: ["none", "none"] }}
          >
            <Column
              title="Номер"
              dataIndex="id"
              key="id"
              width="80px"
              render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
            />
            <Column
              title="Название"
              dataIndex="name"
              key="name"
              width="150px"
              render={(name) => <Text>{name}</Text>}
            />
            <Column
              title="Размер депозита"
              dataIndex="depositStartBalance"
              key="depositStartBalance"
              width="160px"
              render={(_, record) => (
                <Text>{`${record.depositStartBalance} ${record.currency.code}`}</Text>
              )}
            />
            <Column
              title="Процентная ставка"
              dataIndex="interestRate"
              key="interestRate"
              width="140px"
              render={(value) => <Text>{`${value}%`}</Text>}
            />
            <Column
              title="Срок выдачи"
              dataIndex="depositTermInDays"
              key="depositTermInDays"
              width="120px"
              render={(value) => <Text>{`${convertMonths(value)} `}</Text>}
            />
            <Column
              title="Отзывной"
              dataIndex="isRevocable"
              key="isRevocable"
              width="120px"
              render={(_, record) =>
                record.isRevocable === true ? (
                  <Tag color="green">Да</Tag>
                ) : (
                  <Tag color="orange">Нет</Tag>
                )
              }
            />
            <Column
              title="Капитализация"
              dataIndex="hasCapitalisation"
              key="hasCapitalisation"
              width="100px"
              render={(_, record) =>
                record.hasCapitalisation === true ? (
                  <Tag color="green">Да</Tag>
                ) : (
                  <Tag color="orange">Нет</Tag>
                )
              }
            />
            <Column
              title="Возможность снятия процентов"
              dataIndex="hasInterestWithdrawalOption"
              key="hasInterestWithdrawalOption"
              width="160px"
              render={(_, record) =>
                record.hasInterestWithdrawalOption === true ? (
                  <Tag color="green">Да</Tag>
                ) : (
                  <Tag color="orange">Нет</Tag>
                )
              }
            />
            <Column
              title="Статус"
              dataIndex="isActive"
              key="status"
              width="110px"
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
            <Column
              title="Действия"
              dataIndex="actions"
              key="actions"
              render={(_, record) => (
                <Flex justify="flex-start">
                  {record.isActive === true ? (
                    <Button
                      type="dashed"
                      danger
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleDeleteDepositPackage(record.id)}
                    >
                      Удалить
                    </Button>
                  ) : (
                    <Button
                      type="dashed"
                      style={{ margin: "0px 0px 0px 0px" }}
                      onClick={() => handleRestoreDepositPackage(record.id)}
                    >
                      Восстановить
                    </Button>
                  )}
                </Flex>
              )}
            />
          </Table>
          <Flex
            justify="flex-start"
            gap={20}
            style={{ width: widthPerc, margin: "20px 0px 20px 20px" }}
          >
            <Button type="primary" onClick={handleAddDepositPackage}>
              Добавить
            </Button>
            <Button onClick={handleEditDepositPackage}>Редактировать</Button>
          </Flex>
        </Flex>
      </Flex>
    </>
  );
}
