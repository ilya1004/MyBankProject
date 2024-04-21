import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Table, Tag } from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData, redirect, useNavigate } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

const getCardsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Cards/GetAllInfoByCurrentUser?includeData=${true}`
    );
    return { cardsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { cardsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { cardsData: null, error: err.response };
  }
};

export async function loader() {
  const { cardsData, error } = await getCardsData();
  if (!cardsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { cardsData };
}

export default function CardsPage() {
  const navigate = useNavigate();

  const { cardsData } = useLoaderData();

  const convertCardAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const handleAddCard = () => {
    navigate("create");
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "82vh" }}
      >
        <Flex justify="space-between" style={{ width: "80%" }}>
          <Title style={{ marginLeft: "10px" }} level={2}>
            Мои карты
          </Title>
        </Flex>
        <Table
          dataSource={cardsData}
          style={{ width: "80%" }}
          pagination={{ position: ["none", "none"] }}
        >
          <Column
            title="Основная"
            dataIndex="personalAccount"
            key="personalAccount"
            width={"100px"}
            render={(personalAccount) => (
              <Flex align="center" justify="center">
                {personalAccount.isForTransfersByNickname === true ? (
                  <CheckCircleTwoTone
                    twoToneColor="#52c41a"
                    style={{ fontSize: "20px" }}
                  />
                ) : (
                  <CloseCircleTwoTone
                    twoToneColor="red"
                    style={{ fontSize: "20px" }}
                  />
                )}
              </Flex>
            )}
          />
          <Column
            title="Карта"
            key="name"
            width={"130px"}
            dataIndex="name"
            render={(_, record) => <Text>{record.name}</Text>}
          />
          <Column
            title="Номер карты"
            key="number"
            width={"320px"}
            dataIndex="number"
            render={(_, record) => (
              <Link to={`/cards/${record.id}`} component={Typography.Link}>
                {convertCardAccNumber(record.number)}
              </Link>
            )}
          />
          <Column
            title="Счет"
            dataIndex="personalAccount"
            key="account"
            width={"130px"}
            render={(personalAccount) => (
              <Flex gap={30}>
                <Text>{personalAccount.name}</Text>
              </Flex>
            )}
          />
          <Column
            title="Номер счета"
            key="accNumber"
            width={"320px"}
            dataIndex="accNumber"
            render={(_, record) => (
              <Link
                to={`/accounts/${record.personalAccount.id}`}
                component={Typography.Link}
              >
                {convertCardAccNumber(record.personalAccount.number)}
              </Link>
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
        <Flex justify="space-between" style={{ width: "80%" }}>
          <Button
            style={{ margin: "20px 0px 20px 10px" }}
            type="primary"
            onClick={handleAddCard}
          >
            Добавить карту
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
