import news from "./../../img/icon/news.jpg";
import newsreng from "./../../img/icon/newseng.png";
import pobutivi from "./../../img/icon/pobutivi.jpg";
import professional from "./../../img/icon/professional.jpg";
import promuslovi from "./../../img/icon/promuslovi.jpg";
import { useTranslation } from 'react-i18next';
import React, {useEffect, useState} from "react";
import axios from 'axios';
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer"
import styles from './Stules.module.css';


const  MainPge = () => {
    const { t, i18n } = useTranslation();
    const [language, setLanguage] = useState(localStorage.getItem("language"));
    console.log(language)
    return (
        <main className={styles.section}>
            <NavBar/>
                <>
                <div className={styles.container}>
                    <img className={styles.news} src={news} alt="Link"/>
                    <h2 className={styles.text_header}>{t("mainpage.services")}</h2>
                    <ul className={styles.services}>
                    <li>
                        <h3>{t("mainpage.titleOne")}</h3>
                        <p>{t("mainpage.textTitleOne")}</p>
                    </li>
                    <li>
                        <h3>{t("mainpage.titleTwo")}</h3>
                        <p>{t("mainpage.textTitleTwo")}</p>
                    </li>
                    <li>
                        <h3>{t("mainpage.titleThree")}</h3>
                        <p>{t("mainpage.textTitleThree")}</p>
                    </li>
                    </ul>

                
                </div>

                <div className={styles.create__tour}>
                    <div className={styles.container}>
                        <img className={styles.foto_create_tour} src="https://helion.com.ua/wp-content/uploads/2021/02/%D0%AF%D0%BA-%D0%BF%D1%96%D0%B4%D0%B3%D0%BE%D1%82%D1%83%D0%B2%D0%B0%D1%82%D0%B8-%D0%B2%D0%B0%D0%BD%D1%82%D0%B0%D0%B6-%D0%B4%D0%BE-%D0%BF%D0%B5%D1%80%D0%B5%D0%B2%D0%B5%D0%B7%D0%B5%D0%BD%D0%BD%D1%8F-%D1%80%D1%96%D0%B7%D0%BD%D0%B8%D0%BC%D0%B8-%D0%B2%D0%B8%D0%B4%D0%B0%D0%BC%D0%B8-%D1%82%D1%80%D0%B0%D0%BD%D1%81%D0%BF%D0%BE%D1%80%D1%82%D1%83--scaled.jpg" alt="Link"/>
                        
                            <div className={styles.text_create_tour}>{t("mainpage.textTitleFour")}

                                <a href="/redactor" className={styles.link_create_tour}>{t("mainpage.titleFour")}</a>
                            </div>
                    </div> 
                </div>
                
                </>

                <Footer/>
        </main>
        
);
}
 
export default MainPge;