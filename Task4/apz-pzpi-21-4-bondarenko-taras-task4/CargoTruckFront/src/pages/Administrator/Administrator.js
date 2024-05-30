import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { exportStore } from './../../Stores/ExportStore';
import NavBar from '../../Components/navbar/NavBar';
import CargoList from '../../Components/EntityList/CargoList';
import UserList from '../../Components/EntityList/UserList';
import DriverList from '../../Components/EntityList/DriverList';
import CarList from '../../Components/EntityList/CarList';
import SensorList from '../../Components/EntityList/SensorList';
import ContainerList from '../../Components/EntityList/ContainerList';
import Footer from '../../Components/footer/Footer';
import Modal from 'react-modal';
import styles from './Stules.module.css';


Modal.setAppElement('#root');

const AdminPage = () => {
    const { t } = useTranslation();
    const [selectedEntity, setSelectedEntity] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [entityDetails, setEntityDetails] = useState({});
    const [cargoDetails, setCargoDetails] = useState({});
    const entities = [
      { id: 1, name: 'User' },
      { id: 2, name: 'Cargo' },
      { id: 3, name: 'Driver' },
      { id: 4, name: 'Car' },
      { id: 5, name: 'Sensor' },
      { id: 6, name: 'Container' },
    ];
  
  
    const renderComponent = (entityName) => {
        console.log(entityName)
        switch (entityName) {
            case 'Cargo':
                return <CargoList/>;
            case 'User':
                return <UserList/>;
            case 'Driver':
                return <DriverList/>;
            case 'Car':
                return <CarList/>;
            case 'Sensor':
                return <SensorList/>;
            case 'Container':
                return <ContainerList/>;
            default:
                return null;
        }
    };

    const handleEntityClick = (entity) => {
      setSelectedEntity(entity);
    };
  
    const handleModalClose = () => {
      setIsModalOpen(false);
    };
  
    const handleExport = async (entiteName) => {
      try {
        await exportStore.getExportEntity(entiteName);
      } catch (error) {
          console.error('Download error', error);
      }
  };
    
    return (

      <main>
                    <NavBar/>
      <div className="container">
      <div className={styles.first}>
        <h1>{t('AdminPanel.title')}</h1>
        <ul>
          {entities.map((entity) => (
            <li key={entity.id} onClick={() => handleEntityClick(entity)}>
              {entity.name}
            </li>
          ))}
        </ul>
  
        {selectedEntity && (
          <div className={styles.two}>
            <h2>{selectedEntity.name}</h2>
            <button  className={styles.twoo} onClick={() => handleExport(selectedEntity.name)}>Створити резервну копію</button>
            {renderComponent(selectedEntity.name)}
          </div>
        )}
    
      </div>
      </div>
      <Footer/>
      </main>
    );
  };
  
  export default AdminPage;