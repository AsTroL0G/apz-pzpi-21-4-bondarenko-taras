import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer";
import styles from './Stules.module.css';
import { cargoStore } from './../../Stores/CargoStore';
import TripDialog from '../../Components/TripDialog/TripDialog.js';
import SheduleDialog from '../../Components/SheduleDialog/SheduleDialog.js';
import NoticeDialog from '../../Components/NoticeDialog/NoticeDialog.js';
import Modal from 'react-modal';

Modal.setAppElement('#root');

const Logist = () => {
  const { t } = useTranslation();
  const [cargos, setCargos] = useState([]);
  const [coordinates, setCoordinates] = useState({ lat: '', lng: '' });
  const [notice, setNotice] = useState({});
  const [isTripDialogOpen, setIsTripDialogOpen] = useState(false);
  const [isSheduleDialogOpen, setIsSheduleDialogOpen] = useState(false);
  const [isNoticeDialogOpen, setIsNoticeDialogOpen] = useState(false);
  const [nearestContainer, setNearestContainer] = useState(null);
  const [receiverInfo, setReceiverInfo] = useState({});
  const [noticeInfo, setNoticeInfo] = useState({});
  const [shedulerInfo, setSheduleInfo] = useState({});

  const navigate = useNavigate();
  
  useEffect(() => {
    fetchCargosByReceiver();
  }, []);

  const fetchCargosByReceiver = async () => {
    try {
      await cargoStore.fetchCargoStatus();
      setCargos(cargoStore.cargos);
    } catch (error) {
      console.error('Fetch cargos data error', error);
    }
  };

  const handleCreateJourney = (cargoId) => {
    // Logic to create a journey
  };

  const handleCreateSchedule = (cargoId) => {
    // Logic to create a schedule
  };

  const handleCreateManifest = (cargoId) => {
    // Logic to create a manifest
  };

  const openTripDialog = () => {
    setIsTripDialogOpen(true);
  };

  const closeTripDialog = () => {
    setIsTripDialogOpen(false);
  };

  const openSheduleDialog = () => {
    setIsSheduleDialogOpen(true);
  };

  const closeSheduleDialog = () => {
    setIsSheduleDialogOpen(false);
  };

  const openNoticeDialog = () => {
    setIsNoticeDialogOpen(true);
  };

  const closeNoticeDialog = () => {
    setIsNoticeDialogOpen(false);
  };

  const handleSearchContainer = async () => {
    try {
      await cargoStore.findContainer(coordinates);
      setNearestContainer(cargoStore.coordinates);
    } catch (error) {
      console.error('Find container error', error);
    }
  };

  const handleStatistic = async () => {
    try {
      navigate("/cabinet/statistic");
    } catch (error) {
      console.error('Navigate to statistics error', error);
    }
  };

  const handleChangeStatus = async (cargoId) => {
    try {
      await cargoStore.updateNotice(cargoId, notice);
      fetchCargosByReceiver();
    } catch (error) {
      console.error('Update notice error', error);
    }
  };

  return (
    <main>
      <NavBar/>
      <div className={styles.container}>
      <div className={styles.cargolistcontainer}>
        <div className={styles.searchcontainer}>
          <input 
            type="text" 
            placeholder={t('Logist.latitude')} 
            value={coordinates.lat} 
            onChange={(e) => setCoordinates({ ...coordinates, lat: e.target.value })} 
          />
          <input 
            type="text" 
            placeholder={t('Logist.longitude')} 
            value={coordinates.lng} 
            onChange={(e) => setCoordinates({ ...coordinates, lng: e.target.value })} 
          />
          <button onClick={handleSearchContainer}>{t('Logist.searchContainer')}</button>
        </div>

        <button className="sta" onClick={handleStatistic}>{t('Logist.static')}</button>

        {nearestContainer && (
          <div className={styles.nearestcontainer}>
            <h2>{t('Logist.nearestContainer')}</h2>
            <p>{t('Logist.containerId')}: {nearestContainer.containerId}</p>
            <p>{t('Logist.containerCoordinates')}: {nearestContainer.coordinates}</p>
            <p>{t('Logist.containerDistance')}: {nearestContainer.distance}</p>
          </div>
        )}
        <h1>{t('Logist.cargoList')}</h1>
        <table className={styles.cargotable}>
          <thead>
            <tr>
              <th>{t('Logist.cargoId')}</th>
              <th>{t('Logist.description')}</th>
              <th>{t('Logist.actions')}</th>
            </tr>
          </thead>
          <tbody>
            {cargos.map(cargo => (
              <tr key={cargo.id}>
                <td>{cargo.id}</td>
                <td>{cargo.name}</td>
                <td>
                  <button onClick={openTripDialog}>{t('Logist.createJourney')}</button>
                  <button onClick={openSheduleDialog}>{t('Logist.createSchedule')}</button>
                  <button onClick={openNoticeDialog}>{t('Logist.createManifest')}</button>
                  
                  <Modal
                    isOpen={isTripDialogOpen}
                    onRequestClose={closeTripDialog}
                  >
                    <TripDialog setReceiverInfo={setReceiverInfo} closeDialog={closeTripDialog} />
                  </Modal>

                  <Modal
                    isOpen={isSheduleDialogOpen}
                    onRequestClose={closeSheduleDialog}
                  >
                    <SheduleDialog setShedulerInfo={setSheduleInfo} closeDialog={closeSheduleDialog} />
                  </Modal>

                  <Modal
                    isOpen={isNoticeDialogOpen}
                    onRequestClose={closeNoticeDialog}
                  >
                    <NoticeDialog setNoticeInfo={setNoticeInfo} closeDialog={closeNoticeDialog} />
                  </Modal>

                  <input 
                    type="text" 
                    placeholder={t('Logist.notice')} 
                    onChange={(e) => setNotice(e.target.value)} 
                  />
                  <button className={styles.create} onClick={() => handleChangeStatus(cargo.id)}>{t('Logist.buton')}</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      </div>
      <Footer/>
    </main>
  );
};

export default Logist;
