import { Typography, Flex, Card, Image } from "antd";
import {
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
} from "@ant-design/icons";
import { useLoaderData, redirect } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";
import AdminAvatar from "../../Assets/admin.jpg";

const { Text, Title } = Typography;

const getAdminData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Admin/GetInfoCurrent?includeData=${true}`
    );
    return { adminData: res.data.item, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { adminData: null, error: err.response };
  }
};

export async function loader() {
  const { adminData, error } = await getAdminData();
  if (!adminData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { adminData };
}

export function ProfilePage() {
  const { adminData } = useLoaderData();

  return (
    <>
      <Flex
        gap={30}
        justify="flex-start"
        align="center"
        vertical
        style={{
          width: "98%",
          minHeight: "80vh",
          height: "fit-content"
        }}
      >
        <Image
          height={"300px"}
          width={"300px"}
          src={AdminAvatar}
          preview={false}
          style={{ borderRadius: "10px", margin: "10px 0px 10px 0px" }}
        />
        <Card
          style={{
            width: "300px",
          }}
        >
          <Flex vertical gap={5}>
          <Title
              level={2}
              style={{ margin: "0px 0px 15px 0px" }}
            >Администратор</Title>
            <Flex gap={7}>
              <MailOutlined
                style={{ margin: "4px 0px 0px 0px", fontSize: "16px" }}
              />
              <Text style={{ fontSize: "16px" }}>{adminData.login}</Text>
            </Flex>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
