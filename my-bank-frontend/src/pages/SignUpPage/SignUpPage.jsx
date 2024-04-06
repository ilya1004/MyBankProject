import React, { useState } from "react";
import { Form, Button, Input, Card, Flex, Typography, message } from "antd";
import { Link, Route, Routes, useNavigate } from "react-router-dom";
import LoginPage from "../LoginPage/LoginPage";
import axios from "axios";
import "./SignUpPage.css";

const { Text, Title } = Typography;

const BASE_URL = `http://localhost:7050/api`;

export default function SignUpPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [password2, setPassword2] = useState("");
  const [nickname, setNickname] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [patronymic, setPatronymic] = useState("");
  const [passportSeries, setPassportSeries] = useState("");
  const [passportNumber, setPassportNumber] = useState("");
  const [citizenship, setCitizenship] = useState("");
  const [isValid, setIsValid] = useState("");
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const handleChangeEmail = (e) => {
    setEmail(e.target.value);
  };

  const handleChangePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleChangePassword2 = (e) => {
    setPassword2(e.target.value);
  };

  const handleChangeNickname = (e) => {
    setNickname(e.target.value);
  };

  const handleChangeName = (e) => {
    setName(e.target.value);
  };

  const handleChangeSurname = (e) => {
    setSurname(e.target.value);
  };

  const handleChangePatronymic = (e) => {
    setPatronymic(e.target.value);
  };

  const handleChangePassportSeries = (e) => {
    setPassportSeries(e.target.value);
  };

  const handleChangePassportNumber = (e) => {
    setPassportNumber(e.target.value);
  };

  const handleChangeCitizenship = (e) => {
    setCitizenship(e.target.value);
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

  const handleSubmit = (e) => {
    e.preventDefault();

    const axiosInstance = axios.create({ baseURL: BASE_URL });

    axiosInstance
      .post(`User/Register`, {
        Email: email.trim(),
        Password: password.trim(),
        Nickname: nickname.trim(),
        Name: name.trim(),
        Surname: surname.trim(),
        Patronymic: patronymic.trim(),
        PassportSeries: passportSeries.trim(),
        PassportNumber: passportNumber.trim(),
        Citizenship: citizenship.trim(),
      })
      .then((response) => {
        console.log(response.data);
        if (response.status === 200) {
          setIsValid(true);
          navigate("/login");
        } else {
          setIsValid(false);
          showMessage("Произошла ошибка. Повторите попытку");
        }
      })
      .catch((err) => {
        showMessage("Произошла неизвестная ошибка.");
        console.error(err);
      });
  };

  return (
    <Flex
      justify="center"
      align="flex-start"
      style={{
        height: "105vh",
        margin: "30px 0px 0px 0px",
      }}
    >
      <Card
        style={{
          display: "block",
          width: "550px",
        }}
        title={
          <Title style={{ margin: "0px" }} level={4}>
            Введите свои данные
          </Title>
        }
      >
        <Form
          labelCol={{ span: 8, offset: 2 }}
          labelAlign="right"
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
          <Form.Item
            label="Подтвердите пароль"
            name="password2"
            rules={[
              {
                required: true,
                message: "Подтвердите пароль!",
              },
            ]}
          >
            <Input.Password
              name="password2"
              onChange={handleChangePassword2}
              value={password}
            />
          </Form.Item>
          <Form.Item
            label="Никнейм"
            name="nickname"
            tooltip="Ваше отображаемое имя в системе банка"
            rules={[
              {
                required: true,
                message: "Введите никнейм!",
              },
            ]}
          >
            <Input
              name="nickname"
              onChange={handleChangeNickname}
              value={nickname}
            />
          </Form.Item>
          <Form.Item
            label="Имя:"
            name="name"
            rules={[
              {
                required: true,
                message: "Введите ваше имя!",
              },
            ]}
          >
            <Input name="name" onChange={handleChangeName} value={name} />
          </Form.Item>
          <Form.Item
            label="Фамилия:"
            name="surname"
            rules={[
              {
                required: true,
                message: "Введите вашу фамилию!",
              },
            ]}
          >
            <Input
              name="surname"
              onChange={handleChangeSurname}
              value={surname}
            />
          </Form.Item>
          <Form.Item
            label="Отчество:"
            name="patronymic"
            rules={[
              {
                required: true,
                message: "Введите ваше отчество!",
              },
            ]}
          >
            <Input
              name="patronymic"
              onChange={handleChangePatronymic}
              value={patronymic}
            />
          </Form.Item>
          <Form.Item
            label="Серия паспорта:"
            name="passportSeries"
            rules={[
              {
                required: true,
                message: "Введите вашу серию паспорта!",
              },
            ]}
          >
            <Input
              name="passportSeries"
              onChange={handleChangePassportSeries}
              value={passportSeries}
            />
          </Form.Item>
          <Form.Item
            label="Номер паспорта:"
            name="passportNumber"
            rules={[
              {
                required: true,
                message: "Введите ваш номер паспорта!",
              },
            ]}
          >
            <Input
              name="passportNumber"
              onChange={handleChangePassportNumber}
              value={passportNumber}
            />
          </Form.Item>
          <Form.Item
            label="Гражданство:"
            name="citizenship"
            rules={[
              {
                required: true,
                message: "Введите ваше гражданство!",
              },
            ]}
          >
            <Input
              name="citizenship"
              onChange={handleChangeCitizenship}
              value={citizenship}
            />
          </Form.Item>

          <Form.Item>
            <Flex justify="center" align="center">
              <Button
                type="primary"
                htmlType="submit"
                style={{
                  margin: "0px",
                }}
              >
                Зарегистрироваться
              </Button>
            </Flex>
          </Form.Item>
        </Form>
        {contextHolder}
        <Flex align="center" justify="center">
          <Text style={{ marginRight: "5px" }}>Уже есть аккаунт?</Text>
          <Link className="link-primary" to="/login">
            Войти
          </Link>
        </Flex>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </Card>
    </Flex>
  );
}
