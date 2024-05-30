import axios from 'axios';

const BASE_URL = 'http://192.168.0.105:7064/';

export const ApiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});
