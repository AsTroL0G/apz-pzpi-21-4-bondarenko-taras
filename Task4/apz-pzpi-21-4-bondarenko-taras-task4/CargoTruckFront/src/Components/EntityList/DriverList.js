import React, { useState, useEffect } from 'react';
import { driverStore } from './../../Stores/DriverStore'; 
import styles from './CargoList.module.css';

const DriverList = () => {
    const [driverDetails, setDriverDetails] = useState([]);
    const [selectedDriver, setSelectedDriver] = useState(null);

    const fetchDrivers = async () => {
        try {
            await driverStore.getDriverPage();
            setDriverDetails(driverStore.drivers);
        } catch (error) {
            console.error('Fetch driver details error', error);
        }
    };

    useEffect(() => {
        fetchDrivers();
    }, []);

    const handleEditDriver = async(driver) => {
        if (driver._id) {
            await driverStore.updateDriver(driver);
        } else {
            await driverStore.createDriver(driver);
        }
        fetchDrivers();
        setSelectedDriver(null);
    };

    const handleSaveDriver = (driver) => {
        setSelectedDriver(driver);
    };

    const handleDeleteDriver = async (driverId) => {
        try {
            await driverStore.deleteDriver(driverId);
            fetchDrivers();
        } catch (error) {
            console.error('Delete driver error', error);
        }
    };

    const handleCreateDriver = () => {
        setSelectedDriver({
            _id: '',
            FirstName: '',
            LastName: '',
            Patronym: '',
            PhoneNumber: '',
            Email: '',
            PasswordHash: '',
            DriverLicenseNumber: ''
        });
    };

    return (
        <div className={styles.container}>
            {selectedDriver && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedDriver.FirstName}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, FirstName: e.target.value })}
                        placeholder="Ім'я"
                    />
                    <input
                        type="text"
                        value={selectedDriver.LastName}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, LastName: e.target.value })}
                        placeholder="Прізвище"
                    />
                    <input
                        type="text"
                        value={selectedDriver.Patronym}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, Patronym: e.target.value })}
                        placeholder="По батькові"
                    />
                    <input
                        type="text"
                        value={selectedDriver.PhoneNumber}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, PhoneNumber: e.target.value })}
                        placeholder="Номер телефону"
                    />
                    <input
                        type="text"
                        value={selectedDriver.Email}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, Email: e.target.value })}
                        placeholder="Електронна пошта"
                    />
                    <input
                        type="password"
                        value={selectedDriver.PasswordHash}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, PasswordHash: e.target.value })}
                        placeholder="Пароль"
                    />
                    <input
                        type="text"
                        value={selectedDriver.DriverLicenseNumber}
                        onChange={(e) => setSelectedDriver({ ...selectedDriver, DriverLicenseNumber: e.target.value })}
                        placeholder="Номер водійського посвідчення"
                    />
                    <button onClick={() => handleEditDriver(selectedDriver)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
                <button onClick={handleCreateDriver}>Створити водія</button>
                {driverDetails.map(driver => (
                    <li key={driver._id} className={styles.item}>
                        <p>Ім'я: {driver.FirstName}</p>
                        <p>Прізвище: {driver.LastName}</p>
                        <p>По батькові: {driver.Patronym}</p>
                        <p>Номер телефону: {driver.PhoneNumber}</p>
                        <p>Електронна пошта: {driver.Email}</p>
                        <p>Номер водійського посвідчення: {driver.DriverLicenseNumber}</p>
                        <div>
                            <button onClick={() => handleSaveDriver(driver)}>Переглянути</button>
                            <button className={styles.del} onClick={() => handleDeleteDriver(driver._id)}>Видалити</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default DriverList;
