import { Typography, Table, Tag } from "antd";

const { Text } = Typography;
const { Column } = Table;

export default function PackagesTable({ packagesData }) {
  return (
    <>
      <Table
        dataSource={packagesData}
        style={{ width: "80%" }}
        pagination={{ position: ["none", "none"] }}
      >
        <Column
          title="Номер"
          dataIndex="id"
          key="id"
          width={"150px"}
          render={(id) => <Text>{id.toString().padStart(4, "0")}</Text>}
        />
        <Column
          title="Название"
          dataIndex="name"
          key="name"
          width={"400px"}
          render={(name) => <Text>{name}</Text>}
        />
        <Column
          title="Цена"
          dataIndex="price"
          key="price"
          width={"300px"}
          render={(price) => <Text>{`${price.toString()} BYN`}</Text>}
        />
        <Column
          title="Количество операций"
          dataIndex="operationsNumber"
          key="operationsNumber"
          width={"300px"}
          render={(operationsNumber) => (
            <Text>{operationsNumber.toString()}</Text>
          )}
        />
        <Column
          title="Сумма операций"
          dataIndex="operationsSum"
          key="operationsSum"
          width={"300px"}
          render={(operationsSum) => <Text>{operationsSum.toString()}</Text>}
        />
        <Column
          title="Статус"
          dataIndex="isActive"
          key="status"
          width={"200px"}
          render={(isActive) =>
            isActive === true ? (
              <Tag color={"green"} key="active">
                Активен
              </Tag>
            ) : (
              <Tag color={"red"} key="no-active">
                Неактивен
              </Tag>
            )
          }
        />
      </Table>
    </>
  );
}
