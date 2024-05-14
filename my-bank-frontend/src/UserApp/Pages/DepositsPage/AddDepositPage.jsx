import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Input,
  Select,
  Tag,
} from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { useState } from "react";
import { useLoaderData, Link, useNavigate, redirect } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Text, Title } = Typography;

const colLabelWidth = "320px";
const colTextWidth = "200px";

const getDepositPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `DepositPackages/GetAllInfo?includeData=${true}`
    );
    return { packagesData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { packagesData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { packagesData: null, error: err.response };
  }
};

const getAccountsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `PersonalAccounts/GetAllInfoByCurrentUser?includeData=${true}`
    );
    return { accountsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { accountsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { accountsData: null, error: err.response };
  }
};

export async function loader() {
  const { packagesData, error } = await getDepositPackagesData();
  if (!packagesData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  const { accountsData, error: error1 } = await getAccountsData();
  if (!accountsData) {
    if (error1.status === 401 || error1.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error1.status,
      });
    }
  }
  const selectPackagesData = [];
  for (let i = 0; i < packagesData.length; i++) {
    selectPackagesData.push({
      value: packagesData[i].id,
      label: packagesData[i].name,
    });
  }
  const selectAccountsData = [];
  for (let i = 0; i < accountsData.length; i++) {
    selectAccountsData.push({
      value: accountsData[i].id,
      label: accountsData[i].name,
    });
  }
  return { packagesData, selectPackagesData, accountsData, selectAccountsData };
}

export default function AddDepositPage() {
  const [packageId, setPackageId] = useState(-1);
  const [name, setName] = useState("");
  const [startBalance, setStartBalance] = useState("");
  const [interestRate, setInterestRate] = useState("");
  const [termInDays, setTermInDays] = useState("");
  const [isRevocable, setIsRevocable] = useState("");
  const [hasCapitalisation, setHasCapitalisation] = useState("");
  const [interestWithdrawalOption, setInterestWithdrawalOption] = useState("");
  const [depositcurrencyCode, setDepositCurrencyCode] = useState("");
  const [currencyId, setCurrencyId] = useState("");

  const [accountId, setAccountId] = useState(-1);
  const [currentBalance, setCurrentBalance] = useState("");
  const [accountCurrencyCode, setAccountCurrencyCode] = useState("");

  const [enterDisabled, setEnterDisabled] = useState(false);

  const { packagesData, selectPackagesData, accountsData, selectAccountsData } =
    useLoaderData();

  const navigate = useNavigate();

  const handleSelectPackage = (packageId) => {
    setPackageId(packageId);
    let item = packagesData.find((item) => item.id === packageId);
    setName(item.name);
    setStartBalance(item.depositStartBalance);
    setInterestRate(item.interestRate);
    setTermInDays(item.depositTermInDays);
    setIsRevocable(item.isRevocable);
    setHasCapitalisation(item.hasCapitalisation);
    setInterestWithdrawalOption(item.hasInterestWithdrawalOption);
    setDepositCurrencyCode(item.currency.code);
    setCurrencyId(item.currencyId);
    if (startBalance > currentBalance) {
      setEnterDisabled(true);
    } else {
      setEnterDisabled(false);
    }
  };

  const handleSelectAccount = (accountId) => {
    setAccountId(accountId);
    let item = accountsData.find((item) => item.id == accountId);
    setCurrentBalance(item.currentBalance);
    setAccountCurrencyCode(item.currency.code);
    if (startBalance > item.currentBalance) {
      setEnterDisabled(true);
    } else {
      setEnterDisabled(false);
    }
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

  const addDeposit = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      name: `Мой ${name}`,
      depositStartBalance: startBalance,
      interestRate: interestRate,
      depositTermInDays: termInDays,
      isRevocable: isRevocable,
      hasCapitalisation: hasCapitalisation,
      hasInterestWithdrawalOption: interestWithdrawalOption,
      currencyId: currencyId,
      personalAccountId: accountId,
    };
    try {
      const res = await axiosInstance.post(`DepositAccounts/Add`, data);
      showMessageStc("Депозит был успешно оформлен", "success");
      navigate(-1);
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnter = () => {
    if (startBalance > currentBalance) {
      showMessageStc("Да выбранном счете недостаточно средств", "error");
      return;
    }
    addDeposit();
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <>
      <Flex
        vertical
        align="center"
        justify="flex-start"
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Link to="/credits">
            <Button style={{ margin: "18px 0px 0px 20px" }}>Назад</Button>
          </Link>
          <Title level={3}>Оформление депозита</Title>
        </Flex>
        <Card
          title="Выберите депозит"
          style={{
            width: "650px",
            // height: "400px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text style={{ fontSize: "16px" }}>Пакет депозита:</Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Select
                  defaultValue=""
                  style={{ width: "200px" }}
                  onChange={handleSelectPackage}
                  options={selectPackagesData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Название депозита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{packageId !== -1 ? `Мой ${name}` : null}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Размер депозита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{`${startBalance} ${depositcurrencyCode}`}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Процентная ставка:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>{packageId !== -1 ? `${interestRate} %` : null}</Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Срок выдачи депозита:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>
                  {packageId !== -1 ? convertMonths(termInDays) : null}
                </Text>
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Отзывной:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                {packageId !== -1 ? (
                  isRevocable === true ? (
                    <CheckCircleTwoTone
                      twoToneColor="#52c41a"
                      style={{ fontSize: "18px" }}
                    />
                  ) : (
                    <CloseCircleTwoTone
                      twoToneColor="red"
                      style={{ fontSize: "18px" }}
                    />
                  )
                ) : null}
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  С капитализацией:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                {packageId !== -1 ? (
                  hasCapitalisation === true ? (
                    <CheckCircleTwoTone
                      twoToneColor="#52c41a"
                      style={{ fontSize: "18px" }}
                    />
                  ) : (
                    <CloseCircleTwoTone
                      twoToneColor="red"
                      style={{ fontSize: "18px" }}
                    />
                  )
                ) : null}
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Возможность снятия процентов:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                {packageId !== -1 ? (
                  interestWithdrawalOption === true ? (
                    <CheckCircleTwoTone
                      twoToneColor="#52c41a"
                      style={{ fontSize: "18px" }}
                    />
                  ) : (
                    <CloseCircleTwoTone
                      twoToneColor="red"
                      style={{ fontSize: "18px" }}
                    />
                  )
                ) : null}
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text style={{ fontSize: "16px" }}>
                  Счет для снятия средств:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Select
                  defaultValue=""
                  style={{ width: "200px" }}
                  onChange={handleSelectAccount}
                  options={selectAccountsData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colLabelWidth }}>
                <Text type="secondary" style={{ fontSize: "15px" }}>
                  Остаток на счете:
                </Text>
              </Col>
              <Col style={{ width: colTextWidth }}>
                <Text>
                  {accountId !== -1
                    ? `${currentBalance} ${accountCurrencyCode}`
                    : null}
                </Text>
              </Col>
            </Row>
          </Flex>
          <Flex
            gap={20}
            align="center"
            justify="center"
            style={{
              margin: "30px 0px 0px 0px",
            }}
          >
            <Button onClick={handleCancel}>Отмена</Button>
            <Button
              disabled={enterDisabled}
              type="primary"
              onClick={handleEnter}
            >
              Оформить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
