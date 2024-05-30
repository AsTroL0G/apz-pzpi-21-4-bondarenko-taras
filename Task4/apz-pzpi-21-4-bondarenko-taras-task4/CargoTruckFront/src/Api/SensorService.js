import { ApiClient } from './ApiClient';

export const SensorService = {

    getSensorByContainer: async (id) => {
    const response = await ApiClient.get(`sensors/get/GPS/container/${id}`);
    return response.data;
  }
};
