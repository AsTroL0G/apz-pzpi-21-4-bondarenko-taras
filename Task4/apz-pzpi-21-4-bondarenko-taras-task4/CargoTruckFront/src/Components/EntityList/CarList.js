import React, { useState, useEffect } from 'react';
import { carStore } from './../../Stores/ContainerStore';
import styles from './CargoList.module.css';

const CarList = () => {
    const [carDetails, setCarDetails] = useState([]);
    const [selectedCar, setSelectedCar] = useState(null);

    const fetchCargo = async () => {
        try {
            await carStore.getCarPage();
            setCarDetails(carStore.containers);
        } catch (error) {
            console.error('Fetch cargo details error', error);
        }
    };

    useEffect(() => {
        fetchCar();
    }, []);

    const handleEditCargo = async(car) => {
        if (car.id) {
            await carStore.updateCar(car);
        } else {
            await carStore.createContainer(car);
        }
        fetchCar();
        setSelectedCar(null);
    };

    const handleSaveCar = (cargo) => {
        setSelectedCar(cargo);
    };

    const handleDeleteCar = async (carId) => {
        try {
            await carStore.deleteCar(carId);
            fetchCar();
        } catch (error) {
            console.error('Delete cargo error', error);
        }
    };

    const handleCreateCar = () => {
        setSelectedCar({
            id: '',
            Number_MIA: '',
            Brand: '',
            LoadCapacity: ''
        });
    };

    return (
        <div className={styles.container}>
            {selectedCargo && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedCar.Number_MIA}
                        onChange={(e) => setSelectedCar({ ...selectedCar, Number_MIA: e.target.value })}
                        placeholder="Номер"
                    />
                    <input
                        type="text"
                        value={selectedCar.Brand}
                        onChange={(e) => setSelectedCar({ ...selectedCar, Brand: e.target.value })}
                        placeholder="Бренд"
                    />
                    <input
                        type="text"
                        value={selectedCar.LoadCapacity}
                        onChange={(e) => setSelectedCar({ ...selectedCar, LoadCapacity: e.target.value })}
                        placeholder="Вантажопідйомність"
                    />
                    <button onClick={() => handleEditCar(selectedCar)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
                <button onClick={handleCreateCar}>Створити вантаж</button>
                {cargoDetails.map(car => (
                    <li key={car.id} className={styles.item}>
                        <p>Номер: {car.Number_MIA}</p>
                        <p>Бренд: {car.Brand}</p>
                        <p>Вантажопідйомність: {car.LoadCapacity} кг</p>
                        <div>
                            <button onClick={() => handleSaveCar(car)}>Переглянути</button>
                            <button className={styles.del} onClick={() => handleDeleteCar(car.id)}>Видалити</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CarList;
