import React, { useEffect, useState } from "react";
import {
  Typography,
  List,
  Card,
  Flex,
  InputNumber,
  Dropdown,
  Space,
  Divider,
} from "antd";
import { DownOutlined } from "@ant-design/icons";
import PackagesList from "./Components/PackagesList";
import CurrencyConventer from "./Components/CurrencyConventer";
import "./MainPage.css";
import axios from "axios";

const { Title, Text } = Typography;

const BASE_URL = `https://localhost:7050/api/`;

export default function MainPage() {
  return (
    <div>
      <Title align="center">Пакеты карт</Title>
      <PackagesList />
      <Divider />

      <CurrencyConventer />
    </div>
  );
}
