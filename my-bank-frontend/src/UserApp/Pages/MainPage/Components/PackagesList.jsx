import { List, Typography, Card } from "antd";
const { Title, Paragraph } = Typography;

export default function PackagesList({ packagesData }) {

  console.log(packagesData);

  const listItem = (item) => {
    return (
      <List.Item key={item["id"]}>
        <Card
          style={{
            width: "350px",
          }}
        >
          <Title level={2} style={{margin: "10px 0px 0px 0px"}}>{item.name}</Title>
          <Title level={4} style={{margin: "15px 0px 5px 0px"}}>{`${item.price} BYN`}</Title>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Условия бесплатности (в месяц):`}</Paragraph>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Количество операций: ${item.operationsNumber}`}</Paragraph>
          <Paragraph
            style={{ fontSize: "16px", margin: "0px 0px 10px 0px" }}
          >{`Сумма операций: ${item.operationsSum} BYN`}</Paragraph>
        </Card>
      </List.Item>
    );
  };

  return (
    <List
      itemLayout="horizontal"
      grid={{
        gutter: 20,
        column: packagesData.length,
      }}
      style={{
        margin: "0px 30px",
      }}
      dataSource={packagesData}
      renderItem={(item) => listItem(item)}
    />
  );
}
