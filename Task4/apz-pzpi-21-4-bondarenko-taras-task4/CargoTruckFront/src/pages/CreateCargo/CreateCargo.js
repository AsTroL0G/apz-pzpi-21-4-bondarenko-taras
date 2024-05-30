
import styles from './Stules.module.css';
import Modal from 'react-modal';
import React, { useState } from "react";
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer";
import { useNavigate } from 'react-router-dom';
import profile from "../../img/icon/6554725.png";
import { cityStore } from './../../Stores/CityStore';
import { cargoStore } from './../../Stores/CargoStore';
import ReceiverInfoDialog from '../../Components/ReceiverInfoDialog/ReceiverInfoDialog.js';

Modal.setAppElement('#root');

const CreateCargo = () => {
    const navigate = useNavigate();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [receiverInfo, setReceiverInfo] = useState({});
    const [citySenderId, setCitySenderId] = useState('');
    const [cityReceiverId, setCityReceiverId] = useState('');
    const [cargo, setCargo] = useState({});
    const userIdStorage = localStorage.getItem("login");

    const handleChange = (e) => {
      setCargo({ ...cargo, [e.target.name]: e.target.value });
    };

    const handleCityChange = async (e) => {
      const { name, value } = e.target;
      setCargo({ ...cargo, [name]: value });
      if (value) {
        try {
          await cityStore.fetchCity(value);
          if (cityStore.city) {
            if (name === 'citySenderId') {
              setCargo((prevCargo) => ({ ...prevCargo, citySenderId: cityStore.city.id }));
            } else if (name === 'cityReceiverId') {
              setCargo((prevCargo) => ({ ...prevCargo, cityReceiverId: cityStore.city.id }));
            }
          } else {
            alert('Місто не знайдено');
          }
        } catch (error) {
          console.error('Fetch city data error', error);
        }
      }
    };

    const handleSave = async () => {
      const updatedCargo = {
        ...cargo,
        senderId: userIdStorage,
        receiverId: receiverInfo
      };
      console.log(userIdStorage);
      console.log(receiverInfo);
      try {
        await cargoStore.createCargo(updatedCargo);
        navigate('/cabinet');
      } catch (error) {
        console.error('Create cargo error', error);
      }
    };

    const openSenderInfoDialog = () => {
      setIsModalOpen(true);
    };

    const closeSenderInfoDialog = () => {
      setIsModalOpen(false);
    };

    return (
      <>
        <NavBar />
        <div className={styles.container}>
          <div className={styles.buttons}>
            <ul className={styles.btn_list}>
              <li className={styles.btn_list__item}><a href="/cabinet" className={styles.btn_list__link}><img src={profile} className={styles.icon_btn} alt="" />Профіль</a></li>
              <li className="btn-list__item"><a href="/cabinet/outgoingCargo" className="btn-list__link">Вихідні вантажі</a></li>
              <li className="btn-list__item"><a href="/cabinet/incomingCargo" className="btn-list__link">Вхідні вантажі</a></li>
            </ul>
          </div>
          <div className={styles.createcargo}>
            <h1>Ствоорення вантажу</h1>
            <input type="text" name="name" placeholder="Назва" onChange={handleChange} />
            <input type="text" name="weight" placeholder="Вага" onChange={handleChange} />
            <input type="text" name="length" placeholder="Довжина" onChange={handleChange} />
            <input type="text" name="height" placeholder="Висота" onChange={handleChange} />
            <input type="text" name="width" placeholder="Ширина" onChange={handleChange} />
            <input type="text" name="announcedPrice" placeholder="Оголошена вартість" onChange={handleChange} />
            <input type="text" name="citySenderId" placeholder="Місто відправки" onBlur={handleCityChange} />
            <input type="text" name="addressSenderId" placeholder="Адрес відправки" onChange={handleChange} />
            <button onClick={openSenderInfoDialog}>Ввести інформацію про отримувача</button>
            <input type="text" name="cityReceiverId" placeholder="Місто отримання" onBlur={handleCityChange} />
            <input type="text" name="addressReceiverId" placeholder="Адрес отримання" onChange={handleChange} />
            <button onClick={handleSave}>Зберегти</button>
          </div>
          <Modal
            isOpen={isModalOpen}
            onRequestClose={closeSenderInfoDialog}
            contentLabel="Sender Info Dialog"
          >
            <ReceiverInfoDialog setReceiverInfo={setReceiverInfo} closeDialog={closeSenderInfoDialog} />
          </Modal>
        </div>
        <Footer />
      </>
    );
};

export default CreateCargo;
