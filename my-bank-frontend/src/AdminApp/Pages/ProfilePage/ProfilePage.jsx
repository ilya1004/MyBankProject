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
import AdminAvatar from "../../Assets/admin.jpg"

const { Text } = Typography;

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
        gap={20}
        justify="flex-start"
        align="center"
        vertical
        style={{
          width: "35%",
          minHeight: "91vh",
        }}
      >
        <Image
          height={"200px"}
          width={"200px"}
          src={AdminAvatar}
          preview={false}
        />
        <Card
          style={{
            width: "300px",
          }}
        >
          <Flex vertical gap={5}>
            <Flex gap={7}>
              <MailOutlined style={{ margin: "4px 0px 0px 0px" }} />
              <Text>{adminData.login}</Text>
            </Flex>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
