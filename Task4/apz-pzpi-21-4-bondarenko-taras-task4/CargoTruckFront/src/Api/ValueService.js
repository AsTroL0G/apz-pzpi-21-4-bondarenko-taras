import { ApiClient } from './ApiClient';

export const ValueService = {

    getValueBySensorId: async (id) => {
    const response = await ApiClient.get(`value/getBySensorId/${id}`);
    return response.data;
  }
};
