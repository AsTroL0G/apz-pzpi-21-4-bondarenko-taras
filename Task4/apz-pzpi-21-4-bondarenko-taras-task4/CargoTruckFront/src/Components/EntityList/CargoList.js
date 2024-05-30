
import React, { useState, useEffect } from 'react';
import { cargoStore } from './../../Stores/CargoStore';
import styles from './CargoList.module.css';

const CargoList = () => {
    const [cargoDetails, setCargoDetails] = useState([]);
    const [selectedCargo, setSelectedCargo] = useState(null);

    const fetchCargo = async () => {
        try {
            await cargoStore.fetchCargoPage();
            setCargoDetails(cargoStore.cargos);
        } catch (error) {
            console.error('Fetch cargo details error', error);
        }
    };

    useEffect(() => {
        fetchCargo();
    }, []);

    const handleEditCargo = async(cargo) => {
        if(cargo.id){
            await cargoStore.updateCargo(cargo);
            fetchCargo();
        }else{
            await cargoStore.createCargo(cargo);
            fetchCargo();
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
            await cargoStore.deleteCargo(cargoId);
            fetchCargo();
        } catch (error) {
            console.error('Delete cargo error', error);
        }
    };

    const handleCreateCargo = () => {
        setSelectedCargo({
            id: '',
            name: '',
            weight: '',
            length: '',
            height: '',
            width: '',
            announcedPrice: '',
            shippingPrice: '',
            status: ''
        });
    };

    return (
        <div className={styles.container}>
            {selectedCargo && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedCargo.name}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, name: e.target.value })}
                        placeholder="Назва"
                    />
                    <input
                        type="text"
                        value={selectedCargo.weight}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, weight: e.target.value })}
                        placeholder="Вага"
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
                        value={selectedCargo.announcedPrice}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, announcedPrice: e.target.value })}
                        placeholder="Оголошена вартість"
                    />
                    <input
                        type="text"
                        value={selectedCargo.shippingPrice}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, shippingPrice: e.target.value })}
                        placeholder="Вартість доставки"
                    />
                    
                    <input
                        type="text"
                        value={selectedCargo.noticeId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, noticeId: e.target.value })}
                        placeholder="Номер відомості"
                    />

                    <input
                        type="text"
                        value={selectedCargo.senderId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, senderId: e.target.value })}
                        placeholder="Номер відправника"
                    />
                    <input
                        type="text"
                        value={selectedCargo.receiverId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, receiverId: e.target.value })}
                        placeholder="Номер отримувача"
                    />
                    <input
                        type="text"
                        value={selectedCargo.citySenderId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, citySenderId: e.target.value })}
                        placeholder="Місто відправки"
                    />
                    <input
                        type="text"
                        value={selectedCargo.addressSenderId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, addressSenderId: e.target.value })}
                        placeholder="Адрес відпправки"
                    />
                    <input
                        type="text"
                        value={selectedCargo.cityReceiverId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, cityReceiverId: e.target.value })}
                        placeholder="Місто отримання"
                    />
                    <input
                        type="text"
                        value={selectedCargo.addressReceiverId}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, addressReceiverId: e.target.value })}
                        placeholder="Адрес отримання"
                    />
                    <input
                        type="text"
                        value={selectedCargo.status}
                        onChange={(e) => setSelectedCargo({ ...selectedCargo, status: e.target.value })}
                        placeholder="Статус"
                    />

                    <button  onClick={() => handleEditCargo(selectedCargo)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
            <button onClick={handleCreateCargo}>Створити вантаж</button>
                {cargoDetails.map(cargo => (
                    <li key={cargo.id} className={styles.item}>
                        <p>Номер: {cargo.id}</p>
                        <p>Назва: {cargo.name}</p>
                        <p>Вага: {cargo.weight}</p>
                        <p>Габарити: {cargo.length}*{cargo.height}*{cargo.width}</p>
                        <p>Статус: {cargo.status}</p>
                        {/* Додайте решту полів для виводу */}
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
