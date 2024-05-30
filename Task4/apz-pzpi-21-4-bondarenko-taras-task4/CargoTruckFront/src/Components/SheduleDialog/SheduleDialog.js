import React, { useState } from 'react';
import { userStore } from '../../Stores/UserStore';
import styles from './Stules.module.css';

const ReceiverInfoDialog = ({ setReceiverInfo, closeDialog }) => {
  const [phoneNumber, setPhoneNumber] = useState('');
  const [lastName, setLastName] = useState('');
  const [firstName, setFirstName] = useState('');

  const handleFetchUser = async () => {

    try {
        await userStore.fetchUserByNumberPhone(phoneNumber);
        if(userStore.user.lastName){
          setLastName(userStore.user.lastName);
          setFirstName(userStore.user.firstName);
          console.log(userStore.user.phoneNumber)
          setReceiverInfo(userStore.user.id);
        }else{
          setLastName("");
          setFirstName("");
        }
    } catch (error) {
        console.error('Login error', error);
    }
  };

  const handleRegisterUser = async () => {
    console.log(userStore.user.phoneNumber)
    if(!userStore.user.phoneNumber){
    try{
      await userStore.registerUser(firstName, lastName, "", phoneNumber, 'guest@gmail.com', '1111');
      handleFetchUser();
      closeDialog();
    } 
    catch (error) {
      console.error('Error registering user:', error);
    }
  }else{
    closeDialog();
  }
  };


  return (
    <div className={styles.senderinfodialog}>
    <h2>Введіть інформацію про розклад</h2>
    <input
      type="text"
      placeholder="Введіть номер подорожі"
      value={phoneNumber}
      onChange={(e) => setPhoneNumber(e.target.value)}
      onBlur={handleFetchUser}
    />
    <input
      type="text"
      placeholder="Введіть місто"
      value={lastName}
      onChange={(e) => setLastName(e.target.value)}
    />
    <input
      type="text"
      placeholder="Введіть дату прибуття"
      value={firstName}
      onChange={(e) => setFirstName(e.target.value)}
    />
    <button onClick={handleRegisterUser}>OK</button>
  </div>
  );
};

export default ReceiverInfoDialog;