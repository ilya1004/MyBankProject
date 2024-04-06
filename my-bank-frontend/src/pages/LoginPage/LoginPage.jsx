import {
  Form,
  Button,
  Input,
  Card,
  Flex,
  Alert,
  message,
  Typography,
} from "antd";
import React, { useState } from "react";
import {
  Link,
  Route,
  Routes,
  useNavigate,
  useLocation,
} from "react-router-dom";
import SignUpPage from "../SignUpPage/SignUpPage.jsx";
import axios from "axios";
import useAuth from "../../hooks/useAuth.jsx";
import { Role } from "../../store/AuthContext/AuthProvider.jsx";
import "./LoginPage.css";

const { Title, Text } = Typography;

const BASE_URL = `https://localhost:7050/api`;

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isValid, setIsValid] = useState("");
  const [messageApi, contextHolder] = message.useMessage();
  const navigate = useNavigate();
  const location = useLocation();
  const { setAuth, setRole, setId } = useAuth();

  const handleChangeEmail = (e) => {
    setEmail(e.target.value);
  };

  const handleChangePassword = (e) => {
    setPassword(e.target.value);
  };

  const showMessage = (msg) => {
    messageApi.open({
      type: "error",
      content: msg,
      style: {
        marginTop: "60px",
      },
    });
  };

  const from = location.state?.from?.pathname || "/";

  const handleSubmit = () => {
    const axiosInstance = axios.create({ baseURL: BASE_URL });

    let emailVal = email;
    let passwordVal = password;

    if (email.startsWith("#admin#") && !email.includes("@")) {
      axiosInstance
        .post(`Admin/Login`, {
          Email: email.trim(),
          Password: password.trim(),
        })
        .then((response) => {
          console.log(response.data);
          if (response.status === 200) {
            setIsValid(true);
            setId(response.data["id"]);
            setAuth(true);
            setRole(Role.Moderator);
            navigate("/admin/");
            // navigate(from, { replace: true });
          } else {
            setIsValid(false);
            showMessage("Введена неверная электронная почта или пароль");
          }
        })
        .catch((err) => {
          showMessage("Произошла неизвестная ошибка.");
          console.error(err);
        });
    } else if (email.startsWith("#moderator#") && !email.includes("@")) {
      axiosInstance
        .post(`Moderator/Login`, {
          Email: email.trim(),
          Password: password.trim(),
        })
        .then((response) => {
          console.log(response.data);
          if (response.status === 200) {
            setIsValid(true);
            setId(response.data["id"]);
            setAuth(true);
            setRole(Role.Moderator);
            navigate("/moderator/");
            // navigate(from, { replace: true });
          } else {
            setIsValid(false);
            showMessage("Введена неверная электронная почта или пароль");
          }
        })
        .catch((err) => {
          showMessage("Произошла неизвестная ошибка.");
          console.error(err);
        });
    } else {
      axiosInstance
        .post(`User/Login`, {
          Email: email.trim(),
          Password: password.trim(),
        })
        .then((response) => {
          console.log(response.data);
          if (response.status === 200) {
            setIsValid(true);
            setId(response.data["id"]);
            setAuth(true);
            setRole(Role.User);
            navigate(from, { replace: true });
          } else {
            setIsValid(false);
            showMessage("Введена неверная электронная почта или пароль");
          }
        })
        .catch((err) => {
          showMessage("Произошла неизвестная ошибка.");
          console.error(err);
        });
    }
  };

  return (
    <Flex
      justify="center"
      align="flex-start"
      style={{
        height: "80vh",
        margin: "100px 0px 0px 0px",
      }}
    >
      <Card
        style={{
          display: "block",
          width: "450px",
        }}
        title={
          <Title style={{ margin: "0px" }} level={4}>
            Введите свои данные
          </Title>
        }
      >
        <Form
          labelCol={{ span: 10, offset: 0 }}
          wrapperCol={{ offset: 0 }}
          layout="horizontal"
          onFinish={handleSubmit}
        >
          <Form.Item
            label="Электронная почта:"
            name="email"
            rules={[
              {
                required: true,
                message: "Введите электронную почту!",
              },
            ]}
          >
            <Input
              // type="email"
              name="email"
              onChange={handleChangeEmail}
              value={email}
            />
          </Form.Item>

          <Form.Item
            label="Пароль"
            name="password"
            rules={[
              {
                required: true,
                message: "Введите пароль!",
              },
            ]}
          >
            <Input.Password
              name="password"
              onChange={handleChangePassword}
              value={password}
            />
          </Form.Item>

          <Form.Item>
            <Flex justify="center" align="center">
              <Button
                type="primary"
                htmlType="submit"
                style={{
                  margin: "0px",
                  width: "75px",
                }}
              >
                Войти
              </Button>
            </Flex>
          </Form.Item>
        </Form>
        {contextHolder}
        <div className="div-to-reg-page">
          {"Еще нет аккаунта? "}
          <Link className="link-primary" to="/register">
            Зарегистироваться
          </Link>
        </div>
        <Routes>
          <Route path="/register" element={<SignUpPage />} />
        </Routes>
      </Card>
    </Flex>
  );
}
