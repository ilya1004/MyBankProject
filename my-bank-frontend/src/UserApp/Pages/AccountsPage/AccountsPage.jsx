import axios from "axios";
import { Flex, Typography, Button, Table } from "antd";
import { CheckCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

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
  const { accountsData, error } = await getAccountsData();
  if (!accountsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { accountsData };
}

export default function AccountsPage() {
  const navigate = useNavigate();

  const { accountsData } = useLoaderData();

  const convertAccNumber = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const handleAddAccount = () => {
    navigate("create");
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "80vh", height: "fit-content" }}
      >
        <Flex justify="space-between" style={{ width: "70%" }}>
          <Title style={{ marginLeft: "10px" }} level={2}>
            Мои счета
          </Title>
        </Flex>
        <Table
          dataSource={accountsData}
          style={{ width: "70%" }}
          pagination={{ position: ["none", "none"] }}
        >
          <Column
            title="Основной"
            dataIndex="isForTransfersByNickname"
            key="isForTransfersByNickname"
            width="100px"
            render={(state) => (
              <Flex align="center" justify="center">
                {state === true ? (
                  <CheckCircleTwoTone
                    twoToneColor="#52c41a"
                    style={{ fontSize: "20px" }}
                  />
                ) : null}
              </Flex>
            )}
          />
          <Column title="Название" dataIndex="name" key="name" width="150px" />
          <Column
            title="Номер"
            dataIndex="number"
            key="number"
            width="500px"
            render={(_, record) => (
              <Link to={`${record.id}`} component={Typography.Link}>
                {convertAccNumber(record.number)}
              </Link>
            )}
          />
          <Column
            title="Текущий баланс"
            dataIndex="currentBalance"
            key="currentBalance"
            render={(_, record) => (
              <>
                <Text>{`${record.currentBalance} `}</Text>
                <Text>{record.currency.code}</Text>
              </>
            )}
          />
        </Table>
        <Flex justify="space-between" style={{ width: "70%" }}>
          <Button
            style={{ margin: "20px 0px 20px 10px" }}
            type="primary"
            onClick={handleAddAccount}
          >
            Открыть счёт
          </Button>
        </Flex>
      </Flex>
    </>
  );
}
