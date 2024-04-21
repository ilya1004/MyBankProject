import axios from "axios";
import { useEffect, useState } from "react";
import {
  Card,
  Flex,
  List,
  Typography,
  Button,
  Col,
  Row,
  Input,
  Form,
} from "antd";
import { showMessageStc, handleResponseError } from "../../../../Common/Services/ResponseErrorHandler";
import { BASE_URL } from "../../../../Common/Store/constants";

const { Title, Text } = Typography;

export default function ChangePassword({
  onSetIsOpenSettings,
  onSetIsChangingPassword
}) {
  const [oldEmailEdit, setOldEmailEdit] = useState("");
  const [oldPassEdit, setOldPassEdit] = useState("");
  const [newPassEdit, setNewPassEdit] = useState("");
  const [newPassConfEdit, setNewPassConfEdit] = useState("");

  const handleOldEmailEdit = (e) => {
    setOldEmailEdit(e.target.value);
  };

  const handleOldPassEdit = (e) => {
    setOldPassEdit(e.target.value);
  };

  const handleNewPassEdit = (e) => {
    setNewPassEdit(e.target.value);
  };

  const handleNewPassConfEdit = (e) => {
    setNewPassConfEdit(e.target.value);
  };

  const handleCancel = () => {
    setOldEmailEdit("");
    setOldPassEdit("");
    setNewPassEdit("");
    setNewPassConfEdit("");
    onSetIsChangingPassword(false);
  };

  const editUserPassword = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    const data = {
      oldEmail: oldEmailEdit,
      oldPassword: oldPassEdit,
      newPassEdit: newPassEdit,
    };
    try {
      const res = await axiosInstance.put(`User/UpdatePassword`, data);
      console.log(res.data["status"]);
      showMessageStc("Пароль был успешно изменен", "success");
      onSetIsChangingPassword(false);
      onSetIsOpenSettings(false);
    } catch (err) {
      handleResponseError(err.response);
      setOldEmailEdit("");
      setOldPassEdit("");
      setNewPassEdit("");
      setNewPassConfEdit("");
    }
  };

  const handleEnter = () => {
    if (
      oldEmailEdit.length === 0 ||
      oldPassEdit.length === 0 ||
      newPassEdit.length === 0
    ) {
      showMessageStc("Вы заполнили не все поля для ввода", "error");
      return;
    }

    if (newPassEdit !== newPassConfEdit) {
      showMessageStc("Введенные вами пароли не совпадают", "error");
      return;
    }
    editUserPassword();
  };

  return (
    <Card style={{ width: "600px" }}>
      <Flex
        vertical
        align="center"
        justify="center"
        gap={16}
        style={{ width: "100%" }}
      >
        <Row gutter={[16, 16]}>
          <Col style={{ width: "250px" }}>
            <Text style={{ fontSize: "16px" }}>Электронная почта:</Text>
          </Col>
          <Col style={{ width: "200px" }}>
            <Input onChange={handleOldEmailEdit} />
          </Col>
        </Row>
        <Row gutter={[16, 16]}>
          <Col style={{ width: "250px" }}>
            <Text style={{ fontSize: "16px" }}>Пароль:</Text>
          </Col>
          <Col style={{ width: "200px" }}>
            <Input.Password onChange={handleOldPassEdit} />
          </Col>
        </Row>
        <Row gutter={[16, 16]}>
          <Col style={{ width: "250px" }}>
            <Text style={{ fontSize: "16px" }}>Новый пароль:</Text>
          </Col>
          <Col style={{ width: "200px" }}>
            <Input.Password onChange={handleNewPassEdit} />
          </Col>
        </Row>
        <Row gutter={[16, 16]}>
          <Col style={{ width: "250px" }}>
            <Text style={{ fontSize: "16px" }}>Подтвердите новый пароль:</Text>
          </Col>
          <Col style={{ width: "200px" }}>
            <Input.Password onChange={handleNewPassConfEdit} />
          </Col>
        </Row>
      </Flex>
      <Flex
        gap={20}
        align="center"
        justify="center"
        style={{
          margin: "20px 0px 0px 0px",
        }}
      >
        <Button onClick={handleCancel}>Отмена</Button>
        <Button type="primary" onClick={handleEnter}>
          Применить
        </Button>
      </Flex>
    </Card>
  );
}
