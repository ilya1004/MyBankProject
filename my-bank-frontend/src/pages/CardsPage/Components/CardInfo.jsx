import { Typography, Button } from "antd";
import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import axios from "axios";
import { BASE_URL } from "../../../store/constants";

const { Title } = Typography;



export default function CardInfo() {
  const [cardData, setCardData] = useState(null);
  const { cardId } = useParams();

  useEffect(() => {
    const getCardData = async () => {
      const axiosInstance = axios.create({
        baseURL: BASE_URL,
        withCredentials: true,
      });
      axiosInstance
        .get(`Cards/GetInfoByCurrentUser/?cardId=${cardId}&includeData=${true}`)
        .then((response) => {
          console.log(response.data["item"]);
          setCardData(response.data["item"]);
        })
        .catch((err) => {
          console.error(err);
        });
    };
    getCardData();
  }, []);

  return (
    <>
      <Link to="/cards">
          <Button>Назад</Button>
        </Link>
      <Title>{cardId}</Title>
    </>
  );
}
