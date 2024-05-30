import React, { useState, useEffect } from 'react';
import { sensorStore } from './../../Stores/SensorStore';
import styles from './CargoList.module.css';

const SensorList = () => {
    const [sensorDetails, setSensorDetails] = useState([]);
    const [selectedSensor, setSelectedSensor] = useState(null);

    const fetchSensors = async () => {
        try {
            await sensorStore.getSensorPage();
            setSensorDetails(sensorStore.sensors);
        } catch (error) {
            console.error('Fetch sensor details error', error);
        }
    };

    useEffect(() => {
        fetchSensors();
    }, []);

    const handleEditSensor = async(sensor) => {
        if (sensor._id) {
            await sensorStore.updateSensor(sensor);
        } else {
            await sensorStore.createSensor(sensor);
        }
        fetchSensors();
        setSelectedSensor(null);
    };

    const handleSaveSensor = (sensor) => {
        setSelectedSensor(sensor);
    };

    const handleDeleteSensor = async (sensorId) => {
        try {
            await sensorStore.deleteSensor(sensorId);
            fetchSensors();
        } catch (error) {
            console.error('Delete sensor error', error);
        }
    };

    const handleCreateSensor = () => {
        setSelectedSensor({
            _id: '',
            Type: '',
            Name: ''
        });
    };

    return (
        <div className={styles.container}>
            {selectedSensor && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedSensor.Type}
                        onChange={(e) => setSelectedSensor({ ...selectedSensor, Type: e.target.value })}
                        placeholder="Тип"
                    />
                    <input
                        type="text"
                        value={selectedSensor.Name}
                        onChange={(e) => setSelectedSensor({ ...selectedSensor, Name: e.target.value })}
                        placeholder="Назва"
                    />
                    <button onClick={() => handleEditSensor(selectedSensor)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
                <button onClick={handleCreateSensor}>Створити сенсор</button>
                {sensorDetails.map(sensor => (
                    <li key={sensor._id} className={styles.item}>
                        <p>Тип: {sensor.Type}</p>
                        <p>Назва: {sensor.Name}</p>
                        <div>
                            <button onClick={() => handleSaveSensor(sensor)}>Переглянути</button>
                            <button className={styles.del} onClick={() => handleDeleteSensor(sensor._id)}>Видалити</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SensorList;
