
import styles from './Stules.module.css';
import React, {useEffect, useState} from "react";
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer"
import axios from 'axios';
import profile from "../../img/icon/6554725.png"
import ParcelItem from "../../Components/ParcelItem/ParcelItem"
import { cargoStore } from './../../Stores/CargoStore';
import { useNavigate,useLocation   } from "react-router-dom";

const MyOrders = () => {
    const navigate = useNavigate();
    var userIdStorage = localStorage.getItem("login");
  
    const [cargoSender, setCargoSender] = useState([]);

    const fetchCargosBySender = async (userId) => {
        try {
            await cargoStore.fetchCargosBySender(userId);
            setCargoSender(cargoStore.cargos);
        } catch (error) {
          console.error('Fetch user data error', error);
        }
    };

    useEffect(() => {
    if (userIdStorage) {
        fetchCargosBySender(userIdStorage)
    }
    }, []);
    const handleParcelClick = (id) => {
        navigate(`/cargo/${id}`);
    };
    if (!cargoSender) {
        return <h1>...Loading</h1>
    } 

    
    return ( 
        <>
        <NavBar/>
        <div className={styles.container}>

        

                <div className={styles.buttons}>
                    <ul className={styles.btn_list}>
                        <li className={styles.btn_list__item}><a href="/cabinet" className={styles.btn_list__link}><img src={profile} className={styles.icon_btn} alt=""/>Профіль</a></li>
                        <li className="btn-list__item"><a href="/cabinet/incomingCargo" className="btn-list__link">Вхідні вантажі</a></li>        
                        <li className="btn-list__item"><a href="/cabinet/createCargo" className="btn-list__link">Створити замовлення на перевезення</a></li>   
                    </ul>
                </div>

                <div className={styles.last}>
                    <h1 className={styles.last_header}></h1>


                    <div className={styles.parcellist}>
                        <table>
                            <thead>
                            <tr>
                                <th className={styles.number}>Номер</th>
                                <th className={styles.status}>Статус</th>
                                <th>Вага</th>
                                <th>Опис відправлення</th>    
                                <th>Місто відправлення</th>
                                <th>Адреса відправлення</th>
                                <th>Місто отримання</th>
                                <th>Адреса отримання</th>
                            </tr>
                            </thead>
                            <tbody>
                            {cargoSender.map((parcel, index) => (
                                <ParcelItem key={index} {...parcel}  onClick={handleParcelClick}/>
                            ))}
                            </tbody>
                        </table>
                    </div>


                </div>

        </div>
        <Footer/>
        </>
     );
}
 
export default MyOrders;