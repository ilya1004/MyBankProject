// import {
//   Typography,
//   Button,
//   Flex,
//   Card,
//   Col,
//   Row,
//   Input,
//   Select,
//   Popover,
// } from "antd";
// import { QuestionCircleOutlined } from "@ant-design/icons";
// import { useState } from "react";
// import { useLoaderData, Link, useNavigate } from "react-router-dom";
// import axios from "axios";
// import { BASE_URL } from "../../../Common/Store/constants";
// import {
//   handleResponseError,
//   showMessageStc,
// } from "../../../Common/Services/ResponseErrorHandler";;

// const { Text, Title } = Typography;

// const colTextWidth = "200px";
// const colInputWidth = "200px";


// export default function AddCreditPage() {
// 	// const [name, setName] = useState()
// 	// const [grantedAmount, set]

// 	return (
//     <>
//       <Flex
//         vertical
//         align="center"
//         justify="flex-start"
//         style={{
//           minHeight: "90vh",
//         }}
//       >
//         <Flex align="center" gap={30} style={{ margin: "0px 0px 10px 0px" }}>
//           <Link to="/credits">
//             <Button style={{ margin: "18px 0px 0px 20px" }}>Назад</Button>
//           </Link>
//           <Title level={3}>Оформление кредита</Title>
//         </Flex>
//         <Card
//           title="Введите данные"
//           style={{
//             // width: "550px",
//             // height: "400px",
//           }}
//         >
//           <Flex vertical gap={16} style={{ width: "100%" }} align="center">
//             <Row gutter={[16, 16]}>
//               <Col style={{ width: colTextWidth }}>
//                 <Text style={{ fontSize: "16px" }}>Название:</Text>
//               </Col>
//               <Col style={{ width: colInputWidth }}>
//                 <Input
//                   onChange={handleCardName}
//                   value={cardName}
//                   style={{ width: "210px" }}
//                   count={{
//                     show: true,
//                     max: 20,
//                     exceedFormatter: (txt, { max }) => txt.slice(0, max),
//                   }}
//                 />
//               </Col>
//             </Row>
//             <Row gutter={[16, 16]}>
//               <Col style={{ width: colTextWidth }}>
//                 <Text style={{ fontSize: "16px" }}>Пин-код:</Text>
//               </Col>
//               <Col style={{ width: colInputWidth }}>
//                 <Input
//                   style={{ width: "130px" }}
//                   onChange={handlePincode}
//                   value={pincode}
//                   count={{
//                     show: true,
//                     max: 4,
//                     exceedFormatter: (txt, { max }) => txt.slice(0, max),
//                   }}
//                 />
//               </Col>
//             </Row>
//             <Row gutter={[16, 16]}>
//               <Col style={{ width: colTextWidth }}>
//                 <Text style={{ fontSize: "16px" }}>Срок действия:</Text>
//               </Col>
//               <Col style={{ width: colInputWidth }}>
//                 <Select
//                   defaultValue="3 года"
//                   style={{ width: "130px" }}
//                   onChange={handleDuration}
//                   options={listDurations}
//                 />
//               </Col>
//             </Row>
//             <Row gutter={[16, 16]}>
//               <Col style={{ width: colTextWidth }}>
//                 <Text style={{ fontSize: "16px" }}>Пакет услуг:</Text>
//               </Col>
//               <Col style={{ width: colInputWidth }}>
//                 <Select
//                   defaultValue=""
//                   style={{ minWidth: "170px" }}
//                   onChange={handleCardPackage}
//                   options={cardPackagesData}
//                 />
//               </Col>
//             </Row>
//             <Row gutter={[16, 16]}>
//               <Col style={{ width: colTextWidth }}>
//                 <Text style={{ fontSize: "16px" }}>{"Лицевой счет "}</Text>
//                 <Popover
//                   content={
//                     <Flex vertical>
//                       <Text>Эта карта будет привязана к</Text>
//                       <Text>выбранному лицевому счету</Text>
//                     </Flex>
//                   }
//                 >
//                   <QuestionCircleOutlined
//                     style={{ opacity: "0.5" }}
//                     twoToneColor={"#000000"}
//                   />
//                 </Popover>
//                 <Text style={{ fontSize: "16px" }}>:</Text>
//               </Col>
//               <Col style={{ width: colInputWidth }}>
//                 <Select
//                   defaultValue=""
//                   style={{ minWidth: "170px" }}
//                   onChange={handlePersonalAccount}
//                   options={userAccountsData}
//                 />
//               </Col>
//             </Row>
//           </Flex>
//           <Flex
//             gap={20}
//             align="center"
//             justify="center"
//             style={{
//               margin: "30px 0px 0px 0px",
//             }}
//           >
//             <Button onClick={handleCancel}>Отмена</Button>
//             <Button type="primary" onClick={handleEnter}>
//               Добавить
//             </Button>
//           </Flex>
//         </Card>
//       </Flex>
//     </>
//   );
// }