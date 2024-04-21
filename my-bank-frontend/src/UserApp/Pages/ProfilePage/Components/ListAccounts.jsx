import { Card, Flex, List, Typography } from "antd";
import { useNavigate } from "react-router-dom";

const { Title, Text } = Typography;

export default function ListAccounts(props) {
  const navigate = useNavigate();
  const convertNumberAccount = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res.trim();
  };

  const listItemPersonalAccount = (item) => {
    return (
      <List.Item key={item["id"]}>
        <Card
          onClick={() => {
            navigate(`/accounts/${item.id}`);
          }}
          hoverable
          style={{
            width: "310px",
            height: "150px",
            backgroundColor: "#FFF5DF",
          }}
        >
          <Flex
            gap={10}
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
          Мои счета
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
          gutter: 0,
          column: 2,
        }}
        dataSource={props.value}
        renderItem={(item) => listItemPersonalAccount(item)}
      ></List>
    </Card>
  );
}
