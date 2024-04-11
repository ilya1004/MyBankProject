import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Table, Tag } from "antd";
import { CheckCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData } from "react-router-dom";
import BASE_URL from "../../store/constants";

const { Column } = Table;
const { Title, Text } = Typography;

const getCardsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  axiosInstance
    .get(`Cards/GetAllInfoByCurrentUser/?includeData=${true}`)
    .then((response) => {
      console.log(response.data["list"]);
      // setCardsData(response.data["list"]);
      // cardsData1 = 
      return response.data["list"];
    })
    .catch((err) => {
      console.error(err);
    });
};

export async function cardsLoader() {
  const cards = await getCardsData();
  return { cards };
}

export default function MainPage() {
  const [cardsData, setCardsData] = useState([]);

  const {cardsData1} = useLoaderData();
  setCardsData(cardsData1)
  

  // useEffect(() => {
  //   setCardsData();
  // }, [])

  // useEffect(() => {
  //   const getCardsData = async () => {
  //     const axiosInstance = axios.create({
  //       baseURL: BASE_URL,
  //       withCredentials: true,
  //     });
  //     axiosInstance
  //       .get(`Cards/GetAllInfoByCurrentUser/?includeData=${true}`)
  //       .then((response) => {
  //         console.log(response.data["list"]);
  //         setCardsData(response.data["list"]);
  //       })
  //       .catch((err) => {
  //         console.error(err);
  //       });
  //   };
  //   getCardsData();
  // }, []);

  return (
    <>
      <Flex align="center" justify="flex-start" vertical>
        <Title level={2}>Мои карты</Title>
        <Table dataSource={cardsData} style={{ width: "80%" }}>
          <Column
            title="Основная"
            dataIndex="personalAccount"
            key="personalAccount"
            width={"100px"}
            render={(personalAccount) =>
              personalAccount.isForTransfersByNickname === true ? (
                <Flex align="center" justify="center">
                  <CheckCircleTwoTone
                    twoToneColor="#52c41a"
                    style={{ fontSize: "20px" }}
                  />
                </Flex>
              ) : null
            }
          />
          <Column
            title="Карта"
            key="name"
            width={"450px"}
            dataIndex="name"
            render={(_, record) => (
              <Flex gap={30}>
                <Text>{record.name}</Text>
                <Link to={`${record.id}`}>{record.number}</Link>
              </Flex>
            )}
          />
          <Column
            title="Счет"
            dataIndex="personalAccount"
            key="account"
            width={"450px"}
            render={(personalAccount) => (
              <Flex gap={30}>
                <Text>{personalAccount.name}</Text>
                <Link to={"/accounts"}>{personalAccount.number}</Link>
              </Flex>
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
              ) : null
            }
          />
        </Table>
      </Flex>
    </>
  );
}
