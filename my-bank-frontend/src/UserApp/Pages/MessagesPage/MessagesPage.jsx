import {
  Typography,
  Flex,
  Table,
  Tag,
  Button,
  Card,
  Col,
  Row,
  Input,
  Select,
  Popover,
} from "antd";
import {
  CheckOutlined,
  ArrowRightOutlined,
  QuestionCircleOutlined,
} from "@ant-design/icons";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";
import {
  Link,
  useLoaderData,
  redirect,
  useNavigate,
  useRevalidator,
} from "react-router-dom";
import { useState } from "react";

const { Text, Title } = Typography;
const { Column } = Table;
const { TextArea } = Input;

const getMessagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`Messages/GetAllInfoByCurrentUser`);
    return { messagesData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { messagesData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { messagesData: null, error: err.response };
  }
};

export async function loader() {
  const compare = (a, b) => {
    if (a.id > b.id) return 1;
    if (a.id == b.id) return 0;
    if (a.id < b.id) return -1;
  };
  const { messagesData, error } = await getMessagesData();
  if (!messagesData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  messagesData.sort(compare);
  return { messagesData };
}

const colTextWidth = "170px";
const colInputWidth = "250px";

export default function MessagesPage() {
  const [isWritingMessage, setIsWritingMessage] = useState(false);
  const [title, setTitle] = useState("");
  const [text, setText] = useState("");
  const [role, setRole] = useState("");

  const navigate = useNavigate();
  const revalidator = useRevalidator();

  const { messagesData, selectUsers } = useLoaderData();

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    let txt = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
    return <Text>{txt}</Text>;
  };

  const handleAddMessage = () => {
    isWritingMessage === false
      ? setIsWritingMessage(true)
      : setIsWritingMessage(false);
  };

  const printSender = (record) => {
    let txt = null;
    if (record.recepientRole === "admin") {
      txt = (
        <>
          <Flex gap={5}>
            <Text style={{ margin: "0px 5px 0px 0px" }}>Вы</Text>
            <ArrowRightOutlined />
            <Tag
              color="red"
              style={{ margin: "0px 0px 0px 5px", maxHeight: "22px" }}
            >
              Администратор
            </Tag>
          </Flex>
        </>
      );
    } else if (record.recepientRole === "moderator") {
      txt = (
        <>
          <Flex gap={5}>
            <Text style={{ margin: "0px 0px 0px 5px" }}>Вы</Text>
            <ArrowRightOutlined />
            <Tag
              color="volcano"
              style={{ margin: "0px 0px 0px 5px", maxHeight: "22px" }}
            >
              Модератор
            </Tag>
          </Flex>
        </>
      );
    } else if (record.recepientRole === "user") {
      if (record.senderAdminId !== null) {
        txt = (
          <>
            <Flex gap={5}>
              <Tag style={{ maxHeight: "22px" }} color="red">
                Администратор
              </Tag>
              <ArrowRightOutlined />
              <Text style={{ margin: "0px 0px 0px 5px" }}>Вы</Text>
            </Flex>
          </>
        );
      } else if (record.senderModeratorId !== null) {
        txt = (
          <>
            <Flex gap={5}>
              <Tag style={{ maxHeight: "22px" }} color="orange">
                Модератор
              </Tag>
              <ArrowRightOutlined />
              <Text style={{ margin: "0px 0px 0px 0px" }}>Вы</Text>
            </Flex>
          </>
        );
      }
    } else {
      txt = (
        <>
          <Flex gap={5}>
            <Text>Нет данных</Text>
          </Flex>
        </>
      );
    }

    return txt;
  };

  const setReaded = async (id, state) => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      await axiosInstance.put(
        `Messages/UpdateIsRead?messageId=${id}&isRead=${state}`
      );
      showMessageStc("Сообщение было помечено как прочитанное", "success");
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleSetReaded = (id) => {
    setReaded(id, true);
  };

  const handleCancelMess = () => {
    setIsWritingMessage(false);
  };

  const sendMessage = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const data = {
        id: 0,
        title: title.trim(),
        text: text.trim(),
        recepientId: -1,
        recepientNickname: "",
        recepientRole: role,
      };
      await axiosInstance.post(`Messages/Add`, data);
      showMessageStc("Сообщение было успешно отправлено", "success");
      setIsWritingMessage(false);
      revalidator.revalidate();
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleEnterMess = () => {
    setText("");
    setTitle("");
    setRole("");
    sendMessage();
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex style={{ width: "85%", margin: "0px 0px 10px 30px" }}>
          <Title level={2}>Мои сообщения</Title>
        </Flex>
        <Table
          dataSource={messagesData}
          style={{ width: "85%" }}
          pagination={{ position: ["none", "none"] }}
        >
          <Column
            title="Номер"
            dataIndex="id"
            key="id"
            width="90px"
            render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
          />
          <Column
            title="Тема"
            dataIndex="title"
            key="title"
            width="170px"
            render={(title) => <Text>{title}</Text>}
          />
          <Column
            title="Текст сообщения"
            dataIndex="text"
            key="text"
            width="450px"
            render={(text) => <Text>{text}</Text>}
          />
          <Column
            title="Дата отправления"
            key="creationDatetime"
            dataIndex="creationDatetime"
            width="160px"
            render={(creationDatetime) => convertDatetime(creationDatetime)}
          />
          <Column
            title={
              <>
                <Text>Отправитель</Text>
                <ArrowRightOutlined style={{ margin: "0px 7px" }} />
                <Text>Получатель</Text>
              </>
            }
            width="250px"
            dataIndex="sender"
            key="sender"
            render={(_, record) => printSender(record)}
          />
          <Column
            title="Статус"
            dataIndex="isRead"
            key="isRead"
            render={(_, record) => (
              <Flex justify="flex-start">
                {record.recepientRole === "user" ? (
                  record.isRead === true ? (
                    <Tag color="green">Прочитано</Tag>
                  ) : (
                    <Button
                      style={{ margin: "0px 0px 0px 20px", height: "30px" }}
                      onClick={() => handleSetReaded(record.id)}
                      icon={<CheckOutlined />}
                    ></Button>
                  )
                ) : null}
              </Flex>
            )}
          />
        </Table>
        <Flex
          justify="flex-start"
          style={{ width: "85%", margin: "20px 0px 0px 20px" }}
        >
          <Flex justify="flex-start" style={{ width: "30%" }}>
            <Button type="primary" onClick={handleAddMessage}>
              Новое сообщение
            </Button>
          </Flex>
          <Flex>
            {isWritingMessage === true ? (
              <Card
                title={<Text>Новое сообщение</Text>}
                style={{ width: "550px" }}
              >
                <Flex
                  vertical
                  gap={16}
                  style={{ width: "100%" }}
                  align="center"
                >
                  <Row gutter={[16, 16]}>
                    <Col style={{ width: colTextWidth }}>
                      <Text style={{ fontSize: "16px" }}>Тема:</Text>
                    </Col>
                    <Col style={{ width: colInputWidth }}>
                      <Input
                        onChange={(e) => setTitle(e.target.value)}
                        value={title}
                        style={{ width: "250px" }}
                        count={{
                          max: 50,
                          exceedFormatter: (txt, { max }) => txt.slice(0, max),
                        }}
                      />
                    </Col>
                  </Row>
                  <Row gutter={[16, 16]}>
                    <Col style={{ width: colTextWidth }}>
                      <Text style={{ fontSize: "16px" }}>Текст:</Text>
                    </Col>
                    <Col style={{ width: colInputWidth }}>
                      <TextArea
                        value={text}
                        style={{ minWidth: "250px" }}
                        onChange={(e) => setText(e.target.value)}
                        maxLength={200}
                        autoSize={{
                          minRows: 3,
                          maxRows: 6,
                        }}
                      />
                    </Col>
                  </Row>
                  <Row gutter={[16, 16]}>
                    <Col style={{ width: colTextWidth }}>
                      <Text style={{ fontSize: "16px" }}>{"Получатель "}</Text>
                      <Popover
                        content={
                          <Flex vertical align="center">
                            <Text>Выберите получателя</Text>
                            <Text>для отправки сообщения</Text>
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
                        style={{ width: "170px" }}
                        onChange={(role) => setRole(role)}
                        options={[
                          {
                            value: "admin",
                            label: "Администратор",
                          },
                          {
                            value: "moderator",
                            label: "Модератор",
                          },
                        ]}
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
                  <Button onClick={handleCancelMess}>Отмена</Button>
                  <Button type="primary" onClick={handleEnterMess}>
                    Отправить
                  </Button>
                </Flex>
              </Card>
            ) : null}
          </Flex>
        </Flex>
      </Flex>
    </>
  );
}
