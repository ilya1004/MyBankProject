import { Typography, Flex, Card, Table, Tag, Button } from "antd";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";
import { Link, useLoaderData, redirect, useNavigate } from "react-router-dom";

const { Text, Title } = Typography;
const { Column } = Table;

const getModeratorsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Moderators/GetAllInfo?includeData=${false}&onlyActive=${false}`
    );
    return { moderatorsData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorsData: null, error: err.response };
  }
};

export async function loader() {
  const { moderatorsData, error } = await getModeratorsData();
  if (!moderatorsData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { moderatorsData };
}

export default function ModeratorsPage() {
  const navigate = useNavigate();

  const { moderatorsData } = useLoaderData();

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    let txt = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
    return <Text>{txt}</Text>;
  };

  const handleAddModerator = () => {
    navigate("add");
  };

  const handleEditModerator = () => {
    navigate("edit");
  };

  const handleDeleteModerator = () => {
    navigate("delete");
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "82vh" }}
      >
        <Flex style={{ width: "70%", margin: "0px 0px 10px 30px" }}>
          <Title level={2}>Информация о модераторах</Title>
        </Flex>
        <Table
          dataSource={moderatorsData}
          style={{ width: "70%" }}
          pagination={{ position: ["none", "none"] }}
        >
          <Column
            title="Номер"
            dataIndex="id"
            key="id"
            width={"100px"}
            render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
          />
          <Column
            title="Никнейм"
            dataIndex="nickname"
            key="nickname"
            width={"300px"}
            render={(_, record) => (
              <Link to={`${record.id}`} component={Typography.Link}>
                {record.nickname}
              </Link>
            )}
          />
          <Column
            title="Логин"
            dataIndex="login"
            key="login"
            width={"300px"}
            render={(_, record) => <Text>{record.login}</Text>}
          />
          <Column
            title="Дата создания учетной записи"
            key="creationDate"
            width={"520px"}
            dataIndex="creationDate"
            render={(registrationDate) => convertDatetime(registrationDate)}
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
        <Flex
          justify="flex-start"
          gap={20}
          style={{ width: "70%", margin: "20px 0px 0px 20px" }}
        >
          <Button type="primary" onClick={handleAddModerator}>
            Добавить
          </Button>
          <Button onClick={handleEditModerator}>Редактировать</Button>
          <Button onClick={handleDeleteModerator} danger>
            Удалить
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
