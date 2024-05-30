
import React, { useState, useEffect } from 'react';
import { containerStore } from './../../Stores/ContainerStore';
import styles from './CargoList.module.css';

const CargoList = () => {
    const [cargoDetails, setCargoDetails] = useState([]);
    const [selectedCargo, setSelectedCargo] = useState(null);

    const fetchCargo = async () => {
        try {
            await containerStore.fetchContainerPage();
            setCargoDetails(containerStore.containers);
        } catch (error) {
            console.error('Fetch cargo details error', error);
        }
    };

    useEffect(() => {
        fetchCargo();
    }, []);

    const handleEditCargo = async(container) => {
        if(container.id){
            await containerStore.updateContainer(container);
            fetchCargo();
            setSelectedCargo();
        } else {
            await containerStore.createContainer(container);
            fetchCargo();
            setSelectedCargo();
        }
    };

    const handleSaveCargo = async (cargo) => {
        try {
            setSelectedCargo(cargo);
            await fetchCargo();
        } catch (error) {
            console.error('Save cargo error', error);
        }
    };

    const handleDeleteCargo = async (cargoId) => {
        try {
            await containerStore.deleteContainer(cargoId);
            fetchCargo();
        } catch (error) {
            console.error('Delete cargo error', error);
        }
    };

    const handleCreateCargo = () => {
        setSelectedCargo({
            id: '',
            type: '',
            name: '',
            loadCapacity: '',
            length: '',
            height: '',
            width: '',
            status: ''
        });
    };

    return (

        <div className={styles.container}>
            {selectedCargo && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedCargo.type}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, type: e.target.value })}
                        placeholder="Тип"
                    />
                    <input
                        type="text"
                        value={selectedCargo.name}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, name: e.target.value })}
                        placeholder="Назва"
                    />
                    <input
                        type="text"
                        value={selectedCargo.loadCapacity}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, loadCapacity: e.target.value })}
                        placeholder="Вантажопідйомність"
                    />
                    <input
                        type="text"
                        value={selectedCargo.length}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, length: e.target.value })}
                        placeholder="Довжина"
                    />
                    <input
                        type="text"
                        value={selectedCargo.width}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, width: e.target.value })}
                        placeholder="Ширина"
                    />
                    <input
                        type="text"
                        value={selectedCargo.height}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, height: e.target.value })}
                        placeholder="Висота"
                    />
                    <input
                        type="text"
                        value={selectedCargo.status}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, status: e.target.value })}
                        placeholder="Статус"
                    />

                    <button onClick={() => handleEditCargo(selectedCargo)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
                <button onClick={handleCreateCargo}>Створити вантаж</button>
                {cargoDetails.map(cargo => (
                    <li key={cargo.id} className={styles.item}>
                        <p>Номер: {cargo.id}</p>
                        <p>Тип: {cargo.type}</p>
                        <p>Назва: {cargo.name}</p>
                        <p>Вантажопідйомність: {cargo.loadCapacity} кг</p>
                        <p>Габарити: {cargo.length} см * {cargo.width} см * {cargo.height} см</p>
                        <p>Статус: {cargo.status}</p>
                        <div>
                            <button onClick={() => handleSaveCargo(cargo)}>Переглянути</button>
                            <button className={styles.del} onClick={() => handleDeleteCargo(cargo.id)}>Видалити</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>

    );
};

export default CargoList;
