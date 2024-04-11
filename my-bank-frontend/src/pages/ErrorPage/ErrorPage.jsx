import { useRouteError, Link } from "react-router-dom";
import { Typography, Flex, Button, Result } from "antd";

const { Text, Title } = Typography;

export default function ErrorPage() {
  const error = useRouteError();
  console.log(error);

  return (
    // <Flex vertical align="center" gap={5}>
    //   <Title level={2}>Произошла неизвестная ошибка</Title>
    //   <Text style={{ fontSize: "20px" }}>{error.status}</Text>
    //   <Text style={{ fontSize: "20px" }}>{error.statusText}</Text>
    //   <Link to={"/"}>
    //     <Button type="primary">Вернуться на главную страницу</Button>
    //   </Link>
    // </Flex>
    <>
      {error.status === 404 ? (
        <Result
          status="404"
          title="404"
          subTitle="Данная страница не существует."
          extra={
            <Link to={"/"}>
              <Button type="primary">На главную страницу</Button>
            </Link>
          }
        />
      ) : null}
    </>
  );
}
