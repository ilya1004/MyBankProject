import { useContext } from "react";
import { AuthContext } from "../UserApp/Store/AuthContext/AuthProvider";

export default function useAuth() {
  return useContext(AuthContext);
}
