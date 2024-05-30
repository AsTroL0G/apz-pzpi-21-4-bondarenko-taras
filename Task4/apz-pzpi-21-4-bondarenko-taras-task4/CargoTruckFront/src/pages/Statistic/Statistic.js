import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Statistics.css';
import { useTranslation } from 'react-i18next';
import NavBar from "../../Components/navbar/NavBar";
import Footer from "../../Components/footer/Footer"
import { statisticStore } from '../../Stores/StatisticStore';
import PieChart from '../../Components/PieChart/PieChart';
import BarChart from '../../Components/BarChart/BarChart';

const Statistics = () => {
const { t } = useTranslation();
const [containerId, setContainerId] = useState('');
const [tripId, setTripId] = useState('');
const [containerStats, setContainerStats] = useState(null);
const [tripStats, setTripStats] = useState(null);

// Fetch container statistics by ID
const fetchContainerStats = async (id) => {
try {
await statisticStore.getContainerStatistics(id);
setContainerStats(statisticStore.static);
} catch (error) {
console.error('Error fetching container statistics', error);
}
};

// Fetch trip statistics by ID
const fetchTripStats = async (id) => {
try {
await statisticStore.getTripStatistics(id);
setTripStats(statisticStore.static);
} catch (error) {
console.error('Error fetching trip statistics', error);
}
};

// Effect to fetch container statistics when containerId changes
useEffect(() => {
if (containerId) {
fetchContainerStats(containerId);
}
}, [containerId]);

// Effect to fetch trip statistics when tripId changes
useEffect(() => {
if (tripId) {
fetchTripStats(tripId);
}
}, [tripId]);

return (
<main>
<NavBar/>
<div className="statistics-container">
<h1>{t('Statistic.Statistics')}</h1>

<div className="statistics-content">
<div className="statistics-column">
<div className="input-section">
  <label htmlFor="containerId">{t('Statistic.Container ID')}:</label>
  <input
    type="text"
    id="containerId"
    value={containerId}
    onChange={(e) => setContainerId(e.target.value)}
  />
</div>

{containerStats && (
  <div className="stats-section">
    <h2>{t('Statistic.Container Statistics')}</h2>
    <p>{t('Statistic.Container ID')}: {containerStats.containerId}</p>
    <p>{t('Statistic.Container Name')}: {containerStats.containerName}</p>
    <p>{t('Statistic.Container Type')}: {containerStats.containerType}</p>
    <p>{t('Statistic.Number of Trips')}: {containerStats.numberTrips}</p>
    <p>{t('Statistic.Average Load Capacity')}: {containerStats.averageLoadCapacity}</p>
    <p>{t('Statistic.Volumetric Weight')}: {containerStats.volumetricWeight}</p>
    <div className="stati">
      <PieChart 
        averageLoadCapacity={containerStats.averageLoadCapacity}
        volumetricWeight={containerStats.volumetricWeight}
      />
    </div>
  </div>
)}
</div>

<div className="statistics-column">
<div className="input-section">
  <label htmlFor="tripId">{t('Statistic.Trip ID')}:</label>
  <input
    type="text"
    id="tripId"
    value={tripId}
    onChange={(e) => setTripId(e.target.value)}
  />
</div>

{tripStats && (
  <div className="stats-section">
    <h2>{t('Statistic.Trip Statistics')}</h2>
    <p>{t('Statistic.Trip ID')}: {tripStats.tripId}</p>
    <p>{t('Statistic.Container Name')}: {tripStats.containerName}</p>
    <p>{t('Statistic.Container Type')}: {tripStats.containerType}</p>
    <p>{t('Statistic.Total Weight')}: {tripStats.totalWeight}</p>
    <p>{t('Statistic.Average Speed')}: {tripStats.averageSpeed}</p>
    <p>{t('Statistic.Time Spent')}: {tripStats.timeSpent}</p>
    <div className="stati">
      <BarChart 
        totalWeight={tripStats.totalWeight}
      />
    </div>
  </div>
)}
</div>
</div>
</div>
<Footer/>
</main>
);
};

export default Statistics;
