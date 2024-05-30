import { ApiClient } from './ApiClient';

export const ContainerService = {

  getContainerPage: async () => {
    const response = await ApiClient.get('container?pageNumber=1&pageSize=100');
    return response.data.items;
  },
  createContainer: async (container) => {
    const response = await ApiClient.post('container/create', container);
    return response;
  },
  deleteContainer: async (id) => {
    const response = await ApiClient.delete(`container/delete/${id}`);
    return response;
  },
};
