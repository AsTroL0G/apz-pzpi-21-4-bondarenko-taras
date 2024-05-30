
import edit from "./../../img/icon/edit_icon.png"
import exit from "./../../img/icon/exit_icon.png"
import avatar from "./../../img/icon/avatar.png"
import "./style.css"; 
import React, {useEffect, useState} from "react";
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer"
import { userStore } from './../../Stores/UserStore';
import { cargoStore } from './../../Stores/CargoStore';


const Cabinet = () => {

    const [cargoSender, setCargoSender] = useState(0);
    const [cargoReceiver, setCargoReceiver] = useState(0);
    const[user,setUser] = useState([]);
    var userIdStorage = localStorage.getItem("login");

    const fetchUserData = async (userId) => {
        try {
        await userStore.fetchUser(userId);
        setUser(userStore.user);
        } catch (error) {
        console.error('Fetch user data error', error);
        }
    };
    const fetchCargosBySender = async (userId) => {
        try {
            await cargoStore.fetchCargosBySender(userId);
            setCargoSender(cargoStore.cargos.length);
        } catch (error) {
            console.error('Fetch user data error', error);
        }
    };
    const fetchCargosByReceiver = async (userId) => {
        try {
            await cargoStore.fetchCargosByReceiver(userId);
            setCargoReceiver(cargoStore.cargos.length);
        } catch (error) {
          console.error('Fetch user data error', error);
        }
    };

    useEffect(() => {
    if (userIdStorage) {
        fetchUserData(userIdStorage);
        fetchCargosBySender(userIdStorage)
        fetchCargosByReceiver(userIdStorage)
    }
    }, []);


        if (!user && !cargoSender && !cargoReceiver) {
            return <h1>...Loading</h1>
        } 

        function exit_function() {
            localStorage.removeItem("login");
        }

    return ( 
        <>
        <NavBar/>
        <div className="container">
            <div className="cabinet">
                <img className="profile-photo" src={avatar} alt="Profile" />
                <div>
                    <div className="profile-info">
                        <div className="name">
                            <h1 className="h1-name">{user.firstName}</h1>
                        
                            <h1 className="h1-name">{user.lastName}</h1>
                        </div>
                    
                        <div className="level">
                            <h1 className="level__label">Рівень {Math.floor(20/100)+1}</h1>
                            <progress className="level__bar" max={100} value={user.experience%100}/>
                        </div>
                    </div>
                </div>

                <div className="additional">
                    <h2>Кількість відправелених вантажів</h2>
                    <h3 className="info">{cargoSender}</h3>
                </div>
                
                <div className="additional">
                    <h2>Кількість отриманих вантажів</h2>
                    <h3 className="info">{cargoReceiver}</h3>
                </div>

                <div className="btns">
                    <div className="edit"><a href="/cabinet/editprofile"><img src={edit} alt="edit"/></a></div>
                    <div onClick={exit_function}  className="exit"><a href="/"><img src={exit} alt="exit"/></a></div>
                </div>
            </div>
        </div>

        <div className="buttons">
            <ul className="btn-list">
                <li className="btn-list__item"><a href="/cabinet/incomingCargo" className="btn-list__link">Вхідні вантажі</a></li>          
                <li className="btn-list__item"><a href="/cabinet/outgoingCargo" className="btn-list__link">Вихідні вантажі</a></li>
                <li className="btn-list__item"><a href="/cabinet/createCargo" className="btn-list__link">Створити замовлення на перевезення</a></li>               
            </ul>
        </div>


        <Footer/>
        </>
        );
}
    
export default Cabinet;