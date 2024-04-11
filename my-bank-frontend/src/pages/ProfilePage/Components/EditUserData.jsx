import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Row, Col, Select } from "antd";
import { triggerToReload } from "../ProfilePage";

const { Title, Text, Paragraph } = Typography;

const BASE_URL = `https://localhost:7050/api/`;

const colTextWidth = "100px";
const colInputWidth = "200px";

const colTextWidth2 = "150px";
const colInputWidth2 = "200px";

const listCountries = [
  {
    value: "Азербайджан",
    label: "Азербайджан",
  },
  {
    value: "Армения",
    label: "Армения",
  },
  {
    value: "Беларусь",
    label: "Беларусь",
  },
  {
    value: "Болгария",
    label: "Болгария",
  },
  {
    value: "Великобритания",
    label: "Великобритания",
  },
  {
    value: "Германия",
    label: "Германия",
  },
  {
    value: "Грузия",
    label: "Грузия",
  },
  {
    value: "Дания",
    label: "Дания",
  },
  {
    value: "Испания",
    label: "Испания",
  },
  {
    value: "Италия",
    label: "Италия",
  },
  {
    value: "Казахстан",
    label: "Казахстан",
  },
  {
    value: "Кипр",
    label: "Кипр",
  },
	{
		value: "Китай",
		label: "Китай"
	},
  {
    value: "Кыргызстан",
    label: "Кыргызстан",
  },
  {
    value: "Латвия",
    label: "Латвия",
  },
  {
    value: "Литва",
    label: "Литва",
  },
  {
    value: "Молдова",
    label: "Молдова",
  },
  {
    value: "Нидерланды",
    label: "Нидерланды",
  },
  {
    value: "Норвегия",
    label: "Норвегия",
  },
  {
    value: "Польша",
    label: "Польша",
  },
  {
    value: "Россия",
    label: "Россия",
  },
  {
    value: "Румыния",
    label: "Румыния",
  },
  {
    value: "Соединенные Штаты Америки",
    label: "Соединенные Штаты Америки",
  },
  {
    value: "Словакия",
    label: "Словакия",
  },
  {
    value: "Словения",
    label: "Словения",
  },
  {
    value: "Таджикистан",
    label: "Таджикистан",
  },
  {
    value: "Туркмения",
    label: "Туркмения",
  },
  {
    value: "Турция",
    label: "Турция",
  },
  {
    value: "Узбекистан",
    label: "Узбекистан",
  },
  {
    value: "Украина",
    label: "Украина",
  },
  {
    value: "Финляндия",
    label: "Финляндия",
  },
  {
    value: "Франция",
    label: "Франция",
  },
  {
    value: "Швеция",
    label: "Швеция",
  },
  {
    value: "Эстония",
    label: "Эстония",
  },
];

export default function EditUserData({ userData, onSetIsEditing, onReload, onShowMessage }) {
  const [nicknameEdit, setNicknameEdit] = useState(userData["nickname"]);
  const [nameEdit, setNameEdit] = useState(userData["name"]);
  const [surnameEdit, setSurnameEdit] = useState(userData["surname"]);
  const [patronymicEdit, setPatronymicEdit] = useState(userData["patronymic"]);
  const [phoneNumberEdit, setPhoneNumberEdit] = useState(
    userData["phoneNumber"]
  );
  const [passportSeriesEdit, setPassportSeriesEdit] = useState(
    userData["passportSeries"]
  );
  const [passportNumberEdit, setPassportNumberEdit] = useState(
    userData["passportNumber"]
  );
  const [citizenshipEdit, setCitizenshipEdit] = useState(
    userData["citizenship"]
  );

  const handleNicknameEdit = (e) => {
    setNicknameEdit(e.target.value);
  };

  const handleNameEdit = (e) => {
    setNameEdit(e.target.value);
  };

  const handleSurnameEdit = (e) => {
    setSurnameEdit(e.target.value);
  };

  const handlePatronymicEdit = (e) => {
    setPatronymicEdit(e.target.value);
  };

  const handlePhoneNumberEdit = (e) => {
    setPhoneNumberEdit(e.target.value);
  };

  const handlePassportSeriesEdit = (e) => {
    setPassportSeriesEdit(e.target.value);
  };

  const handlePassportNumberEdit = (e) => {
    setPassportNumberEdit(e.target.value);
  };

  const handleCitizenshipEdit = (e) => {
    setCitizenshipEdit(e);
  };

  const filterOption = (input, option) => {
    return (option?.label ?? "").toLowerCase().includes(input.toLowerCase());
  };

  const handleEnter = () => {
    onReload();
    onSetIsEditing(false);
    const editUserData = async () => {
      const axiosInstance = axios.create({
        baseURL: BASE_URL,
        withCredentials: true,
      });
      const data = {
        id: 1,
        nickname: nicknameEdit,
        name: nameEdit,
        surname: surnameEdit,
        patronymic: patronymicEdit,
        phoneNumber: phoneNumberEdit,
        passportSeries: passportSeriesEdit,
        passportNumber: passportNumberEdit,
        citizenship: citizenshipEdit,
      };
      try {
        const res = await axiosInstance.put(`User/UpdatePersonalInfo`, data);
        console.log(res.data["status"]);
				onShowMessage("Данные были успешно изменены", "success")
      } catch (err) {
				onShowMessage("Во время изменения данных произошла ошибка", "error")
        console.error(err);
      }
    };
    editUserData();
  };

  const handleCancel = () => {
    setNicknameEdit("");
    setNameEdit("");
    setSurnameEdit("");
    setPatronymicEdit("");
    setPhoneNumberEdit("");
    setPassportSeriesEdit("");
    setPassportNumberEdit("");
    setCitizenshipEdit("");
    onSetIsEditing(false);
  };

  return (
    <>
      <Card
        style={{
          width: "90%",
          height: "270px",
        }}
      >
        <Flex>
          <Flex vertical gap={16} style={{ width: "100%" }}>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Никнейм:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input onChange={handleNicknameEdit} value={nicknameEdit} />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Имя:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input onChange={handleNameEdit} value={nameEdit} />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Фамилия:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input onChange={handleSurnameEdit} value={surnameEdit} />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth }}>
                <Text style={{ fontSize: "16px" }}>Отчество:</Text>
              </Col>
              <Col style={{ width: colInputWidth }}>
                <Input onChange={handlePatronymicEdit} value={patronymicEdit} />
              </Col>
            </Row>
          </Flex>
          <Flex vertical gap={16} style={{ width: "100%" }}>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth2 }}>
                <Text style={{ fontSize: "16px" }}>Номер телефона:</Text>
              </Col>
              <Col style={{ width: colInputWidth2 }}>
                <Input
                  addonBefore={<Text style={{ fontSize: "16px" }}>+</Text>}
                  onChange={handlePhoneNumberEdit}
                  value={phoneNumberEdit}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth2 }}>
                <Text style={{ fontSize: "16px" }}>Серия паспорта:</Text>
              </Col>
              <Col style={{ width: colInputWidth2 }}>
                <Input
                  count={{
                    // show: true,
                    max: 2,
                    exceedFormatter: (txt, { max }) => txt.substring(0, max),
                  }}
                  onChange={handlePassportSeriesEdit}
                  value={passportSeriesEdit}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth2 }}>
                <Text style={{ fontSize: "16px" }}>Номер паспорта:</Text>
              </Col>
              <Col style={{ width: colInputWidth2 }}>
                <Input
                  count={{
                    // show: true,
                    max: 7,
                    exceedFormatter: (txt, { max }) => txt.substring(0, max),
                  }}
                  onChange={handlePassportNumberEdit}
                  value={passportNumberEdit}
                />
              </Col>
            </Row>
            <Row gutter={[16, 16]}>
              <Col style={{ width: colTextWidth2 }}>
                <Text style={{ fontSize: "16px" }}>Гражданство:</Text>
              </Col>
              <Col style={{ width: colInputWidth2 }}>
                <Select
                  showSearch
                  defaultValue={citizenshipEdit}
                  placeholder="Выберите страну"
                  style={{ minWidth: "184px" }}
                  onChange={handleCitizenshipEdit}
                  filterOption={filterOption}
                  options={listCountries}
                />
              </Col>
            </Row>
          </Flex>
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
    </>
  );
}
