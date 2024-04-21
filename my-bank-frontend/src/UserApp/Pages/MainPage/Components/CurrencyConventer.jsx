import React, { useEffect, useState } from "react";
import {
  Typography,
  List,
  Card,
  Flex,
  InputNumber,
  Dropdown,
  Space,
  Button,
} from "antd";
import { DownOutlined, CloseOutlined } from "@ant-design/icons";

const { Text } = Typography;

export default function CurrencyConventer({ currenciesData }) {
  const [currencies, setCurrencies] = useState([
    [0, "USD", true],
    [1, "EUR", true],
    [2, "BYN", true],
    [3, "RUB", true],
  ]);
  const [dctCurr, setDctCurr] = useState({
    0: "USD",
    1: "EUR",
    2: "BYN",
    3: "RUB",
  });
  const [values, setValues] = useState([0, 0, 0, 0]);
  const [items, setItems] = useState([]);

  useEffect(() => {
    let lst = [];
    for (let i = 0; i < currenciesData.length; i++) {
      lst.push({
        label: (
          <Text>{`${currenciesData[i].Cur_Abbreviation} (${currenciesData[i].Cur_Name})`}</Text>
        ),
        key: `${i}`,
      });
    }
    setItems(lst);
  }, [currenciesData]);

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
      [currencies.length, currenciesData[key].Cur_Abbreviation, false],
    ]);
    setValues((prevList) => [...prevList, 0]);
    setDctCurr((prevDict) => ({
      ...prevDict,
      [Object.keys(dctCurr).length]: currenciesData[key].Cur_Abbreviation,
    }));
  };

  const handleClickDelCurrency = (item) => {
    let updCurrencies = [];
    let count = 0;
    for (let i = 0; i < currencies.length; i++) {
      if (currencies[i][1] === item[1]) {
        continue;
      } else {
        updCurrencies.push([count, currencies[i][1], currencies[i][2]]);
        count++;
      }
    }
    setCurrencies(updCurrencies);

    setValues((prevList) => [
      ...prevList.slice(0, item[0]),
      ...prevList.slice(item[0] + 1),
    ]);

    let updDct = {};
    count = 0;
    for (let index in dctCurr) {
      if (dctCurr[index] === item[1]) {
        continue;
      } else {
        updDct[count] = dctCurr[index];
        count++;
      }
    }
    setDctCurr(updDct);
  };

  const listItem = (item) => {
    return (
      <List.Item key={item[0]}>
        <Flex justify="flex-start" align="center">
          <Text strong={true} style={{ width: "35px" }}>
            {item[1]}
          </Text>
          <InputNumber
            key={item[0]}
            min={0}
            value={values[item[0]]}
            placeholder="Введите значение"
            precision={4}
            onChange={(itemChange) => handleChangeValue(item[0], itemChange)}
            style={{ width: "150px", margin: "0px 0px 0px 10px" }}
            changeOnWheel
          />
          {item[2] === false ? (
            <Button
              shape="circle"
              type="text"
              style={{ margin: "0px 0px 0px 5px" }}
              icon={<CloseOutlined style={{ fontSize: "14px" }} />}
              onClick={() => handleClickDelCurrency(item)}
            />
          ) : null}
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
