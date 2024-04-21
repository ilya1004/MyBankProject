import axios from "axios";
import { Flex, Typography, Table, Tag } from "antd";
import { Link, useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

const getUsersData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`User/GetAllInfo?includeData=${true}`);
    return { usersData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { usersData: null, error: err.response };
  }
};

export async function loader() {
  const { usersData, error } = await getUsersData();
  if (!usersData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { usersData };
}

export function UsersInfo() {
  const navigate = useNavigate();

  const { usersData } = useLoaderData();

  const convertDatetime = (datetime) => {
    let dt = new Date(datetime);
    let txt = `${dt.toLocaleDateString()} ${dt.toLocaleTimeString()}`;
    return <Text>{txt}</Text>;
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "82vh" }}
      >
        <Flex
          justify="space-between"
          style={{ width: "80%", margin: "0px 0px 10px 0px" }}
        >
          <Title style={{ marginLeft: "10px" }} level={2}>
            Информация о пользователях
          </Title>
        </Flex>
        <Table
          dataSource={usersData}
          style={{ width: "80%" }}
          pagination={{ position: ["none", "none"] }}
        >
          <Column
            title="Номер"
            dataIndex="id"
            key="id"
            width={"100px"}
            render={(id) => <Text>{id.toString().padStart(5, "0")}</Text>}
          />
          <Column
            title="Никнейм"
            dataIndex="nickname"
            key="nickname"
            width={"200px"}
            render={(_, record) => (
              <Link to={`${record.id}`} component={Typography.Link}>
                {record.nickname}
              </Link>
            )}
          />
          <Column
            title="ФИО"
            key="fio"
            width={"320px"}
            dataIndex="fio"
            render={(_, record) => (
              <Text>
                {`${record.surname} ${record.name} ${record.patronymic}`}
              </Text>
            )}
          />
          <Column
            title="Номер телефона"
            key="phoneNumber"
            width={"230px"}
            dataIndex="phoneNumber"
            render={(phoneNumber) => <Text>{`+${phoneNumber}`}</Text>}
          />
          <Column
            title="Дата регистрации"
            key="registrationDate"
            width={"230px"}
            dataIndex="registrationDate"
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
        {/* <Flex justify="space-between" style={{ width: "80%" }}>
          <Button
            style={{ margin: "20px 0px 20px 10px" }}
            type="primary"
            onClick={handleUserInfo}
          >
            Добавить карту
          </Button>
        </Flex> */}
      </Flex>
    </>
  );
}
