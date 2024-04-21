import { useState, useCallback } from "react";

export const useCreateAppContext = function (props) {
  const [test, setTest] = useState(props.test || "Hello world11");

  return {
    test,
    setTest,
  };
};
