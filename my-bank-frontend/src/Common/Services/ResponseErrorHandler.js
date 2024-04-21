import { message } from "antd";

export const handleResponseError = (response) => {
  if (response.status === 401) {
    console.error(response);
    showMessageStc("Произошла ошибка аутентификации", "error");
  } else if (response.status === 403) {
    console.error(response);
    showMessageStc("Произошла ошибка авторизации", "error");
  } else if (response.status === 500) {
    console.error(response);
    showMessageStc("Произошла ошибка на сервере", "error");
  } else {
    console.error(response);
    showMessageStc("Произошла неизвестная ошибка", "error");
  }
};

export const showMessageStc = (msg, msgType) => {
  message.config({
    top: 55,
    duration: 3,
    maxCount: 1,
  });
  const data = {
    type: msgType,
    content: msg,
  };
  if (msgType === "error") {
    message.error(data);
  } else if (msgType === "success") {
    message.success(data);
  } else {
    message.info(data);
  }
};
