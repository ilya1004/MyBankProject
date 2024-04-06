import { BrowserRouter as Router, useLocation, Outlet, Navigate  } from "react-router-dom";
// import {createContext, useContext, useState} from "react"
import useAuth from "../../hooks/useAuth.jsx";


export const PrivateRoute = () => {
  const { isAuthenticated } = useAuth()

  const location = useLocation()

  return (
    // Если пользователь авторизован, то рендерим дочерние элементы текущего маршрута, используя компонент Outlet
    isAuthenticated === true ?
      <Outlet />
      // Если пользователь не авторизован, то перенаправляем его на маршрут /login с помощью компонента Navigate.
      // Свойство replace указывает, что текущий маршрут будет заменен на новый, чтобы пользователь не мог вернуться обратно, используя кнопку "назад" в браузере.
      :
      <Navigate to="/login" state={{ from: location }} replace />
  )
};