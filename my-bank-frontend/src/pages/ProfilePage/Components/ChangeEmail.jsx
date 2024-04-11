import axios from "axios";
import { useState } from "react";
import { Card, Flex, Typography, Button, Col, Row, Input, Form } from "antd";

const { Title, Text } = Typography;

const BASE_URL = `https://localhost:7050/api/`;

export default function ChangeEmail({
  onSetIsOpenSettings,
  onSetIsChangingEmail,
  onShowMessage,
}) {
  const [oldEmailEdit, setOldEmailEdit] = useState("");
  const [oldPassEdit, setOldPassEdit] = useState("");
  const [newEmailEdit, setNewEmailEdit] = useState("");

  const handleOldEmailEdit = (e) => {
    setOldEmailEdit(e.target.value);
  };

  const handleOldPassEdit = (e) => {
    setOldPassEdit(e.target.value);
  };

  const handleNewEmailEdit = (e) => {
    setNewEmailEdit(e.target.value);
  };

  const handleCancel = () => {
    setOldEmailEdit("");
    setOldPassEdit("");
    setNewEmailEdit("");
    onSetIsChangingEmail(false);
    // onSetIsOpenSettings(false);
  };

  const handleEnter = () => {
    const editUserEmail = async () => {
      const axiosInstance = axios.create({
        baseURL: BASE_URL,
        withCredentials: true,
      });
      const data = {
        oldEmail: oldEmailEdit,
        oldPassword: oldPassEdit,
        newEmail: newEmailEdit,
      };
      try {
        const res = await axiosInstance.put(`User/UpdateEmail`, data);
        console.log(res.data["status"]);
        onShowMessage("Электронная почта была успешно изменена", "success");
        onSetIsChangingEmail(false);
        onSetIsOpenSettings(false);
      } catch (err) {
        if (err.response.status === 401) {
          onShowMessage("Вы ввели неверную электронную почту или пароль", "error");
          setOldEmailEdit("");
          setOldPassEdit("");
          setNewEmailEdit("");
          // setNewPassConfEdit("");
        }
        console.error(err);
      }
    };
    if (oldEmailEdit.length === 0 || oldPassEdit.length === 0 || newEmailEdit.length === 0) {
			onShowMessage("Вы заполнили не все поля для ввода", "error");
    }
    editUserEmail();
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
            <Text style={{ fontSize: "16px" }}>Новая электронная почта:</Text>
          </Col>
          <Col style={{ width: "200px" }}>
            <Input onChange={handleNewEmailEdit} />
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
