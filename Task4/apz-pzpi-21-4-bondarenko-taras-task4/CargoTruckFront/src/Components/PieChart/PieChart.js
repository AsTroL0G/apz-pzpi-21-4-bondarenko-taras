import React from 'react';
import { Pie } from 'react-chartjs-2';
import { useTranslation } from 'react-i18next';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';

ChartJS.register(ArcElement, Tooltip, Legend);

const PieChart = ({ averageLoadCapacity, volumetricWeight }) => {
  const { t } = useTranslation();
  const data = {
    labels: [t('Statistic.Average Load Capacity'), t('Statistic.Volumetric Weight')],
    datasets: [
      {
        data: [averageLoadCapacity, volumetricWeight],
        backgroundColor: ['#FF6384', '#36A2EB'],
        hoverBackgroundColor: ['#FF6384', '#36A2EB']
      }
    ]
  };

  return (
    <div>
      <Pie data={data} />
    </div>
  );
};

export default PieChart;
