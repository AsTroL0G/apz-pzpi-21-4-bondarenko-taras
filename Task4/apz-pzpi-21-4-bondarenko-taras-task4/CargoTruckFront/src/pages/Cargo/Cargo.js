import React, { useState, useEffect } from 'react';
import styles from './Stules.module.css';
import { useParams } from 'react-router-dom';
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer";
import { cargoStore } from './../../Stores/CargoStore';
import { userStore } from './../../Stores/UserStore';
import { noticeStore } from './../../Stores/NoticeStore';
import { valueStore } from './../../Stores/ValueStore';
import { sensorStore } from './../../Stores/SensorStore';
import GoogleMapComponent from "../../Components/GoogleMap/GoogleMap";

const Cargo = () => {
  const { id } = useParams();
  const [cargo, setCargo] = useState({});
  const [sender, setSender] = useState({});
  const [receiver, setReceiver] = useState({});
  const [latitude, setlatitude]  = useState({});
  const [longitude, setlongitude]  = useState({});


  const fetchCargos = async (cargoId) => {
    try {
      await cargoStore.fetchCargo(cargoId);
      setCargo(cargoStore.cargos);
      fetchUserSender(cargoStore.cargos.senderId);
      fetchUserReceiver(cargoStore.cargos.receiverId);
      fetchCoordinates(cargoStore.cargos.noticeId);
    } catch (error) {
      console.error('Fetch cargo data error', error);
    }
  };

  const fetchUserSender = async (userId) => {
    try {
      await userStore.fetchUser(userId);
      setSender(userStore.user);
    } catch (error) {
      console.error('Fetch user data error', error);
    }
  };

  const fetchUserReceiver = async (userId) => {
    try {
      await userStore.fetchUser(userId);
      setReceiver(userStore.user);
    } catch (error) {
      console.error('Fetch user data error', error);
    }
  };
  const fetchCoordinates = async (noticeId) => {
    try {
      await noticeStore.getNotice(noticeId);
      await sensorStore.getSensorByContainer(noticeStore.notice.containerId);
      await valueStore.getValueBySensorId(sensorStore.sensor.sensorId);
      const valueList = valueStore.value;
      const value = valueList[valueList.length - 1];
      const coordinates = value.values.split('/');
      setlatitude(parseFloat(coordinates[0]))
      setlongitude(parseFloat(coordinates[1]))    
      setReceiver(userStore.user);
    } catch (error) {
      console.error('Fetch user data error', error);
    }
  };

  useEffect(() => {
    if (id) {
      fetchCargos(id);
    }
  }, [id]);

  return (
    <>
      <NavBar />
      <div className={styles.container}>
        <div className={styles.cargocontainer}>
          <h1>Деталі вантажу</h1>
          <div className={styles.cargodetails}>
            <p>Номер посилки: {cargo.id}</p>
            <p>Опис посилки: {cargo.name}</p>
            <p>Вага: {cargo.weight} кг</p>
            <p>Розміри: {cargo.length}×{cargo.height}×{cargo.width}</p>
            <p>Вартість доставки: {cargo.shippingPrice} грн</p>
            <p>Статус: {cargo.status}</p>
            <h2>Відправник</h2>
            <p>Місто: {cargo.citySenderId}</p>
            <p>Адреса: {cargo.addressSenderId}</p>
            <p>Телефон: {sender.phoneNumber}</p>
            <p>ПІБ: {sender.patronym} {sender.lastName} {sender.firstName}</p>
            <h2>Одержувач</h2>
            <p>Місто: {cargo.cityReceiverId}</p>
            <p>Адреса: {cargo.addressReceiverId}</p>
            <p>Телефон: {receiver.phoneNumber}</p>
            <p>ПІБ: {receiver.patronym} {receiver.lastName} {receiver.firstName}</p>
          </div>
          
          {cargo.status === 'В дорозі' && latitude && longitude ? (
            <div className={styles.mapcontainer}>
              <GoogleMapComponent lat={latitude} lng={longitude} />
            </div>
          ) : (
            <div>На дани момент відстеження не можливе</div>
          )}
        </div>
      </div>
      <Footer />
    </>
  );
};

export default Cargo;



