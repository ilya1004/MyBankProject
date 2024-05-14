import { Card, Flex, List, Typography } from "antd";
import { widthCardAcc, heightCardAcc } from "../ProfilePage";
import { useNavigate } from "react-router-dom";

const { Title, Text } = Typography;

export default function ListCredits(props) {
  const navigate = useNavigate();
  const convertNumberAccount = (number) => {
    let numStr = number.toString();
    let res = "";
    for (let i = 0; i < 28; i += 4) {
      res += `${numStr.substring(i, i + 4)} `;
    }
    return res;
  };

  const listItemCreditAccount = (item) => {
    let colors = ["#C9D6AD", "#E3E0B8"];
    return (
      <List.Item key={item.id}>
        <Card
          hoverable
          onClick={() => {
            navigate(`/credits/${item.id}`);
          }}
          style={{
            width: widthCardAcc,
            height: heightCardAcc,
            backgroundColor: colors[0],
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
              {item.name}
            </Title>
            <Text>{convertNumberAccount(item.number)}</Text>
            <Text
              style={{ fontSize: "18px" }}
            >{`${item.currentBalance} ${item.currency.code}`}</Text>
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
      <Flex
        align="center"
        justify="flex-start"
        style={{ width: "100%", margin: "0px 0px 0px 50px" }}
      >
        <List
          itemLayout="horizontal"
          grid={{
            gutter: 90,
            column: 2,
          }}
          dataSource={props.value}
          renderItem={(item) => listItemCreditAccount(item)}
        ></List>
      </Flex>
    </Card>
  );
}
