import { Typography, Flex, Card, Image } from "antd";
import {
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
} from "@ant-design/icons";
import { useLoaderData } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../Common/Store/constants";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";
import { redirect } from "react-router-dom";
import ModerAvatar from "../../Assets/moder.png";

const { Text, Title } = Typography;

const getModeratorData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Moderators/GetInfoCurrent?includeData=${true}`
    );
    console.log(res.data["item"]);
    return { moderatorData: res.data["item"], error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorData: null, error: err.response };
  }
};

export async function loader() {
  const { moderatorData, error } = await getModeratorData();
  if (!moderatorData) {
    if (error.status === 401) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { moderatorData };
}

export function ProfilePage() {
  const { moderatorData } = useLoaderData();

  const printRegDate = (date) => {
    let dateObj = new Date(date);
    return `Модератор с ${dateObj.toLocaleDateString()}`;
  };

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
        }}
      >
        <Image
          height="300px"
          width="300px"
          src={ModerAvatar}
          preview={false}
          style={{ borderRadius: "10px", margin: "10px 0px 0px 0px" }}
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
            >{`${moderatorData.nickname}`}</Title>
            <Flex gap={7}>
              <CalendarOutlined />
              <Text>{printRegDate(moderatorData.creationDate)}</Text>
            </Flex>
            <Flex gap={7}>
              <MailOutlined style={{ margin: "4px 0px 0px 0px" }} />
              <Text>{moderatorData.login}</Text>
            </Flex>
          </Flex>
        </Card>
      </Flex>
    </>
  );
}
