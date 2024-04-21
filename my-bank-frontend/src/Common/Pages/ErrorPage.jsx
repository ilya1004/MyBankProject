import { useRouteError, Link } from "react-router-dom";
import { Button, Result } from "antd";

export default function ErrorPage() {
  const error = useRouteError();
  console.log(error);

  const getPage = (error) => {
    if (error.status === 404) {
      return (
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
      );
    } else if (error.status === 401) {
      return (
        <Result
          status="403"
          title="401"
          subTitle="Вы не аутентифицированы в системе."
          extra={
            <Link to={"/login"}>
              <Button type="primary">Войти</Button>
            </Link>
          }
        />
      );
    } else if (error.status === 403) {
      return (
        <Result
          status="403"
          title="403"
          subTitle="Произошла ошибка авторизации."
          extra={
            <Link to={"/"}>
              <Button type="primary">На главную страницу</Button>
            </Link>
          }
        />
      );
    } 
  };

  return (
    <>
      {getPage(error)}
    </>
  );
}
