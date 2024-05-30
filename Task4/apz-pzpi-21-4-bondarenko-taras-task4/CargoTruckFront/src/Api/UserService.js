import { ApiClient } from './ApiClient';

export const UserService = {
  registerUser: async (user) => {
    const response = await ApiClient.post('users/register', user);
    return response;
  },
  loginUser: async (credentials) => {
    const response = await ApiClient.post('users/login', credentials);
    return response;
  },
  getUser: async (id) => {
    const response = await ApiClient.get(`users/${id}`);
    return response.data;
  },
  getUserByNumberPhone: async (numberPhone) => {
    const response = await ApiClient.get(`users/Number/${numberPhone}`);
    return response.data;
  },
  updateUser: async (user) => {
    const response = await ApiClient.put('users', user);
    return response;
  },
  getPageUser: async () => {
    const response = await ApiClient.get('users?pageNumber=1&pageSize=100');
    return response.data.items;
  },
  deleteUser: async (id) => {
    const response = await ApiClient.delete(`users/delete/${id}`);
    return response;
  },
  banUser: async (id) => {
    const response = await ApiClient.put(`users/ban/${id}`);
    return response;
  },
  unBanUser: async (id) => {
    const response = await ApiClient.put(`users/unban/${id}`);
    return response;
  },
};
