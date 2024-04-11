import axios from "axios";
import { useEffect, useState } from "react";
import {
  Card,
  Flex,
  Image,
  Typography,
  Button,
  Input,
  message,
  Modal,
} from "antd";
import {
  EnvironmentOutlined,
  PhoneOutlined,
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import Avatar from "../../assets/avatar-sq.jpg";
import ListCards from "./Components/ListCards";
import ListAccounts from "./Components/ListAccounts";
import ListCredits from "./Components/ListCredits";
import ListDeposits from "./Components/ListDeposits";
import EditUserData from "./Components/EditUserData";
import ChangePassword from "./Components/ChangePassword";
import ChangeEmail from "./Components/ChangeEmail";
import { BASE_URL } from "../../store/constants"

const { Title, Text } = Typography;

const imageSize = "300px";
export const widthCardAcc = "300px";
export const heightCardAcc = "145px";

export default function ProfilePage() {
  const [userData, setUserData] = useState({});
  const { role, id } = useAuth();
  const [isEditing, setIsEditing] = useState(false);
  const [isOpenSettings, setIsOpenSettings] = useState(false);
  const [isChangingPassword, setIsChangingPassword] = useState(false);
  const [isChangingEmail, setIsChangingEmail] = useState(false);
  const [toReload, setToReload] = useState(false);
  const [messageApi, contextHolder] = message.useMessage();
  const [openModal, setOpenModal] = useState(false);
  const [loadingModal, setLoadingModal] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const getUserData = async () => {
      const axiosInstance = axios.create({
        baseURL: BASE_URL,
        withCredentials: true,
      });
      axiosInstance
        .get(`User/GetInfoCurrent/?includeData=${true}`)
        .then((response) => {
          console.log(response.data["item"]);
          setUserData(response.data["item"]);
        })
        .catch((err) => {
          console.error(err);
        });
    };
    setToReload(false);
    getUserData();
  }, [toReload]);

  const deleteUserAccount = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      // const res = await axiosInstance.delete(`User/Delete`);
      // console.log(res.data["status"]);
      showMessage("Учетная запись была успешно удалена", "success");
      navigate("/");
    } catch (err) {
      if (err.response.status === 401) {
        showMessage("Вы ввели неверную электронную почту или пароль", "error");
      }
      console.error(err);
    }
  };

  const handleReload = () => {
    setToReload(true);
  };

  const handleEditProfile = () => {
    setIsOpenSettings(false);
    setIsChangingPassword(false);
    isEditing === false ? setIsEditing(true) : setIsEditing(false);
  };

  const handleSettingsProfile = () => {
    setIsEditing(false);
    setIsChangingPassword(false);
    isOpenSettings === false
      ? setIsOpenSettings(true)
      : setIsOpenSettings(false);
  };

  const handleCloseSettings = () => {
    setIsOpenSettings(false);
    setIsChangingPassword(false);
  };

  const printRegDate = (date) => {
    let dateObj = new Date(date);
    return `Клиент банка с ${dateObj.toLocaleDateString()}`;
  };

  const handleChangePassword = () => {
    setIsChangingEmail(false);
    isChangingPassword === false
      ? setIsChangingPassword(true)
      : setIsChangingPassword(false);
  };

  const handleDeleteAccount = () => {
    setOpenModal(true);
    setLoadingModal(true);
    setTimeout(() => {
      if (openModal === false) {
        console.log(openModal);
        setLoadingModal(false);
      }
    }, 3000);
  };

  const handleOkModal = () => {
    setOpenModal(false);
    deleteUserAccount();
  };

  const handleCancelModal = () => {
    setOpenModal(false);
  };

  const handleChangeEmail = () => {
    setIsChangingPassword(false);
    isChangingEmail === false
      ? setIsChangingEmail(true)
      : setIsChangingEmail(false);
  };

  const showMessage = (msg, msgType) => {
    messageApi.open({
      type: msgType,
      content: msg,
      duration: 3,
      style: {
        marginTop: "55px",
      },
    });
  };

  return (
    <div>
      <Flex
        justify="center"
        align="flex-start"
        style={{
          margin: "10px 15px",
          // height: "690px",
          height: "100vh",
        }}
      >
        <Flex
          gap={20}
          justify="flex-start"
          align="center"
          vertical
          style={{
            width: "35%",
          }}
        >
          <Image
            height={imageSize}
            width={imageSize}
            src={Avatar}
            preview={false}
          />
          <Flex gap={15}>
            <Button onClick={handleEditProfile}>Редактировать профиль</Button>
            <Button onClick={handleSettingsProfile}>Настройки аккаунта</Button>
          </Flex>
          <Card
            style={{
              width: "400px",
            }}
          >
            <Flex vertical gap={5}>
              <Flex gap={7}>
                <CalendarOutlined />
                <Text>{printRegDate(userData["registrationDate"])}</Text>
              </Flex>
              <Flex gap={7}>
                <IdcardOutlined style={{ margin: "1px 0px 0px 0px" }} />
                <Text>{`${userData["passportSeries"]}${userData["passportNumber"]}`}</Text>
              </Flex>
              <Flex gap={7}>
                <MailOutlined style={{ margin: "4px 0px 0px 0px" }} />
                <Text>{userData["email"]}</Text>
              </Flex>
            </Flex>
          </Card>
        </Flex>
        <Flex
          gap={20}
          vertical
          justify="flex-start"
          align="center"
          style={{
            width: "65%",
          }}
        >
          <Card
            // type="inner"
            style={{
              width: "70%",
              padding: "0px",
            }}
          >
            <Flex vertical gap={8} align="flex-start">
              <Title
                level={2}
                style={{ margin: "0px 0px 0px 0px" }}
              >{`${userData["nickname"]}`}</Title>
              <Title
                level={4}
                style={{
                  margin: "0px",
                }}
              >{`${userData["surname"]} ${userData["name"]} ${userData["patronymic"]}`}</Title>
              <Flex gap={5}>
                <EnvironmentOutlined />
                <Text>{`${userData["citizenship"]}`}</Text>
              </Flex>
              <Flex gap={5}>
                <PhoneOutlined />
                <Text>{`+${userData["phoneNumber"]}`}</Text>
              </Flex>
            </Flex>
          </Card>

          {isOpenSettings === false && isEditing === false ? (
            <>
              <ListCards value={userData["cards"]} />
              <ListAccounts value={userData["personalAccounts"]} />
              <ListCredits value={userData["creditAccounts"]} />
              <ListDeposits value={userData["depositAccount"]} />
            </>
          ) : null}
          {isOpenSettings === true ? (
            <>
              <Card style={{ width: "600px" }}>
                <Flex align="center" justify="center" gap={50}>
                  <Flex align="center" justify="center" gap={30} vertical>
                    <Button type="primary" onClick={handleChangePassword}>
                      Сменить пароль
                    </Button>
                    <Button onClick={handleCloseSettings}>Закрыть</Button>
                  </Flex>
                  <Flex align="center" justify="center" gap={30} vertical>
                    <Button type="primary" onClick={handleChangeEmail}>
                      Сменить электронную почту
                    </Button>
                    <Button type="primary" danger onClick={handleDeleteAccount}>
                      Удалить учетную запись
                    </Button>
                  </Flex>
                </Flex>
              </Card>
              <Modal
                title="Удаление учетной записи"
                open={openModal}
                onOk={handleOkModal}
                onCancel={handleCancelModal}
                confirmLoading={loadingModal}
                okButtonProps={{ type: "primary" }}
                okType="danger"
                okText="Удалить"
                cancelText="Отмена"
              >
                <Text style={{ fontSize: "16px" }}>
                  Вы действительно хотите удалить свою учетную запись?
                </Text>
              </Modal>
            </>
          ) : null}
          {isEditing === true ? (
            <EditUserData
              onSetIsEditing={handleEditProfile}
              onReload={handleReload}
              userData={userData}
            />
          ) : null}
          {isChangingPassword === true ? (
            <ChangePassword
              onSetIsOpenSettings={setIsOpenSettings}
              onSetIsChangingPassword={setIsChangingPassword}
              onShowMessage={showMessage}
            />
          ) : null}
          {isChangingEmail === true ? (
            <ChangeEmail
              onSetIsOpenSettings={setIsOpenSettings}
              onSetIsChangingEmail={setIsChangingEmail}
              onShowMessage={showMessage}
            />
          ) : null}
        </Flex>
      </Flex>
      {contextHolder}
    </div>
  );
}
