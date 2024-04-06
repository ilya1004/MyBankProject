import React, { useEffect, useState } from "react";
import {
  Typography,
  List,
  Card,
  Flex,
  InputNumber,
  Dropdown,
  Space,
} from "antd";
import { DownOutlined } from "@ant-design/icons";
import axios from "axios";

const { Title, Text } = Typography;


export default function CurrencyConventer() {
  const [currencies, setCurrencies] = useState([
    [0, "USD"],
    [1, "EUR"],
    [2, "BYN"],
    [3, "RUB"],
  ]);
  const [currenciesData, setCurrenciesData] = useState([]);
  const [values, setValues] = useState([0, 0, 0, 0]);
  const [items, setItems] = useState([]);
  const [dctCurr, setDctCurr] = useState({
    0: "USD",
    1: "EUR",
    2: "BYN",
    3: "RUB",
  });

  useEffect(() => {
    const fetchData = async () => {
      const axiosInstance = axios.create();
      axiosInstance
        .get(`https://api.nbrb.by/exrates/rates?periodicity=0`)
        .then((response) => {
          setCurrenciesData(response.data);
          setOtherCurrencies(response.data);
        })
        .catch((err) => {
          console.error(err);
        });
    };
    fetchData();
  }, []);

  const setOtherCurrencies = (data) => {
    let lst = [];
    for (let i = 0; i < data.length; i++) {
      lst.push({
        label: (
          <Text>{`${data[i]["Cur_Abbreviation"]} (${data[i]["Cur_Name"]})`}</Text>
        ),
        key: `${i}`,
      });
    }
    setItems(lst);
  };

  const handleChangeValue = (index, value) => {
    const updatedList = values.map((item, i) => {
      if (i === index) {
        return value;
      }

      if (dctCurr[index] === "BYN") {
        let elem = currenciesData.find(
          (element) => element["Cur_Abbreviation"] === dctCurr[i]
        );

        let officialRate = elem["Cur_OfficialRate"];
        let scale = elem["Cur_Scale"];

        let valueInByn = (value * scale) / officialRate;

        return valueInByn;
      }

      let res = 0;
      if (dctCurr[i] === "BYN") {
        let elem = currenciesData.find(
          (element) => element["Cur_Abbreviation"] === dctCurr[index]
        );
        let officialRate = elem["Cur_OfficialRate"];
        let scale = elem["Cur_Scale"];

        res = (value / scale) * officialRate;
      } else {
        let elem = currenciesData.find(
          (element) => element["Cur_Abbreviation"] === dctCurr[i]
        );

        let officialRate = elem["Cur_OfficialRate"];
        let scale = elem["Cur_Scale"];

        let valueInByn = (value * scale) / officialRate;

        elem = currenciesData.find(
          (element) => element["Cur_Abbreviation"] === dctCurr[index]
        );

        officialRate = elem["Cur_OfficialRate"];
        scale = elem["Cur_Scale"];

        res = (valueInByn / scale) * officialRate;
      }

      return res;
    });

    setValues(updatedList);
  };

  const onItemClick = ({ key }) => {
    setCurrencies((prevList) => [
      ...prevList,
      [currencies.length, currenciesData[key]["Cur_Abbreviation"]],
    ]);
    setValues((prevList) => [...prevList, 0]);
    setDctCurr((prevDict) => ({
      ...prevDict,
      [Object.keys(dctCurr).length]: currenciesData[key]["Cur_Abbreviation"],
    }));
  };

  const listItem = (item) => {
    return (
      <List.Item key={item[0]}>
        <Flex justify="flex-start" align="center" gap="middle">
          <Text strong={true}>{item[1]}</Text>
          <InputNumber
            key={item[0]}
            min={0}
            value={values[item[0]]}
            placeholder="Введите значение"
            precision={4}
            // formatter={(value) => `${value}`.replace(/\B(0)+/g, '')}
            onChange={(itemChange) => handleChangeValue(item[0], itemChange)}
            style={{ width: "150px" }}
            changeOnWheel
          />
        </Flex>
      </List.Item>
    );
  };

  return (
    <Card
      style={{
        marginLeft: "100px",
        width: "300px",
      }}
    >
      <List
        style={{
          margin: "0px 0px",
        }}
        dataSource={currencies}
        renderItem={(item) => listItem(item)}
      />

      <Dropdown
        getPopupContainer={() => document.getElementById("container")}
        menu={{
          items,
          onClick: onItemClick,
          // style: { maxHeight: "200px" },
        }}
        trigger={["click"]}
      >
        <a onClick={(e) => e.preventDefault()}>
          <Space>
            Добавить валюту
            <DownOutlined />
          </Space>
        </a>
      </Dropdown>
    </Card>
  );
}
