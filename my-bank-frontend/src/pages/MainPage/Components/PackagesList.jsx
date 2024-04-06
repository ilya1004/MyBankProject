import { List, Typography, InputNumber, Flex, Card } from "antd";
import { useState, useEffect } from "react";
import axios from "axios";

const { Title, Text, Paragraph } = Typography;

export default function PackagesList() {
  const [items, setItems] = useState([]);
  const [packagesData, setPackagesData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const BASE_URL = `https://localhost:7050/api/`;
      const axiosInstance = axios.create({ baseURL: BASE_URL });
      axiosInstance
        .get(`CardPackages/GetAllInfo`)
        .then((response) => {
          console.log(response.data["list"]);
          setPackagesData(response.data["list"]);
          setItems(response.data["list"]);
        })
        .catch((err) => {
          console.error(err);
        });
    };
    fetchData();
  }, []);

  // console.log(items)

  const listItem = (item) => {
    // console.log(items);
    return (
      <List.Item key={item['id']}>
        <Card
				// style={{
					// width: "400px"
				// }}
				>
          <Title level={2}>{item["name"]}</Title>
          <Title level={4}>{`${item['price']} BYN`}</Title>
          <Paragraph >{`Условия бесплатности (в месяц):`}</Paragraph>
          <Paragraph>{`Количество операций: ${item['operationsNumber']}`}</Paragraph>
          <Paragraph>{`Сумма операций: ${item['operationsNumber']}`}</Paragraph>
          {/* <Text>{`Средний остаток по счету: ${item['']}`}</Text> */}
        </Card>
      </List.Item>
    );
  };

  return (
      <List
        itemLayout="horizontal"
        grid={{
          gutter: 20,
          column: items.length,
        }}
        style={{
        	margin: "0px 30px",
        }}
        dataSource={items}
        renderItem={(item) => listItem(item)}
      />
  );
}
