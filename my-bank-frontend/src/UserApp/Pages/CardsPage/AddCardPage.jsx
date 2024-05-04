import {
  Typography,
  Button,
  Flex,
  Card,
  Col,
  Row,
  Input,
  Select,
  Popover,
} from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";
import { useState } from "react";
import { useLoaderData, Link, useNavigate } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";;

const { Text, Title } = Typography;

const colTextWidth = "200px";
const colInputWidth = "200px";

const getCardPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`CardPackages/GetAllInfo`);
    return res.data.list;
  } catch (err) {
    handleResponseError(err.response);
  }
};

const getUserAccountsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `PersonalAccounts/GetAllInfoByCurrentUser?includeData=${false}`
    );
    console.log(res.data.list);
    return res.data["list"];
  } catch (err) {
    handleResponseError(err.response);
  }
};

export async function loader() {
  const cardPackagesRaw = await getCardPackagesData();
  const cardPackagesData = [];
  for (let i = 0; i < cardPackagesRaw.length; i++) {
    cardPackagesData.push({
      value: cardPackagesRaw[i].id,
      label: cardPackagesRaw[i].name,
    });
  }
  const userAccountsRaw = await getUserAccountsData();
  let userAccountsData = [];
  for (let i = 0; i < userAccountsRaw.length; i++) {
    userAccountsData.push({
      value: userAccountsRaw[i].id,
      label: userAccountsRaw[i].name,
    });
  }
  return { cardPackagesData, userAccountsData };
}

const listDurations = [
  {
    value: 2,
    label: "2 года",
  },
  {
    value: 3,
    label: "3 года",
  },
  {
    value: 4,
    label: "4 года",
  },
  {
    value: 5,
    label: "5 лет",
  },
];

export default function AddCardPage() {
  const [cardName, setCardName] = useState("");
  const [pincode, setPincode] = useState("");
  const [durationInYears, setDurationInYears] = useState(3);
  const [cardPackageId, setCardPackageId] = useState();
  const [personalAccountId, setPersonalAccountId] = useState();
  const navigate = useNavigate();

  const { cardPackagesData, userAccountsData } = useLoaderData();

  const handleCardName = (e) => {
    setCardName(e.target.value);
  };

  function isNumber(value) {
    return /^\d+$/.test(value);
  }

  const handlePincode = (e) => {
    if (!isNumber(e.target.value.at(-1)) && e.target.value.length !== 0) {
      return;
    }
    setPincode(e.target.value);
  };

  const handleDuration = (e) => {
    setDurationInYears(e);
  };

  const handleCardPackage = (e) => {
    setCardPackageId(e);
  };

  const handlePersonalAccount = (e) => {
    setPersonalAccountId(e);
  };

  const handleCancel = () => {
    navigate("/cards");
  };

  const handleEnter = () => {
    if (pincode.length !== 4) {
      showMessageStc("Пин-код должен иметь длину 4 цифры")
      return;
    }
    addCard();
  };

  const addCard = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      name: cardName,
      pincode: pincode,
      durationInYears: durationInYears,
      cardPackageId: cardPackageId,
      personalAccountId: personalAccountId,
    };
    try {
      const res = await axiosInstance.post(`Cards/Add`, data);
      console.log(res.data["status"]);
      showMessageStc("Карта была успешно добавлена", "success");
      navigate("/cards");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  return (
    <>
      <Flex
        vertical
        align="center"
        justify="flex-start"
        style={{
          minHeight: "90vh",
        }}
      >
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Link to="/cards">
            <Button style={{ margin: "18px 0px 0px 20px" }}>Назад</Button>
          </Link>
          <Title level={3}>Оформление карты</Title>
        </Flex>
        <Card
          title="Введите данные"
          style={{
            width: "550px",
            height: "400px",
          }}
        >
          <Flex vertical gap={16} style={{ width: "100%" }} align="center">
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Название:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  onChange={handleCardName}
                  value={cardName}
                  style={{ width: "210px" }}
                  count={{
                    show: true,
                    max: 20,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Пин-код:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input
                  style={{ width: "130px" }}
                  onChange={handlePincode}
                  value={pincode}
                  count={{
                    show: true,
                    max: 4,
                    exceedFormatter: (txt, { max }) => txt.slice(0, max),
                  }}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Срок действия:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  defaultValue="3 года"
                  style={{ width: "130px" }}
                  onChange={handleDuration}
                  options={listDurations}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Пакет услуг:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  defaultValue=""
                  style={{ minWidth: "170px" }}
                  onChange={handleCardPackage}
                  options={cardPackagesData}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>{"Лицевой счет "}</Text>
                <Popover
                  content={
                    <Flex vertical>
                      <Text>Эта карта будет привязана к</Text>
                      <Text>выбранному лицевому счету</Text>
                    </Flex>
                  }
                >
                  <QuestionCircleOutlined
                    style={{ opacity: "0.5" }}
                    twoToneColor={"#000000"}
                  />
                </Popover>
                <Text style={{ fontSize: "16px" }}>:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Select
                  defaultValue=""
                  style={{ minWidth: "170px" }}
                  onChange={handlePersonalAccount}
                  options={userAccountsData}
                />
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
            <Button type="primary" onClick={handleEnter}>
              Добавить
            </Button>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
