import { useLoaderData } from "react-router-dom";

const getModeratorData = async (moderatorId) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `Moderators/GetInfoById?moderatorId=${moderatorId}&includeData=${true}`
    );
    return { moderatorData: res.data.item, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { moderatorData: null, error: err.response };
  }
};

export async function loader({params}) {
  const { moderatorData, error } = await getModeratorData(params.moderatorId);
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

export function ModeratorInfo() {
  const { moderatorData } = useLoaderData();
  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        vertical
        style={{ minHeight: "82vh" }}
      >
        <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
          <Button
            style={{ margin: "18px 0px 0px 20px" }}
            onClick={() => navigate(-1)}
          >
            Назад
          </Button>
          <Title level={3}>Информация о модераторе</Title>
        </Flex>
      </Flex>
    </>
  );
}
