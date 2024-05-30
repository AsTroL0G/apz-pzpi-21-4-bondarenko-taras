import { makeAutoObservable } from 'mobx';
import { ExportService } from '../Api/ExportService';
import { User, UserCreate, UserUpdate, Credentials } from './../Models/User';
import jwt_decode from 'jwt-decode';

class ExportStore {

  constructor() {
    makeAutoObservable(this);
  }

  async getExportEntity(entityName) {
    await ExportService.getExportEntity(entityName);
  }

}

export const exportStore = new ExportStore();