import axios from "axios";
import { useState } from "react";
import { Card, Upload, Flex, Image, Typography, Button, Modal } from "antd";
import { useNavigate, useLoaderData } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";
import PackagesTable from "./Components/PackagesList";

const { Title, Text } = Typography;

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
  if (!packagesData) {
    throw new Response("", {
      status: error.status,
    });
  }
  return { packagesData };
}

export default function ManagementPage() {
  const { packagesData } = useLoaderData();

  const navigate = useNavigate();

  const handleAddPackage = () => {
    navigate("add-package");
  };

  const handleEditPackage = () => {
    navigate("edit-package");
  };

  const handleDeletePackage = () => {
    navigate("delete-package");
  };

  return (
    <>
      <Flex
        align="center"
        justify="flex-start"
        style={{ minHeight: "82vh" }}
        vertical
      >
        <Flex justify="center" align="center" vertical>
          <Flex
            style={{ width: "80%", margin: "0px 0px 10px 30px" }}
          >
            <Title level={2}>
              Пакеты карт
            </Title>
          </Flex>
          <PackagesTable packagesData={packagesData} />
          <Flex
            justify="flex-start"
            gap={20}
            style={{ width: "80%", margin: "20px 0px 0px 20px" }}
          >
            <Button type="primary" onClick={handleAddPackage}>
              Добавить
            </Button>
            <Button onClick={handleEditPackage}>Редактировать</Button>
            <Button onClick={handleDeletePackage} danger>
              Удалить
            </Button>
          </Flex>
        </Flex>
      </Flex>
    </>
  );
}
