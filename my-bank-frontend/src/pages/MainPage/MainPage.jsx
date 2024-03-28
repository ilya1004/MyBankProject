import "./MainPage.css";

import { Title, List, Card, Flex, InputNumber, Dropdown } from "antd";

var list = ["Name", 234.234, 30, 1900, 500];

var str = "Название валюты"
var code = "CODE"

var otherCurrencies = [
  {
    label: <Text>{`${code} ${qwe}`}</Text>,
    key: "0"
  }
];

export default function MainPage() {
  return (
    <div>
      <h1>MainPage</h1>
      <List>
        <Card>
          <Title>{list[0]}</Title>
          <Title level={2}>{`${list[1]} BYN`}</Title>
          <Title level={5}>{"Условия бесплатности (в месяц):"}</Title>
          <Text>{`Количество операций: ${list[2]}`}</Text>
          <Text>{`Сумма операций: ${list[3]}`}</Text>
          <Text>{`Средний остаток по счету: ${list[4]}`}</Text>
        </Card>
      </List>

      <Card>
        <List>
          <List.Item>
            <Flex justify="flex-start" align="center">
              <Title>USD</Title>
              <InputNumber defaultValue={1} placeholder="Введите значение" />
            </Flex>
          </List.Item>

          <List.Item>
            <Flex justify="flex-start" align="center">
              <Title>EUR</Title>
              <InputNumber defaultValue={0} placeholder={0} />
            </Flex>
          </List.Item>

          <List.Item>
            <Flex justify="flex-start" align="center">
              <Title>RUB</Title>
              <InputNumber defaultValue={0} placeholder={0} />
            </Flex>
          </List.Item>

          <List.Item>
            <Flex justify="flex-start" align="center">
              <Title>PLN</Title>
              <InputNumber defaultValue={0} placeholder={0} />
            </Flex>
          </List.Item>
        </List>
        <Dropdown 
          menu={{ otherCurrencies }}
          trigger={["click"]}
          >
            Добавить валюту
          </Dropdown>
      </Card>
    </div>
  );
}
