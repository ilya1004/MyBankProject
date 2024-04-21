import { Card, Flex, List, Typography } from "antd";
import { widthCardAcc, heightCardAcc } from "../ProfilePage";

const { Title, Text } = Typography;

export default function ListCredits(props) {
  const convertNumberAccount = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res;
  };

  const listItemCreditAccount = (item) => {
    return (
      <List.Item key={item["id"]}>
        <Card
          hoverable
          style={{
            width: widthCardAcc,
            height: heightCardAcc,
            backgroundColor: "#FFF5DF",
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
            <Text>{convertNumberAccount(item["number"])}</Text>
            <Text
              style={{ fontSize: "18px" }}
            >{`${item["currentBalance"]} ${item["currency"]["code"]}`}</Text>
          </Flex>
        </Card>
      </List.Item>
    );
  };
  return (
    <Card
      title={
        <Title level={3} style={{ margin: "0px" }}>
          Мои кредиты
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
        renderItem={(item) => listItemCreditAccount(item)}
      ></List>
    </Card>
  );
}
