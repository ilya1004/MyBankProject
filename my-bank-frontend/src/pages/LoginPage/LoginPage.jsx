import { Form, Button, Input, Card, Flex } from "antd";
import React from "react";
import "./LoginPage.css";
import { Link, Route, Routes } from "react-router-dom";
import SignUpPage from "../SignUpPage/SignUpPage.jsx";

const BASE_URL = `http://localhost:7050/api`;
// const url_login = BASE_URL + `/auth/jwt/login`;
const axios = require("axios").default;

class LoginPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      inputValue: "error",
    };
  }

  handleSubmit = (e) => {
    // e.preventDefault();

    const axiosInstance = axios.create({baseURL: BASE_URL})

    let data = new URLSearchParams();
    data.append("grant_type", "");
    data.append("username", this.state.email);
    data.append("password", this.state.password);
    data.append("scope", "");
    data.append("client_id", "");
    data.append("client_secret", "");

    var response = axios.post(`User/Login`,
    {
      Email: "string",
      Password: "string"
    })
  };

  //     fetch(url_login, {
  //         method: 'POST',
  //         credentials: 'include',
  //         body: new URLSearchParams(data)
  //     })
  //         .then((response) => {
  //             return response.json()
  //         })
  //         .then((data) => {
  //             this.setState({inputValue: data.status})
  //             if (data.status === "success") {
  //                 window.location.href = '/home';
  //             } else {
  //                 alert("User with given email is not registered")
  //                 this.setState({email: "", password: ""})
  //             }
  //             console.log(data);
  //         })
  // };

  // handleChange = (e) => {
  //   this.setState({ [e.target.name]: e.target.value });
  // };

  render() {
    return (
      <Flex justify="center" align="center">
        <Card className="login-card" title="Введите свои данные">
          <Form
            labelCol={{ span: 8, offset: 2 }}
            labelAlign="right"
            // wrapperCol={{ span: 40 }}
            layout="horizontal"
            // style={{ width: 800 }}
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
          </Form>
          <Flex justify="center" align="center">
            <Button
              type="primary"
              style={{
                margin: "10px 0px",
              }}
            >
              Войти
            </Button>
          </Flex>
          <div className="div-to-reg-page">
            Еще нет аккаунта?{" "}
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
}

export default LoginPage;
