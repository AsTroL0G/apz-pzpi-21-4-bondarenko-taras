import { ApiClient } from './ApiClient';

export const NoticeService = {

    getNotice: async (id) => {
    const response = await ApiClient.get(`notice/get/${id}`);
    return response.data;
  }
};
