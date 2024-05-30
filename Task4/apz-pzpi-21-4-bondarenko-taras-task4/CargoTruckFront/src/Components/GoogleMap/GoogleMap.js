
import React from 'react';
import { MarkerF } from "@react-google-maps/api";
import { GoogleMap, LoadScript, Marker } from '@react-google-maps/api';

const containerStyle = {
  width: '100%',
  height: '400px',
};

const center = {
  lat: -3.745,
  lng: -38.523,
};

const GoogleMapComponent = ({ lat, lng }) => {
  return (
    <LoadScript googleMapsApiKey="AIzaSyCj4z_VeAQ5N8PcBmdqWRnR7dA-2M27bbg">
      <GoogleMap mapContainerStyle={containerStyle} center={{ lat, lng }} zoom={10}>
        <MarkerF position={{ lat, lng }} />
      </GoogleMap>
    </LoadScript>
  );
};

export default GoogleMapComponent;


