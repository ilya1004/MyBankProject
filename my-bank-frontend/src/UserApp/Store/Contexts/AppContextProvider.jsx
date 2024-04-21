import React, { createContext, useContext } from "react";
import { useCreateAppContext } from "./AppContext";

export const Context = createContext("qeere");

export const AppContextProvider = ({ children, ...props }) => {
  const context = useCreateAppContext(props);
  return <Context.Provider value={context}>{children}</Context.Provider>;
};

export function useAppContext() {
  const context = useCreateAppContext(Context);
  if (!context) throw new Error("Use app context within provider!");
  return context;
}
