import React, { useState } from "react";
import {
  Form,
  Button,
  Input,
  Card,
  Flex,
  Typography,
  Select,
  message,
  DatePicker,
  Upload,
} from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { Link, useNavigate } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import axios from "axios";
import { listCountries } from "../../../Common/Store/constants";
import { showMessageStc } from "../../../Common/Services/ResponseErrorHandler";
import { handleResponseError } from "../../../Common/Services/ResponseErrorHandler";
import dayjs from "dayjs";

const { Text, Title } = Typography;

const inputWidth = "200px";

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
  const [birthdayDate, setBirthdayDate] = useState(null);
  const [citizenship, setCitizenship] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  // const [avatarFile, setAvatarFile] = useState("");
  const navigate = useNavigate();

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
    setCitizenship(e);
  };

  const handleBirthdayDate = (date) => {
    setBirthdayDate(date);
  };

  const handleChangePhoneNumber = (e) => {
    setPhoneNumber(e.target.value);
  };

  const registerUser = async () => {
    const axiosInstance = axios.create({ baseURL: BASE_URL });
    const data = {
      email: email.trim(),
      password: password.trim(),
      nickname: nickname.trim(),
      name: name.trim(),
      surname: surname.trim(),
      patronymic: patronymic.trim(),
      phoneNumber: phoneNumber.trim(),
      passportSeries: passportSeries.trim(),
      passportNumber: passportNumber.trim(),
      birthdayDate: birthdayDate.toJSON().substring(0, 10),
      citizenship: citizenship.trim(),
    };
    try {
      const res = await axiosInstance.post(`User/Register`, data);
      if (res.status === 201) {
        showMessageStc("Вы были успешно зарегистрированы.", "success");
        navigate("/profile");
      }
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleSubmit = (e) => {
    // e.preventDefault();
    registerUser();
  };

  const filterOption = (input, option) => {
    return (option?.label ?? "").toLowerCase().includes(input.toLowerCase());
  };

  const handleBeforeUploadFile = (file) => {
    console.log("qwe");
    console.log(file);
  };

  // const handleChangeFileState = (info) => {
  // console.log(info)
  // };

  return (
    <Flex
      justify="center"
      align="flex-start"
      style={{
        height: "105vh",
      }}
    >
      <Card
        style={{
          margin: "30px 0px 0px 0px",
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
              // {
              //   type: "email",
              //   message: "Вы ввели неверную электронную почту",
              // },
            ]}
            hasFeedback
          >
            <Input
              name="email"
              onChange={handleChangeEmail}
              value={email}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
            hasFeedback
          >
            <Input.Password
              name="password"
              onChange={handleChangePassword}
              value={password}
              style={{ width: inputWidth, marginLeft: "5px" }}
            />
          </Form.Item>
          <Form.Item
            label="Подтвердите пароль"
            name="password2"
            dependencies={["password"]}
            rules={[
              {
                required: true,
                message: "Подтвердите пароль!",
              },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue("password") === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(
                    new Error("Введенные пароли не совпадают!")
                  );
                },
              }),
            ]}
            hasFeedback
          >
            <Input.Password
              name="password2"
              onChange={handleChangePassword2}
              value={password}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
              count={{
                show: true,
                max: 30,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="nickname"
              onChange={handleChangeNickname}
              value={nickname}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
            <Input
              count={{
                max: 30,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="name"
              onChange={handleChangeName}
              value={name}
              style={{ width: inputWidth, marginLeft: "5px" }}
            />
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
              count={{
                max: 30,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="surname"
              onChange={handleChangeSurname}
              value={surname}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
              count={{
                max: 30,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="patronymic"
              onChange={handleChangePatronymic}
              value={patronymic}
              style={{ width: inputWidth, marginLeft: "5px" }}
            />
          </Form.Item>
          <Form.Item
            label="Номер телефона:"
            name="phoneNumber"
            rules={[
              {
                required: true,
                message: "Введите ваш номер телефона!",
              },
            ]}
          >
            <Input
              addonBefore="+"
              count={{
                max: 20,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="phoneNumber"
              onChange={handleChangePhoneNumber}
              value={phoneNumber}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
              count={{
                max: 10,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="passportSeries"
              onChange={handleChangePassportSeries}
              value={passportSeries}
              style={{ width: inputWidth, marginLeft: "5px" }}
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
              count={{
                max: 20,
                exceedFormatter: (txt, { max }) => txt.slice(0, max),
              }}
              name="passportNumber"
              onChange={handleChangePassportNumber}
              value={passportNumber}
              style={{ width: inputWidth, marginLeft: "5px" }}
            />
          </Form.Item>
          <Form.Item
            label="Дата рождения:"
            name="birthday"
            rules={[
              {
                required: true,
                message: "Выберите вашу дату рождения!",
              },
            ]}
          >
            <DatePicker
              style={{ marginLeft: "5px" }}
              minDate={dayjs().year(1900)}
              maxDate={dayjs().subtract(18, "year")}
              onChange={handleBirthdayDate}
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
            <Select
              showSearch
              placeholder="Выберите страну"
              style={{ width: "250px", marginLeft: "5px" }}
              onChange={handleChangeCitizenship}
              filterOption={filterOption}
              options={listCountries}
            />
          </Form.Item>
          {/* <Form.Item label="Загрузите аватар:" name="avatar">
            <Upload
              style={{ marginLeft: "5px" }}
              action={`${BASE_URL}User/UploadAvatarFileRegistration`}
              // onChange={handleChangeFileState}
              beforeUpload={handleBeforeUploadFile}
              listType="picture"
              maxCount={1}
            >
              <Button icon={<UploadOutlined />}>Загрузить изображение</Button>
            </Upload>
          </Form.Item> */}

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
        <Flex align="center" justify="center">
          <Text style={{ marginRight: "5px" }}>Уже есть аккаунт?</Text>
          <Link to="/login">Войти</Link>
        </Flex>
      </Card>
    </Flex>
  );
}
