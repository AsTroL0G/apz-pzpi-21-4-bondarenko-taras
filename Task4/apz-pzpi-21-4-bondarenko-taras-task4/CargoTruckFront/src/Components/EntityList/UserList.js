
import React, { useState, useEffect } from 'react';
import { userStore } from './../../Stores/UserStore';
import styles from './CargoList.module.css';


const UserList = () => {
    const [userDetails, setUserDetails] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);

    const fetchUsers = async () => {
        try {
            await userStore.fetchUserPage();
            setUserDetails(userStore.users);
        } catch (error) {
            console.error('Fetch user details error', error);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const handleEditUser = async(user) => {
        if(user.id){
            await userStore.updateUser(user);
            fetchUsers();
        } else {
            await userStore.createUser(user);
            fetchUsers();
        }
    };

    const handleSaveUser = async (user) => {
        try {
            setSelectedUser(user);
            await fetchUsers();
        } catch (error) {
            console.error('Save user error', error);
        }
    };

    const handleBanUser = async (userId) => {
        try {
            await userStore.banUser(userId);
            fetchUsers();
        } catch (error) {
            console.error('Delete user error', error);
        }
    };
    const handleUnBanUser = async (userId) => {
        try {
            await userStore.unBanUser(userId);
            fetchUsers();
        } catch (error) {
            console.error('Delete user error', error);
        }
    };

    const handleCreateUser = () => {
        setSelectedUser({
            id: '',
            firstName: '',
            lastName: '',
            patronym: '',
            phoneNumber: '',
            guestId: null,
            roles: [
                {
                    id: '',
                    name: ''
                }
            ],
            email: '',
            password: null
        });
    };

    return (
        <div className={styles.container}>
            {selectedUser && (
                <div className={styles.form}>
                    <input
                        type="text"
                        value={selectedUser.firstName}
                        onChange={(e) => setSelectedUser({ ...selectedUser, firstName: e.target.value })}
                        placeholder="Ім'я"
                    />
                    <input
                        type="text"
                        value={selectedUser.lastName}
                        onChange={(e) => setSelectedUser({ ...selectedUser, lastName: e.target.value })}
                        placeholder="Прізвище"
                    />
                    <input
                        type="text"
                        value={selectedUser.patronym}
                        onChange={(e) => setSelectedUser({ ...selectedUser, patronym: e.target.value })}
                        placeholder="По-батькові"
                    />
                    <input
                        type="text"
                        value={selectedUser.phoneNumber}
                        onChange={(e) => setSelectedUser({ ...selectedUser, phoneNumber: e.target.value })}
                        placeholder="Телефон"
                    />
                    <input
                        type="email"
                        value={selectedUser.email}
                        onChange={(e) => setSelectedUser({ ...selectedUser, email: e.target.value })}
                        placeholder="Електронна пошта"
                    />
                    <input
                        type="text"
                        value={selectedUser.roles[0].name}
                        onChange={(e) => {
                            const roles = [...selectedUser.roles];
                            roles[0].name = e.target.value;
                            setSelectedUser({ ...selectedUser, roles });
                        }}
                        placeholder="Роль"
                    />

                    <button onClick={() => handleEditUser(selectedUser)}>Зберегти</button>
                </div>
            )}
            <ul className={styles.list}>
                
                {userDetails.map(user => (
                    <li key={user.id} className={styles.item}>
                        <p>Ім'я: {user.firstName}</p>
                        <p>Прізвище: {user.lastName}</p>
                        <p>По-батькові: {user.patronym}</p>
                        <p>Телефон: {user.phoneNumber}</p>
                        <p>Електронна пошта: {user.email}</p>
                        <div>
                            <button onClick={() => handleSaveUser(user)}>Переглянути</button>
                            <button className={styles.del} onClick={() => handleBanUser(user.id)}>Заблокувати</button>
                            <button  onClick={() => handleUnBanUser(user.id)}>Розблокувати</button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserList;
