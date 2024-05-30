import { makeAutoObservable } from 'mobx';
import { CityService } from '../Api/CityService';
import { City } from '../Models/City';

class CityStore {
  city = [];
  selectedCargo = null;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchCity(name) {
    this.city = await CityService.getCityByName(name);
  }
}

export const cityStore = new CityStore();
