import { makeAutoObservable } from 'mobx';
import { ContainerService } from '../Api/ContainerService';
import { User, UserCreate, UserUpdate, Credentials } from './../Models/User';
import jwt_decode from 'jwt-decode';

class ContainerStore {
  user = null;
  containers =[];
  isAuthenticated = false;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchContainerPage() {
    this.containers = await ContainerService.fetchContainerPage();
  }
  async createContainer(cargoData) {
    await ContainerService.createContainer(cargoData);
  }
  async deleteContainer(id) {
    await ContainerService.deleteContainer(id);
  }
}

export const containerStore = new ContainerStore();
