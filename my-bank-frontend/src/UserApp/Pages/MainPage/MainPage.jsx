import React from "react";
import { Typography, Divider, Flex } from "antd";
import PackagesList from "./Components/PackagesList";
import CurrencyConventer from "./Components/CurrencyConventer";
import axios from "axios";
import { useLoaderData, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Title, Text } = Typography;

const getCurrencyRatesData = async () => {
  const axiosInstance = axios.create();
  try {
    const res = await axiosInstance.get(
      `https://api.nbrb.by/exrates/rates?periodicity=0`
    );
    return { currenciesData: res.data, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { currenciesData: null, error: err.response };
  }
};

const getPackagesData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(`CardPackages/GetAllInfo`);
    return { packagesData: res.data.list, error: null };
  } catch (err) {
    handleResponseError(err.response);
    return { packagesData: null, error: err.response };
  }
};

export async function loader() {
  const { packagesData, error } = await getPackagesData();
  const { currenciesData, error: error1 } = await getCurrencyRatesData();
  if (!packagesData) {
    throw new Response("", {
      status: error.status,
    });
  }
  if (!currenciesData) {
    showMessageStc(
      "Произошла ошибка при получении данных о курсах валют.",
      "error"
    );
  }
  return { packagesData, currenciesData };
}

export default function MainPage() {
  const { packagesData, currenciesData } = useLoaderData();

  return (
    <>
      <Flex vertical>
        <Title level={2} align="center">
          Конвертер валют
        </Title>
        <CurrencyConventer currenciesData={currenciesData} />
        <Divider />
        <Title level={2} align="center">
          Пакеты карт
        </Title>
        <Flex align="center" justify="flex-start" style={{ margin: "0px 0px 0px 60px", width: "90%"}}>
          <PackagesList packagesData={packagesData} />
        </Flex>
      </Flex>
    </>
  );
}
