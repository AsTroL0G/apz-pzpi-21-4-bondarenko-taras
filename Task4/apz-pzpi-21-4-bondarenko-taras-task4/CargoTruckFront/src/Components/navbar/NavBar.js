import React, {useEffect, useState} from "react";
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import logo from "./../../img/icon/logo.png"; 
import sign_in from "./../../img/icon/sign_in.png"; 
import avatar from "./../../img/icon/avatar.png";
import lang from "./../../img/icon/lang.png";
import { userStore } from './../../Stores/UserStore';

import "./styles.css";

const NavBar = () => {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();
  const[user,setUser] = useState([]);
  const [userRole, setUserRole] = useState("");

  var userIdStorage = localStorage.getItem("login");
  const handleLanguageChange = (event) => {
    i18n.changeLanguage(event.target.value);
  };

  const fetchUserData = async (userId) => {
    try {
      await userStore.fetchUser(userId);
      const fetchedUser = userStore.user;
      setUser(fetchedUser);

      if (fetchedUser && fetchedUser.roles && fetchedUser.roles.length > 0) {
        setUserRole(fetchedUser.roles[0].name);
      }
    } catch (error) {
      console.error('Fetch user data error', error);
    }
  };
  


  useEffect(() => {
    if (userIdStorage) {
        fetchUserData(userIdStorage);
    }
    }, []);

  return (
    <>
      <nav className="nav">
        <div className="container">
          <div className="nav-row">
            <a href="/" className="logo"><img src={logo} alt="Logo" /></a>
            <ul className="nav-list">
              <li className="nav-list__item">
                <img className="icon_header" src={lang} alt="Language Icon" />
                <select id="language" onChange={handleLanguageChange} defaultValue={i18n.language}>
                  <option value="ua">Українська</option>
                  <option value="en">English</option>
                </select>
              </li>
              {userRole === "Логіст" && (
                <li className="nav-list__item">
                  <a href="/cabinet/logist" className="nav-list_sign">
                    <img className="icon_header" />
                    {t("navbar.logist")}
                  </a>
                </li>
              )}
              {userRole === "Адміністратор" && (
                <li className="nav-list__item">
                  <a href="/admin" className="nav-list_sign">
                    <img className="icon_header" />
                    {t("navbar.admin")}
                  </a>
                </li>
              )}
              {userIdStorage ? (
                <>
                  <li className="nav-list__item">
                    <a href="/cabinet" className="nav-list_user">
                      <img className="icon_user" src={avatar} alt="User Icon" />
                    </a>
                  </li>
                </>
              ) : (
                <>
                  <li className="nav-list__item">
                    <a href="/login" className="nav-list_sign">
                      <img className="icon_header" src={sign_in} alt="Sign In Icon" />
                      {t("navbar.login")}
                    </a>
                  </li>
                  <li className="nav-list__item">
                    <a href="/register" className="nav-list_sign">{t("navbar.register")}</a>
                  </li>
                </>
              )}
            </ul>
          </div>
        </div>
      </nav>
    </>
  );
};

export default NavBar;
