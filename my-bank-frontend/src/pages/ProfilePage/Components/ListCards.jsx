import { Card, Flex, List, Typography } from "antd";

const { Title, Text } = Typography;

export default function ListCards(props) {
  const convertDate = (date) => {
    let dateObj = new Date(date);
    let year = dateObj.getUTCFullYear().toString().substring(2, 4);
    let month = "";
    if (dateObj.getUTCMonth() < 10) {
      month = "0" + dateObj.getMonth().toString();
    } else {
      month = dateObj.getMonth().toString();
    }

    return `${month}/${year}`;
  };

  const convertNumberCard = (number) => {
    let numStr = number.toString();
    return `${numStr.substring(0, 4)} ${numStr.substring(
      4,
      8
    )} ${numStr.substring(8, 12)} ${numStr.substring(12, 16)}`;
  };

  const listItemCard = (item) => {
    // let colorsTest = ["#C9D6AD", "#C6D8C3", "#E3CAB8", "#E3E0B8"];
    let colors = ["#C9D6AD", "#E3E0B8"];
    return (
      <List.Item
        key={item["id"]}
        style={{
          width: "300px",
          height: "100px",
        }}
      >
        <Card
          hoverable
          bordered={false}
          style={{
            width: "230px",
            height: "135px",
            backgroundColor: colors[0],
          }}
        >
          <Flex
            gap={10}
            // justify="flex-start"
            // align="center"
            vertical
            style={{
              width: "100%",
            }}
          >
            <Title
              level={3}
              style={{
                margin: "0px 0px 10px 0px",
              }}
            >
              {item["name"]}
            </Title>
            <Text>{convertNumberCard(item["number"])}</Text>
            <Text>{convertDate(item["expirationDate"])}</Text>
          </Flex>
        </Card>
      </List.Item>
    );
  };

  return (
    <Card
      title={
        <Title level={3} style={{ margin: "0px" }}>
          Мои карты
        </Title>
      }
      type="inner"
      style={{
        width: "90%",
        height: "240px",
      }}
    >
      <List
        itemLayout="horizontal"
        grid={{
          gutter: 15,
          // column: userData["cards"].length,
          column: 3,
        }}
        dataSource={props.value}
        renderItem={(item) => listItemCard(item)}
      ></List>
    </Card>
  );
}
