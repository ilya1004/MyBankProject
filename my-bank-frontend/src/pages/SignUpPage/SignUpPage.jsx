import React from "react";
import {
  Form,
  Button,
  Input,
  Card,
  Flex,
} from "antd";
import { Link, Route, Routes } from "react-router-dom";
import "../LoginPage/Login.css";
import "./SignUp.css"
import LoginPage from "../LoginPage/LoginPage";

const BASE_URL = `http://localhost:8000`;
const url_register = BASE_URL + `/auth/register`;

class SignUpPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      nickname: "",
      email: "",
      password: "",
      passwordConfirm: "",
    };
  }

  handleSubmit = (e) => {
    e.preventDefault();

    if (this.state.password !== this.state.passwordConfirm) {
      alert("You are entered different passwords");
      this.setState({
        nickname: "",
        email: "",
        password: "",
        passwordConfirm: "",
      });
    } else {
      const data = {
        email: this.state.email,
        password: this.state.password,
        is_active: true,
        is_superuser: false,
        is_verified: false,
        nickname: this.state.nickname,
      };

      console.log(data);

      fetch(url_register, {
        method: "POST",
        credentials: "include",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      })
        .then((response) => {
          return response.json();
        })
        .then((data) => {
          this.setState({ inputValue: data.status });
          if (data.email === this.state.email) {
            window.location.href = "/";
          }
          console.log(data);
        });
    }
  };

  goToLogin() {
    window.location.href = "/";
  }

  handleChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  render() {
    return (
      <Flex justify="center" align="center">
        <Card className="reg-card" title="Введите свои данные">
          <Form
            labelCol={{ span: 8, offset: 2 }}
            labelAlign="right"
            // wrapperCol={{ span: 40 }}
            layout="horizontal"
            // style={{ maxWidth: 800 }}
          >
            <Form.Item
              label="Электронная почта:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input type="email" />
            </Form.Item>
            <Form.Item
              label="Пароль"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input.Password />
            </Form.Item>
            <Form.Item
              label="Подтвердите пароль:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input.Password />
            </Form.Item>
            <Form.Item
              label="Никнейм:"
              tooltip="Ваше имя в системе банка"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Имя:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Фамилия:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Отчество:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Серия паспорта:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Номер паспорта:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Гражданство:"
              rules={[
                {
                  required: true,
                },
              ]}
            >
              <Input />
            </Form.Item>
          </Form>
          <Flex justify="center" align="center">
            <Button type="primary" 
							style={{ 
								margin: "10px 0px" 
								}}>
              Зарегистироваться
            </Button>
          </Flex>
          <div className="div-to-login-page">
            Уже есть аккаунт?{" "}
            <Link className="link-primary" to="/login">
              Войти
            </Link>
          </div>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
          </Routes>
        </Card>
      </Flex>
    );
  }
}

export default SignUpPage;
