
import { ApiClient } from './ApiClient';

export const StatisticsService = {

    getContainerStatistics: async (id) => {
    const response = await ApiClient.get(`statistics/delivery/container/${id}`);
    return response.data;
  },
  getTripStatistics: async (id) => {
    const response = await ApiClient.get(`statistics/trip/${id}`);
    return response.data;
  }
};
