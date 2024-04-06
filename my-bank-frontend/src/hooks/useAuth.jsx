import { useContext } from "react";
import { AuthContext } from "../store/AuthContext/AuthProvider";

export default function useAuth() {
  return useContext(AuthContext);
}
