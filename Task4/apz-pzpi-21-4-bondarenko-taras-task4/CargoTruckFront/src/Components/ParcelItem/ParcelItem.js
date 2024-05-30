import styles from './Stules.module.css';
import React, {useEffect, useState} from "react";

const ParcelItem = ({
    id,
    status,
    weight,
    name,
    citySenderId,
    addressSenderId,
    receiver,
    receiverPhone,
    cityReceiverId,
    addressReceiverId,
    onClick 
  }) => {

    return (
        <tr  onClick={() => onClick(id)} className={styles.parcelitem}>
          <td>{id}</td>
          <td className={styles.status}>{status}</td>
          <td>{weight}</td>
          <td>{name}</td>
          <td>{citySenderId}</td>
          <td>{addressSenderId}</td>
          <td>{cityReceiverId}</td>
          <td>{addressReceiverId}</td>
        </tr>
      );
    };


export default ParcelItem;