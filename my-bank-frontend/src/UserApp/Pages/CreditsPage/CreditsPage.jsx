import axios from "axios";
import { useEffect, useState } from "react";
import { Card, Flex, Typography, Button, Input, Table, Tag } from "antd";
import { CheckCircleTwoTone, CloseCircleTwoTone } from "@ant-design/icons";
import { Link, useLoaderData, useNavigate, redirect } from "react-router-dom";
import { BASE_URL } from "../../../Common/Store/constants";
import {
  handleResponseError,
  showMessageStc,
} from "../../../Common/Services/ResponseErrorHandler";

const { Column } = Table;
const { Title, Text } = Typography;

const getCreditsData = async () => {
  const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
  });
  try {
    const res = await axiosInstance.get(
      `PersonalAccounts/GetAllInfoByCurrentUser?includeData=${true}`
    );
    return { accountsData: res.data.list, error: null };
  } catch (err) {
    if (err.response.status === 401) {
      return { accountsData: null, error: err.response };
    }
    handleResponseError(err.response);
    return { accountsData: null, error: err.response };
  }
};

export async function loader() {
  const { accountsData, error } = await getCreditsData();
  if (!accountsData) {
    if (error.status === 401 || error.status === 403) {
      return redirect("/login");
    } else {
      throw new Response("", {
        status: error.status,
      });
    }
  }
  return { accountsData };
}


export default function CreditsPage() {
  return (
    <>
    </>
  );
}
