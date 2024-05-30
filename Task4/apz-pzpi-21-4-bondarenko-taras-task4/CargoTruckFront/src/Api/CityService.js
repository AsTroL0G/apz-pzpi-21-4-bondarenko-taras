import { ApiClient } from './ApiClient';

export const CityService = {
    getCityByName: async (cityName) => {
    const response = await ApiClient.get(`city/get/name/${cityName}`);
    return response.data;
  }
};
