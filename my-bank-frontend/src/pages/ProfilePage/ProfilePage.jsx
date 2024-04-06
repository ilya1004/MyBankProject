import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Image, List, Typography, Button } from "antd";
import {
  EnvironmentOutlined,
  PhoneOutlined,
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
} from "@ant-design/icons";
import { Link } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import Avatar from "../../assets/avatar-sq.jpg";
import ListCards from "./Components/ListCards";
import ListAccounts from "./Components/ListAccounts";
import ListCredits from "./Components/ListCredits";
import ListDeposits from "./Components/ListDeposits";
import { FOCUSABLE_SELECTOR } from "@testing-library/user-event/dist/utils";

const { Title, Text, Paragraph } = Typography;

const imageSize = "300px";
export const widthCardAcc = "300px";
export const heightCardAcc = "145px";

export default function ProfilePage() {
  const [userData, setUserData] = useState({});
  const { role, id } = useAuth();
  const [isEditing, setIsEditing] = useState(false);

  // console.log(isEditing);

  let qwe = false;

  useEffect(() => {
    // if (userData == {}) {
    const fetchData = async () => {
      const BASE_URL = `https://localhost:7050/api/`;
      const axiosInstance = axios.create({
        baseURL: BASE_URL,
        withCredentials: true,
      });
      axiosInstance
        .get(`User/GetInfoById/?userId=${1}&includeData=${true}`)
        .then((response) => {
          console.log(response.data["item"]);
          setUserData(response.data["item"]);
          // setPackagesData(response.data["list"]);
          // setItems(response.data["list"]);
        })
        .catch((err) => {
          console.error(err);
        });
    };
    fetchData();
    // }
  }, []);

  const handleEditProfile = () => {};

  const printRegDate = (date) => {
    let dateObj = new Date(date);
    return `Клиент банка с ${dateObj.toLocaleDateString()}`;
  };

  return (
    <div>
      <Flex
        justify="center"
        align="flex-start"
        style={{
          margin: "10px 15px",
          // height: "690px",
          height: "100vh",
        }}
      >
        <Flex
          gap={20}
          justify="flex-start"
          align="center"
          vertical
          style={{
            width: "35%",
          }}
        >
          <Image
            height={imageSize}
            width={imageSize}
            src={Avatar}
            preview={false}
          />
          <Flex gap={15}>
            <Button onClick={handleEditProfile}>Редактировать профиль</Button>
          </Flex>
          <Card
            style={{
              width: "400px",
            }}
          >
            <Flex vertical gap={5}>
              <Flex gap={7}>
                <CalendarOutlined />
                <Text>{printRegDate(userData["registrationDate"])}</Text>
              </Flex>
              <Flex gap={7}>
                <IdcardOutlined style={{ margin: "1px 0px 0px 0px" }} />
                <Text>{`${userData["passportSeries"]}${userData["passportNumber"]}`}</Text>
              </Flex>
              <Flex gap={7}>
                <MailOutlined style={{ margin: "4px 0px 0px 0px" }} />
                <Text>{userData["email"]}</Text>
              </Flex>
            </Flex>
          </Card>
        </Flex>
        <Flex
          gap={20}
          vertical
          justify="flex-start"
          align="center"
          style={{
            width: "65%",
          }}
        >
          <Card
            // type="inner"
            style={{
              width: "70%",
              padding: "0px",
            }}
          >
            <Flex vertical gap={8} align="flex-start">
              <Title
                level={2}
                style={{ margin: "0px 0px 0px 0px" }}
              >{`${userData["nickname"]}`}</Title>
              <Title
                level={4}
                style={{
                  margin: "0px",
                }}
              >{`${userData["surname"]} ${userData["name"]} ${userData["patronymic"]}`}</Title>
              <Flex gap={5}>
                <EnvironmentOutlined />
                <Text>{` ${userData["citizenship"]}`}</Text>
              </Flex>
              <Flex gap={5}>
                <PhoneOutlined />
                <Text>{` ${userData["phoneNumber"]}`}</Text>
              </Flex>
            </Flex>
          </Card>
          {isEditing === true ? (
            <>
              <ListCards value={userData["cards"]} />
              <ListAccounts value={userData["personalAccounts"]} />
              <ListCredits value={userData["creditAccounts"]} />
              <ListDeposits value={userData["depositAccount"]} />
            </>
          ) : (
            <>
              
            </>
          )}
        </Flex>
      </Flex>
    </div>
  );
}
