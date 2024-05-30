import { ApiClient } from './ApiClient';

export const CargoService = {
  getCargoBySender: async (id) => {
    const response = await ApiClient.get(`cargo/get/sender${id}`);
    return response.data;
  },
  getCargoByReceiver: async (id) => {
    const response = await ApiClient.get(`cargo/get/receiver${id}`);
    return response.data;
  },
  getCargo: async (id) => {
    const response = await ApiClient.get(`cargo/get/${id}`);
    return response.data;
  },
  updateCargo: async (cargo) => {
    const response = await ApiClient.put('cargo/update', cargo);
    return response.data;
},
  getCargoPage: async () => {
    const response = await ApiClient.get(`cargo/get?pageNumber=1&pageSize=100`);
    return response.data.items;
  },
  createCargo: async (cargo) => {
    const response = await ApiClient.post('cargo/create', cargo);
    return response;
  },
  updateNotice: async (cargoId,newNoticeId) => {
    const response = await ApiClient.put(`cargo/update/notice?cargoId=${cargoId}&newNoticeId=${newNoticeId}`);
    return response;
  },
  getCargoStatus: async () => {
    const response = await ApiClient.get('cargo?pageNumber=1&pageSize=100');
    return response.data.items;
  },
  findContainer: async (lat,lng) => {
    const response = await ApiClient.get(`container/find/${lat}/${lng}`);
    return response.data;
  },
  deleteCargo: async (id) => {
    const response = await ApiClient.delete(`cargo/delete/${id}`);
    return response;
  }

};
