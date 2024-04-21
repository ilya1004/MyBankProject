import axios from "axios";
import { useState } from "react";
import { Card, Upload, Flex, Image, Typography, Button, Modal } from "antd";
import {
  EnvironmentOutlined,
  PhoneOutlined,
  CalendarOutlined,
  IdcardOutlined,
  MailOutlined,
  UploadOutlined,
} from "@ant-design/icons";
import {
  useNavigate,
  useLoaderData,
  useRevalidator,
  redirect,
  useOutletContext,
} from "react-router-dom";
import ListCards from "./Components/ListCards";
import ListAccounts from "./Components/ListAccounts";
import ListCredits from "./Components/ListCredits";
import ListDeposits from "./Components/ListDeposits";
import EditUserData from "./Components/EditUserData";
import ChangePassword from "./Components/ChangePassword";
import ChangeEmail from "./Components/ChangeEmail";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;

export const widthCardAcc = "300px";
export const heightCardAcc = "145px";

const getUserData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `User/GetInfoCurrent/?includeData=${true}`
    );
    console.log(res.data.item);
    return { userData: res.data.item, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { userData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { userData: null, error: err.response };
  }
};

const getUserAvatar = async (imgExt) => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`User/GetAvatarCurrent`, {
      responseType: "arraybuffer",
    });
    new Blob(["."]);
    const imageBlob = new Blob([res.data], { type: `image/${imgExt}` });
    const avatarUrl = URL.createObjectURL(imageBlob);
    return { avatarUrl, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { avatarUrl: null, error: err.response };
  }
};

export async function loader() {
  const { userData, error } = await getUserData();
  if (!userData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  const { avatarUrl, error: error1 } = await getUserAvatar(
    userData.avatarImagePath.split(".").pop()
  );
  return { userData, avatarUrl };
}

export default function ProfilePage() {
  const [isEditing, setIsEditing] = useState(false);
  const [isOpenSettings, setIsOpenSettings] = useState(false);
  const [isChangingPassword, setIsChangingPassword] = useState(false);
  const [isChangingEmail, setIsChangingEmail] = useState(false);
  const [openModal, setOpenModal] = useState(false);
  const [loadingModal, setLoadingModal] = useState(false);
  let revalidator = useRevalidator();
  const navigate = useNavigate();

  const [loginState, setLoginState] = useOutletContext();

  const { userData, avatarUrl } = useLoaderData();

  const deleteUserAccount = async () => {
    const axiosInstance = axios.create({
      baseURL: BASE_URL,
      withCredentials: true,
    });
    try {
      const res = await axiosInstance.put(
        `User/UpdateStatus?userId=${userData.id}&isActive=${false}`
      );
      showMessageStc("Учетная запись была успешно удалена", "success");
      setLoginState(false);
      navigate("/login");
    } catch (err) {
      handleResponseError(err.response);
    }
  };

  const handleReload = () => {
    revalidator.revalidate();
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

  const handleChangeFileState = (info) => {
    if (info.file.status === "done") {
      revalidator.revalidate();
    }
  };

  return (
    <>
      <Flex
        justify="center"
        align="flex-start"
        style={{
          margin: "10px 15px",
          minHeight: "90vh",
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
            style={{ maxHeight: "300px", maxWidth: "300px" }}
            src={avatarUrl}
            preview={false}
            // placeholder={AvatarPlaceholder}
          />
          <Flex gap={15} vertical align="center">
            <Flex gap={15}>
              <Button onClick={handleEditProfile}>Редактировать профиль</Button>
              <Button onClick={handleSettingsProfile}>
                Настройки аккаунта
              </Button>
            </Flex>
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
            style={{
              width: "70%",
              padding: "0px",
            }}
          >
            <Flex vertical gap={8} align="flex-start">
              <Title
                level={2}
                style={{ margin: "0px 0px 0px 0px" }}
              >{`${userData.nickname}`}</Title>
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
              <ListCards value={userData.cards} />
              <ListAccounts value={userData.personalAccounts} />
              <ListCredits value={userData.creditAccounts} />
              <ListDeposits value={userData.depositAccount} />
            </>
          ) : null}
          {isOpenSettings === true ? (
            <>
              <Card style={{ width: "550px" }}>
                <Flex justify="center" gap={50}>
                  <Flex align="center" gap={30} vertical>
                    <Button type="primary" onClick={handleChangePassword}>
                      Сменить пароль
                    </Button>
                    <Upload
                      accept="image/*"
                      action={`${BASE_URL}User/UploadAvatarFile`}
                      withCredentials={true}
                      onChange={handleChangeFileState}
                      maxCount={1}
                      showUploadList={false}
                    >
                      <Button icon={<UploadOutlined />}>
                        Загрузить изображение
                      </Button>
                    </Upload>
                    <Button onClick={handleCloseSettings}>Закрыть</Button>
                  </Flex>
                  <Flex align="center" gap={30} vertical>
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
            />
          ) : null}
          {isChangingEmail === true ? (
            <ChangeEmail
              onSetIsOpenSettings={setIsOpenSettings}
              onSetIsChangingEmail={setIsChangingEmail}
              onReload={handleReload}
            />
          ) : null}
          {/* {isUploadingFile === true ? (
            <UploadFile
              onSetIsOpenSettings={setIsOpenSettings}
              onSetIsUploadingFile={setIsUploadingFile}
              onReload={handleReload}
            />
          ) : null} */}
        </Flex>
      </Flex>
    </>
  );
}
