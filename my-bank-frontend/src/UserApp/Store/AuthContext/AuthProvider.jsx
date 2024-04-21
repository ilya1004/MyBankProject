import { createContext, useState } from "react";

export const Role = {
  User: "user",
  Moderator: "moderator",
  Admin: "admin",
};

export const AuthContext = createContext({
  isAuthenticated: false,
  role: Role.Moderator,
  id: -1,
  setAuth: () => {},
  setRole: () => {},
  setId: () => {},
});

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setAuth] = useState(false);
  const [role, setRole] = useState("");
  const [id, setId] = useState(-1);

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, setAuth, role, setRole, id, setId }}
    >
      {children}
    </AuthContext.Provider>
  );
};
